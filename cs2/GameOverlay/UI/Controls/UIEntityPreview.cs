using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.Game.Structs;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIEntityPreview : UIControl
    {
        public UIEntityPreview() : base()
        {
            DrawBackground = true;
            BrushBackground = Brushes.UIHeaderColor;
            Width = 216;
            Height = 408;

            entity = new Entity();

            entity.Bones = new List<(Game.Structs.Bone bone, System.Numerics.Vector3 pos, int id)>()
            {
                (Bone.head, new Vector3(954.7295f, 415.85663f, 0), 6),
                (Bone.neck_0, new Vector3(952.2365f, 441.90872f, 0), 5),
                (Bone.spine_1, new Vector3(955.5058f, 474.60562f, 0), 4),
                (Bone.spine_2, new Vector3(955.9851f, 532.4214f, 0), 2),
                (Bone.pelvis, new Vector3(954.05f, 555.16516f, 0), 0),
                (Bone.arm_upper_L, new Vector3(989.6412f, 457.27667f, 0), 8),
                (Bone.arm_lower_L, new Vector3(1014.2875f, 500.1058f, 0), 9),
                (Bone.hand_L, new Vector3(1003.1033f, 493.09515f, 0), 10),
                (Bone.arm_upper_R, new Vector3(912.8344f, 455.026f, 0), 13),
                (Bone.arm_lower_R, new Vector3(928.72833f, 496.86215f, 0), 14),
                (Bone.hand_R, new Vector3(985.596f, 495.02234f, 0), 15),
                (Bone.leg_upper_L, new Vector3(970.9408f, 577.0981f, 0), 22),
                (Bone.leg_lower_L, new Vector3(1011.0058f, 657.6345f, 0), 23),
                (Bone.ankle_L, new Vector3(1015.11084f, 735.11847f, 0), 24),
                (Bone.leg_upper_R, new Vector3(933.1797f, 573.2026f, 0), 25),
                (Bone.leg_lower_R, new Vector3(902.495f, 661.83746f, 0), 26),
                (Bone.ankle_R, new Vector3(901.279f, 706.8034f, 0), 27),
            };

            entity.Origin = new Vector3(947.4335f, 745.9617f, 0);

            FixPos();

            entity.Health = 70;
            entity.IsDefusing = true;
            entity.IsScoped = true;
            entity.FlashDuration = 3.5f;

            entity.Weapon = new Weapon();
            entity.Weapon.WeaponIndex = WeaponDefIndex.Glock;
            entity.Weapon.Ammo1 = 20;
            entity.Weapon.Ammo2 = 120;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            WallHack.Draw(g, true, entity);
        }

        public void Attach(UIForm form)
        {
            Position = new Point(form.Rect.Right - Width - 1, form.Rect.Top + UIForm.HEADER_SIZE + form.Height / 2 - Height / 2);
            Height = form.Height - UIForm.HEADER_SIZE - 1;
            Y = form.Y + UIForm.HEADER_SIZE;
            form.onMove += Move;
        }

        public void Move(int X, int Y)
        {
            for (int i = 0; i < entity.Bones.Count; i++)
            {
                var bone = entity.Bones[i];
                entity.Bones[i] = (bone.bone, new Vector3(X + bone.pos.X, Y + bone.pos.Y, 0), bone.id);
            }

            entity.Origin = new Vector3(X + entity.Origin.X, Y + entity.Origin.Y, 0);
            entity.HeadPos = entity.Bones[0].pos;
            Rect = new Rectangle(Rect.Left + X, Rect.Top + Y, Rect.Left + X + Rect.Width, Rect.Top + Y + Rect.Height);
        }

        private void FixPos()
        {
            for (int i = 0; i < entity.Bones.Count; i++)
            {
                var bone = entity.Bones[i];
                entity.Bones[i] = (bone.bone, new Vector3(bone.pos.X - 840, bone.pos.Y - 370, 0), bone.id);
            }
            entity.Origin = new Vector3(entity.Origin.X - 840, entity.Origin.Y - 370, 0);
            entity.HeadPos = entity.Bones[0].pos;
        }

        public Point Position
        {
            get => base.Position;
            set
            {
                Rect = new Rectangle(value.X, value.Y, value.X + Width, value.Y + Height);

                for (int i = 0; i < entity.Bones.Count; i++)
                {
                    var bone = entity.Bones[i];
                    entity.Bones[i] = (bone.bone, new Vector3(X + bone.pos.X, Y + bone.pos.Y, 0), bone.id);
                }

                entity.Origin = new Vector3(X + entity.Origin.X, Y + entity.Origin.Y, 0);
                entity.HeadPos = entity.Bones[0].pos;
            }
        }

        public Entity entity;
    }
}
