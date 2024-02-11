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

                Vector3 v2HeadPos = Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v3headPos);
                Vector3 v3Pos = entity.Origin;
                Vector3 v2Pos = Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v3Pos);

                if (!v2HeadPos.IsValidScreen() || !v2Pos.IsValidScreen())
                    continue;

                float boxHeight = v2Pos.Y - v2HeadPos.Y;
                float boxWidth = (boxHeight / 2) * 1.25f;


                rect = new Rectangle(v2Pos.X - (boxWidth / 2), v2HeadPos.Y - (boxHeight / 8) + 1, v2Pos.X - (boxWidth / 2) + boxWidth, v2HeadPos.Y + boxHeight);

                if (Boxes)
                    g.DrawRectangleEdges(Brushes.Boxes, rect, 1);

                if (Health)
                {
                    Rectangle hpBarRect = new Rectangle(rect.Left, rect.Bottom + 4, rect.Right, rect.Bottom + 9);
                    g.FillRectangle(Brushes.HalfBlack, hpBarRect);
                    g.DrawVerticalProgressBar(Brushes.Black, Brushes.White, rect.Left, rect.Bottom + 4, rect.Right, rect.Bottom + 9, 1, entity.Health);
                }

                if (Weapon)
                {
                    string weaponIcon = entity.Weapon.ToIcon().ToString();
                    Rectangle weaponIconRect = g.GetTextRect(Fonts.WeaponIcons, 14, weaponIcon);
                    g.DrawText(Fonts.WeaponIcons, 12, Brushes.Black, rect.Left + 2 + rect.Width / 2 - weaponIconRect.Width / 2, rect.Bottom + weaponIconRect.Height / 2 + 2, weaponIcon);
                    g.DrawText(Fonts.WeaponIcons, 12, Brushes.White, rect.Left + rect.Width / 2 - weaponIconRect.Width / 2, rect.Bottom + weaponIconRect.Height / 2, weaponIcon);
                }

                string alerts = "";

                if (Scoped && entity.IsScoped)
                {
                    alerts += "scoped\n";
                }
                if (Defusing && entity.IsDefusing)
                {
                    alerts += "defusing\n";
                }

                Rectangle alertsRect = g.GetTextRect(Fonts.Consolas, 14, alerts);
                g.DrawText(Fonts.Consolas, 14, Brushes.Red, rect.Left + rect.Width / 2 - alertsRect.Width / 2, rect.Top - alertsRect.Height, alerts);
            }
        }

        private static void DrawBones(Graphics g)
        {
            if (!Bones)
                return;
            foreach (var entity in Program.Entities)
            {
                SolidBrush brush = Brushes.Bones;

                if (entity == null)
                    continue;
                if (!entity.IsAlive() || entity.Team == Program.LocalPlayer.Team)
                    continue;

                g.DrawLineWorld(brush, 0.8f, entity.Bones[0].pos, entity.Bones[1].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[1].pos, entity.Bones[2].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[2].pos, entity.Bones[3].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[3].pos, entity.Bones[4].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[2].pos, entity.Bones[5].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[5].pos, entity.Bones[6].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[6].pos, entity.Bones[7].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[2].pos, entity.Bones[8].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[8].pos, entity.Bones[9].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[9].pos, entity.Bones[10].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[4].pos, entity.Bones[11].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[11].pos, entity.Bones[12].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[12].pos, entity.Bones[13].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[4].pos, entity.Bones[14].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[14].pos, entity.Bones[15].pos);
                g.DrawLineWorld(brush, 0.8f, entity.Bones[15].pos, entity.Bones[16].pos);
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

        public static bool Scoped
        {
            get; set;
        } = true;

        public static bool Defusing
        {
            get; set;
        } = true;

        public static bool Weapon
        {
            get; set;
        } = true;
    }
}
