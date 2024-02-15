using cs2.Game.Objects;
using cs2.Game.Structs;
using cs2.GameOverlay;
using cs2.Offsets;
using GameOverlay.Drawing;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
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
            vkBind.Update();

            if (!Configuration.Current.EnableAimAssist)
            {
                Thread.Sleep(1);
                return;
            }

            if (Configuration.Current.EnableAimAssist)
                UpdateAim();
        }

        public static void Draw(Graphics g)
        {
            if (!Configuration.Current.EnableAimAssist)
                return;
            if (Waiting)
                return;

            _screenCenter = new Vector2(g.Width / 2 - 0.5f, g.Height / 2 - 0.5f);
            g.DrawCircle(Brushes.FOVColor, _screenCenter.X, _screenCenter.Y, FOVRadius, 1);
            if (_targetPos2D != Vector2.Zero)
            {
                if (_targetPos == Vector3.Zero)
                {
                    if (_targetPos2D != Vector2.Zero)
                    {
                        g.FillCircle(Brushes.Red, _targetPos2D.X, _targetPos2D.Y, 2);
                        _targetPos2D = Vector2.Zero;
                    }
                }
                else
                {
                    var pos = _targetPos.ToScreenPos();
                    if (pos.IsValidScreen())
                        g.FillCircle(Brushes.Red, pos.X, pos.Y, 4);
                }
            }
        }

        private static void UpdateTB()
        {
            if (!Configuration.Current.EnableTriggerbot)
            {
                Thread.Sleep(1000);
                return;
            }
            Triggerbot.Update(vkBind.state);
        }

        private static void Aim(Entity entity)
        {
            if (_targetPtr == IntPtr.Zero)
                return;

            Vector3 angle = CalcAngle(EyePosition, _targetPos, ViewAngles);

            double absX = Math.Abs(angle.X);
            double absY = Math.Abs(angle.Y);
            if (absX > 50 || absY > 50)
                return;

            if (absX < 0.3f && absY < 0.3f)
            {
                int xMove2 = -(int)(angle.Y * Configuration.Current.AimAssistMult);
                int yMove2 = (int)(angle.X * Configuration.Current.AimAssistMult);
                Input.MouseMove(xMove2, yMove2);
                Triggerbot.Shot();
            }

            int xMove = -(int)(angle.Y * Configuration.Current.AimAssistMult);
            int yMove = (int)(angle.X * Configuration.Current.AimAssistMult);

            Input.MouseMove(xMove, yMove);
        }

        private static void UpdateAim()
        {
            UpdateAngles();

            _entities.Clear();
            for (int i = 0; i < 12; i++)
            {
                Entity entity = new(i, true);
                entity.Update();

                if (!entity.IsAlive() || entity.Team == LocalPlayer.Current.Team)
                    continue;

                _entities.Add(entity);
            }

            if (vkBind.state == Input.KeyState.DOWN && _targetPtr == 0)
                FindTarget();
            else if (vkBind.state == Input.KeyState.NONE)
                FindTarget(true);
            else if (vkBind.state == Input.KeyState.RELEASE)
            {
                _targetPos = Vector3.Zero;
                _targetPtr = IntPtr.Zero;
                _targetBone = Bone.UNKNOWN;
                Triggerbot.Shot();
            }
            else if (vkBind.state == Input.KeyState.DOWN)
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
            AimDirection = GetAimDirection(ViewAngles, AimPunchAngle);
        }

        #region Calc

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

        private static Vector3 CalcAngle(Vector3 localPos, Vector3 targerPos, Vector3 viewAngles) =>
            ToAngle(targerPos - localPos) - viewAngles;

        private static Vector3 ToAngle(Vector3 vec3) =>
            new Vector3(
                (float)(Math.Atan2(-vec3.Z, Hypot(vec3.X, vec3.Y)) * (180 / Math.PI)),
                (float)(Math.Atan2(vec3.Y, vec3.X) * (180f / Math.PI)),
                0);

        private static double Hypot(float a, float b) => Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

        #endregion

        #region Properties

        private static Vector3 Origin { get; set; }
        private static Vector3 ViewOffset { get; set; }
        public static Vector3 EyePosition { get; private set; }
        public static Vector3 ViewAngles { get; set; }
        public static Vector3 AimPunchAngle { get; private set; }
        public static Vector3 AimDirection { get; private set; }

        #endregion

        #endregion

        public static Input.Key vkBind = new Input.Key(6);

        public static bool Waiting
        {
            get; set;
        } = false;

        public static float FOVRadius
        {
            get
            {
                return LocalPlayer.Current.IsScoped ? 450 : Configuration.Current.FOV_Radius;
            }
        }

        private static List<Entity> _entities = new List<Entity>();

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
