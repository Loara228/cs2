using cs2.Config;
using cs2.Game.Objects;
using cs2.Game.Structs;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using System.Numerics;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class AimAssist
    {
        public static void Start()
        {
            new Thread(() =>
            {
                for (; ; )
                {
                    Update();
                    Thread.Sleep(1);
                }
            }).Start();
            new Thread(() =>
            {
                for (; ; )
                {
                    UpdateTB();
                    Thread.Sleep(1);
                }
            }).Start();
        }

        private static void Update()
        {
            if (LocalPlayer.Current.Weapon.IsPistol)
                CurrentWeaponConfig = Configuration.Current.Pistols;
            else if (LocalPlayer.Current.Weapon.IsSMG)
                CurrentWeaponConfig = Configuration.Current.SMGs;
            else if (LocalPlayer.Current.Weapon.IsShotgun)
                CurrentWeaponConfig = Configuration.Current.Shotguns;
            else if (LocalPlayer.Current.Weapon.IsRifle)
                CurrentWeaponConfig = Configuration.Current.Rifles;
            else if (LocalPlayer.Current.Weapon.IsSniperRifle)
                CurrentWeaponConfig = Configuration.Current.SniperRifles;
            else
                return;

            vkBindXBtn.Update();

            if (!CurrentWeaponConfig.EnableAimAssist)
            {
                Thread.Sleep(100);
                return;
            }

            UpdateAim();
        }

        public static void Draw(Graphics g)
        {
            if (!CurrentWeaponConfig.EnableAimAssist)
                return;
            if (Waiting)
                return;

            _screenCenter = new Vector2(g.Width / 2 - 0.5f, g.Height / 2 - 0.5f);

            Brushes.FOVColor.Color = _targetPtr == IntPtr.Zero ? new Color(255, 0, 0, 100) : new Color(0, 255, 0, 100);
            g.DrawCircle(Brushes.FOVColor, _screenCenter.X, _screenCenter.Y, FOVRadius, 1);
            if (_targetPos != Vector3.Zero)
            {
                var pos = _targetPos.ToScreenPos();
                if (pos.IsValidScreen())
                    g.FillCircle(Brushes.Red, pos.X, pos.Y, 4);
            }
        }

        private static void UpdateTB()
        {
            if (!CurrentWeaponConfig.EnableTriggerbot)
            {
                Thread.Sleep(1000);
                return;
            }
            Triggerbot.Update(vkBindXBtn.state);
        }

        private static void Aim(Entity entity)
        {
            if (_targetPtr == IntPtr.Zero)
            {
                return;
            }

            Vector3 velocity = CurrentWeaponConfig.VelocityPrediction ? entity.Velocity / 20f : Vector3.Zero;

            GetAimAngles(_targetPos + velocity, out Vector2 aimAngles);
            GetAimPixels(aimAngles *= 1 / (CurrentWeaponConfig.Smoothing + 1), out var aimPixels);
            if (TryMouseMove(aimPixels))
                Thread.Sleep(1);
        }

        private static void UpdateAim()
        {
            UpdateAngles();

            _entities.Clear();
            for (int i = 0; i < Program.ENTITY_LIST_COUNT; i++)
            {
                Entity entity = new(i, true);
                entity.Update();

                if (!entity.IsAlive() || !entity.CheckTeam())
                    continue;
                entity.UpdateAimProperties();
                _entities.Add(entity);
            }

            if ((vkBindXBtn.state == Input.KeyState.DOWN) && _targetPtr == IntPtr.Zero)
                FindTarget();
            else if (vkBindXBtn.state == Input.KeyState.NONE)
            {
                _targetPos = Vector3.Zero;
                _targetPtr = IntPtr.Zero;
                _targetBone = Bone.UNKNOWN;
            }
            else if (vkBindXBtn.state == Input.KeyState.RELEASE)
            {
                _targetPos = Vector3.Zero;
                _targetPtr = IntPtr.Zero;
                _targetBone = Bone.UNKNOWN;
            }
            if (vkBindXBtn.state == Input.KeyState.DOWN)
            {
                if (_targetPtr == IntPtr.Zero)
                    return;

                Entity? entity = _entities.Find(x => x.AddressBase == _targetPtr);

                if (entity == null)
                    return;

                _targetPos = entity.Bones.Find(x => x.bone == _targetBone).pos;

                if (_targetPos == Vector3.Zero)
                    return;

                Aim(entity);
            }
        }

        #region Targeting

        private static void FindTarget(bool displayPoint = false)
        {
            float minDistance = 99999;
            foreach (var entity in _entities)
                minDistance = FindBone(entity, minDistance, displayPoint);
        }

        private static float FindBone(Entity entity, float minDistance, bool displayPoint = false)
        {
            IntPtr targetPtr = IntPtr.Zero;
            Vector3 targetPos = Vector3.Zero;
            Vector2 targetPos2d = Vector2.Zero;
            Bone targetBone = Bone.UNKNOWN;

            Circle aimFOV = new Circle(_screenCenter.X, _screenCenter.Y, displayPoint ? 500 : FOVRadius);

            foreach (var bone in entity.Bones)
            {
                if ((int)bone.bone == (int)Hitbox.head ||
                    (int)bone.bone == (int)Hitbox.neck_0 ||
                    (int)bone.bone == (int)Hitbox.spine_1 ||
                    (int)bone.bone == (int)Hitbox.spine_2 ||
                    (int)bone.bone == (int)Hitbox.pelvis)
                {
                    Vector3 boneScreenPos = bone.pos.ToScreenPos();
                    Vector2 boneScreenPos2 = new Vector2(boneScreenPos.X, boneScreenPos.Y);
                    if (!boneScreenPos.IsValidScreen())
                        continue;
                    if (!boneScreenPos2.Touching(aimFOV, _screenCenter.X, _screenCenter.Y))
                        continue;
                    float distance = Vector2.Distance(_screenCenter, boneScreenPos2);
                    if (distance < minDistance)
                    {
                        minDistance = distance;

                        targetPos2d = boneScreenPos2;
                        targetBone = bone.bone;
                        targetPos = bone.pos;
                        targetPtr = entity.AddressBase;
                    }
                }
            }

            if (targetPtr == IntPtr.Zero)
                return minDistance;

            if (displayPoint)
                _targetPos2D = targetPos2d;
            if (!displayPoint)
            {
                _targetPtr = targetPtr;
                _targetPos = targetPos;
                _targetBone = targetBone;
            }

            return minDistance;
        }

        #endregion

        #region Angles

        private static void UpdateAngles()
        {
            IntPtr AddressBase = Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerPawn);
            Origin = Memory.Read<Vector3>(AddressBase + C_BasePlayerPawn.m_vOldOrigin);
            ViewOffset = Memory.Read<Vector3>(AddressBase + C_BaseModelEntity.m_vecViewOffset);
            EyePosition = Origin + ViewOffset;
            ViewAngles = Memory.Read<Vector3>(Memory.ClientPtr + ClientOffsets.dwViewAngles);
            AimPunchAngle = Memory.Read<Vector3>(AddressBase + C_CSPlayerPawn.m_aimPunchAngle);
            if (!CurrentWeaponConfig.RCS)
                AimPunchAngle = Vector3.Zero;
            AimDirection = GetAimDirection(ViewAngles, AimPunchAngle);
            ShotsFired = Memory.Read<int>(AddressBase + C_CSPlayerPawnBase.m_iShotsFired);
            IntPtr pCameraServices = Memory.Read<IntPtr>(AddressBase + C_BasePlayerPawn.m_pCameraServices);
            EyeDirection = GetVectorFromEulerAngles(ViewAngles.X.DegreeToRadian(), ViewAngles.Y.DegreeToRadian());
            PlayerFov = Memory.Read<int>(pCameraServices + 0x210);
            if (PlayerFov == 0)
                PlayerFov = 90;

        }

        #region Calc

        private static Vector3 GetVectorFromEulerAngles(double phi, double theta)
        {
            return new Vector3
            (
                (float)(System.Math.Cos(phi) * System.Math.Cos(theta)),
                (float)(System.Math.Cos(phi) * System.Math.Sin(theta)),
                (float)-System.Math.Sin(phi)
            ).Normalized();
        }

        private static Vector3 GetAimDirection(Vector3 viewAngles, Vector3 aimPunchAngle)
        {
            var phi = (viewAngles.X + aimPunchAngle.X * 2f).DegreeToRadian();
            var theta = (viewAngles.Y + aimPunchAngle.Y * 2f).DegreeToRadian();

            return Vector3.Normalize(new Vector3
            (
                (float)(Math.Cos(phi) * Math.Cos(theta)),
                (float)(Math.Cos(phi) * Math.Sin(theta)),
                (float)-Math.Sin(phi)
            ));
        }

        private static void GetAimPixels(Vector2 aimAngles, out Point aimPixels)
        {
            var fovRatio = 90.0 / PlayerFov;
            aimPixels = new Point
            (
                (int)Math.Round(aimAngles.X / AnglePerPixel * fovRatio),
                (int)Math.Round(aimAngles.Y / AnglePerPixel * fovRatio)
            );
        }
        private static bool TryMouseMove(Point aimPixels)
        {
            if (CurrentWeaponConfig.ShotsFired > ShotsFired)
                return false;
            if (aimPixels.X == 0 && aimPixels.Y == 0)
            {
                return false;
            }

            Input.MouseMove((int)aimPixels.X, (int)aimPixels.Y);
            return true;
        }

        private static void GetAimAngles(Vector3 pointWorld, out Vector2 aimAngles)
        {
            var aimDirection = AimDirection;
            var aimDirectionDesired = (pointWorld - EyePosition).Normalized();
            aimAngles = new Vector2
            (
                aimDirectionDesired.AngleToSigned(aimDirection, new Vector3(0, 0, 1)),
                aimDirectionDesired.AngleToSigned(aimDirection, aimDirectionDesired.Cross(new Vector3(0, 0, 1)).Normalized())
            );
        }

        #endregion

        #endregion

        public static void Calibrate()
        {
            Thread.Sleep(1000);
            Console.Beep();
            Thread.Sleep(1000);
            Console.Beep();
            Thread.Sleep(1000);
            Console.Beep(700, 600);
            var AnglePerPixel = new[]
            {
                CalibrationMeasureAnglePerPixel(100),
                CalibrationMeasureAnglePerPixel(-200),
                CalibrationMeasureAnglePerPixel(300),
                CalibrationMeasureAnglePerPixel(-400),
                CalibrationMeasureAnglePerPixel(200),
            }.Average();
            Console.Beep();
            Console.WriteLine($"{nameof(AnglePerPixel)} = {AnglePerPixel}");
        }

        private static double CalibrationMeasureAnglePerPixel(int deltaPixels)
        {
            // measure starting angle
            Thread.Sleep(100);
            var eyeDirectionStart = EyeDirection;
            eyeDirectionStart.Z = 0;

            // rotate
            Input.MouseMove(deltaPixels, 0);

            // measure end angle
            Thread.Sleep(100);
            var eyeDirectionEnd = EyeDirection;
            eyeDirectionEnd.Z = 0;

            // get angle and divide by number of pixels
            return eyeDirectionEnd.AngleTo(eyeDirectionStart) / Math.Abs(deltaPixels);
        }

        public static bool Waiting
        {
            get; set;
        } = false;

        private static float FOVRadius
        {
            get
            {
                return LocalPlayer.Current.IsScoped ? 450 : CurrentWeaponConfig.FOV + 10;
            }
        }

        private static Vector3 Origin { get; set; }
        private static Vector3 ViewOffset { get; set; }
        public static Vector3 EyePosition { get; private set; }
        public static Vector3 ViewAngles { get; set; }
        public static Vector3 AimPunchAngle { get; private set; }
        public static Vector3 AimDirection { get; private set; }
        public static int ShotsFired { get; private set; }

        private static WeaponConfig CurrentWeaponConfig { get; set; } = Configuration.Current.Pistols;

        public static double AnglePerPixel { get; set; } = 0.0007679434260353446;
        private static Vector3 EyeDirection { get; set; }

        public static int PlayerFov { get; set; }

        private static List<Entity> _entities = new List<Entity>();

        public static Input.Key vkBindXBtn = new Input.Key(6); //6 xbtn

        internal static IntPtr _targetPtr = IntPtr.Zero;
        internal static Vector3 _targetPos = Vector3.Zero;
        internal static Vector2 _targetPos2D = Vector2.Zero;
        internal static Bone _targetBone = Bone.UNKNOWN;

        private static Vector2 _screenCenter;

        public enum Hitbox
        {
            head = 6,
            neck_0 = 5,
            spine_1 = 4,
            spine_2 = 2,
            pelvis = 0,
        }
    }
}
