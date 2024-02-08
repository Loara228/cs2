using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Features
{
    internal static class WallHack
    {
        public static void Draw(Graphics g)
        {
            DrawBones(g);
            DrawRect(g);
        }

        private static void DrawRect(Graphics g)
        {
            foreach (var entity in Program.Entities)
            {
                if (entity == null)
                    continue;
                if (!entity.IsAlive() || entity.Team == Program.LocalPlayer.Team)
                    continue;

                Rectangle rect = new Rectangle();
                Vector3 v3headPos = entity.HeadPos;
                if (v3headPos == Vector3.Zero || entity.Origin == Vector3.Zero)
                    continue;
                Vector3 v2HeadPos = Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v3headPos);
                Vector3 v3Pos = entity.Origin;
                Vector3 v2Pos = Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v3Pos);

                if (!v2HeadPos.IsValidScreen() || !v2Pos.IsValidScreen())
                    return;

                float boxHeight = v2Pos.Y - v2HeadPos.Y;
                float boxWidth = (boxHeight / 2) * 1.25f;


                rect = new Rectangle(v2Pos.X - (boxWidth / 2), v2HeadPos.Y - (boxHeight / 8) + 1, v2Pos.X - (boxWidth / 2) + boxWidth, v2HeadPos.Y + boxHeight);

                if (Boxes)
                    g.DrawRectangleEdges(Brushes.Boxes, rect, 1);

                if (Health)
                {
                    g.FillRectangle(Brushes.HalfBlack, rect.Left, rect.Bottom + 4, rect.Right, rect.Bottom + 9);
                    g.DrawVerticalProgressBar(Brushes.Black, Brushes.White, rect.Left, rect.Bottom + 4, rect.Right, rect.Bottom + 9, 1, entity.Health);
                }

                if (Weapon)
                    g.DrawText(Fonts.Consolas, Brushes.White, rect.Left, rect.Bottom + 10, entity.WeaponIndex.ToString());
            }
        }

        private static void DrawBones(Graphics g)
        {
            if (!Bones)
                return;
            foreach (var entity in Program.Entities)
            {
                if (entity == null)
                    continue;
                if (!entity.IsAlive() || entity.Team == Program.LocalPlayer.Team)
                    continue;

                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[0].pos, entity.Bones[1].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[1].pos, entity.Bones[2].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[2].pos, entity.Bones[3].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[3].pos, entity.Bones[4].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[2].pos, entity.Bones[5].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[5].pos, entity.Bones[6].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[6].pos, entity.Bones[7].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[2].pos, entity.Bones[8].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[8].pos, entity.Bones[9].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[9].pos, entity.Bones[10].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[4].pos, entity.Bones[11].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[11].pos, entity.Bones[12].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[12].pos, entity.Bones[13].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[4].pos, entity.Bones[14].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[14].pos, entity.Bones[15].pos);
                g.DrawLineWorld(Brushes.Bones, 0.8f, entity.Bones[15].pos, entity.Bones[16].pos);
            }
        }

        public static bool Bones
        {
            get; set;
        } = true;

        public static bool Boxes
        {
            get; set;
        } = false;

        public static bool Health
        {
            get; set;
        } = true;

        public static bool Weapon
        {
            get; set;
        } = true;
    }
}
