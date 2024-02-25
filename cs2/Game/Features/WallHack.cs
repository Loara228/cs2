using cs2.Config;
using cs2.Game.Objects;
using cs2.Game.Structs;
using cs2.GameOverlay;
using cs2.Offsets;
using cs2.Offsets.Interfaces;
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
        public static void Draw(Graphics g, bool preview = false, Entity? pEntity = null)
        {
            DrawBones(g, preview, pEntity);
            if (preview)
                DrawEnt(g, pEntity!, true);
            else
                DrawEntities(g);
        }

        private static void DrawEntities(Graphics g)
        {
            foreach (var entity in Program.Entities)
            {
                DrawEnt(g, entity);
            }
        }

        private static void DrawEnt(Graphics g, Entity entity, bool preview = false)
        {
            if (entity == null)
                return;
            if (!preview)
                if (!entity.IsAlive())
                    return;

            if (!preview && !entity.CheckTeam())
                return;

            Rectangle rect = new Rectangle();
            Vector3 v3headPos = entity.HeadPos;

            Vector3 v2HeadPos = preview ? entity.HeadPos : LocalPlayer.Current.MatrixViewProjectionViewport.Transform(v3headPos);
            Vector3 v3Pos = preview ? entity.Origin : entity.Origin;
            Vector3 v2Pos = preview ? v3Pos : LocalPlayer.Current.MatrixViewProjectionViewport.Transform(v3Pos);

            if (!preview)
                if (!v2HeadPos.IsValidScreen() || !v2Pos.IsValidScreen())
                    return;

            float boxHeight = v2Pos.Y - v2HeadPos.Y;
            float boxWidth = (boxHeight / 2) * 1.25f;


            rect = new Rectangle(v2Pos.X - (boxWidth / 2), v2HeadPos.Y - (boxHeight / 8) + 1, v2Pos.X - (boxWidth / 2) + boxWidth, v2HeadPos.Y + boxHeight);

            if (Configuration.Current.ESP_Boxes)
            {
                Brushes.Share.Color = Configuration.Current.ESP_Boxes_Color;
                g.DrawRoundedRectangle(Brushes.Share, new RoundedRectangle(rect, 8), Configuration.Current.ESP_Boxes_Stroke);
            }

            if (Configuration.Current.ESP_Health)
            {
                DrawHealth(g, entity, rect);
            }

            string weaponIcon = "";
            if (Configuration.Current.ESP_Weapon)
            {
                weaponIcon = entity.Weapon.ToIcon().ToString();
            }
            if (Configuration.Current.ESP_Ammo)
            {
                if (Configuration.Current.ESP_Weapon)
                    weaponIcon += " ";

                weaponIcon += $"{entity.Weapon.Ammo1}/{entity.Weapon.Ammo2}";
            }
            if (weaponIcon != "")
            {
                Rectangle weaponIconRect = g.GetTextRect(Fonts.WeaponIcons, Configuration.Current.ESP_Weapon_Font_Size + 8f, weaponIcon, out float num, rect.Left);
                Brushes.Share.Color = Configuration.Current.ESP_Weapon_Color;
                g.DrawText(Fonts.WeaponIcons, Configuration.Current.ESP_Weapon_Font_Size + 8f, Brushes.Share, rect.Left + (rect.Width / 2) - (weaponIconRect.Width / 2) + num, rect.Bottom + (weaponIconRect.Height / 2) + 5, weaponIcon);
            }

            //if (Configuration.Current.ESP_Alerts)
            //{
            //    string alerts = "";
            //    if (Scoped && entity.IsScoped)
            //        alerts += "scoped\n";
            //    if (Defusing && entity.IsDefusing)
            //        alerts += "defusing\n";

            //    Rectangle alertsRect = g.GetTextRect(Fonts.Consolas, alerts, out _);
            //    g.DrawText(Fonts.Consolas, Brushes.Red, rect.Left + rect.Width / 2 - alertsRect.Width / 2, rect.Top - alertsRect.Height, alerts);
            //}
            DrawFlashState(g, entity, rect, preview);
        }

        private static void DrawHealth(Graphics g, Entity entity, Rectangle rect)
        {
            const int h = 4, offset = 5;
            g.FillRectangle(Brushes.HalfBlack, rect.Left, rect.Bottom + h + offset, rect.Right, rect.Bottom + offset);
            g.DrawVerticalProgressBar(Brushes.Black, Brushes.White, rect.Left, rect.Bottom + h + offset, rect.Right, rect.Bottom + offset, 1, entity.Health);
        }

        private static void DrawFlashState(Graphics g, Entity entity, Rectangle rect, bool preview = false)
        {
            if (entity.FlashDuration == 0 || !Configuration.Current.ESP_Flash)
                return;
            if (entity.FlashTimer == 0)
                return;
            float perc = preview ? 75 : entity.FlashTimer / 5f * 100f;
            const int h = 4, offset = 5;
            g.FillRectangle(Brushes.HalfBlack, rect.Left, rect.Top - h - offset, rect.Right, rect.Top - offset);
            g.DrawVerticalProgressBar(Brushes.Black, Brushes.FlashbangColor, rect.Left, rect.Top - h - offset, rect.Right, rect.Top - offset, 1, perc);
        }

        private static void DrawBones(Graphics g, bool preview = false, Entity? pEntity = null)
        {
            if (!Configuration.Current.ESP_Skeleton)
                return;
            float stroke = Configuration.Current.ESP_Bone_Stroke;
            Brushes.Share.Color = Configuration.Current.ESP_Skeleton_Color;
            SolidBrush brush = Brushes.Share;
            if (preview)
            {
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[0].pos, pEntity.Bones[1].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[1].pos, pEntity.Bones[2].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[2].pos, pEntity.Bones[3].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[3].pos, pEntity.Bones[4].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[2].pos, pEntity.Bones[5].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[5].pos, pEntity.Bones[6].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[6].pos, pEntity.Bones[7].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[2].pos, pEntity.Bones[8].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[8].pos, pEntity.Bones[9].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[9].pos, pEntity.Bones[10].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[4].pos, pEntity.Bones[11].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[11].pos, pEntity.Bones[12].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[12].pos, pEntity.Bones[13].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[4].pos, pEntity.Bones[14].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[14].pos, pEntity.Bones[15].pos);
                g.DrawLineWorld(brush, stroke, preview, pEntity.Bones[15].pos, pEntity.Bones[16].pos);
                return;
            }
            foreach (var entity in Program.Entities)
            {

                if (entity == null)
                    continue;
                if (!entity.IsAlive() || !entity.CheckTeam())
                    continue;

                g.DrawLineWorld(brush, stroke, preview, entity.Bones[0].pos, entity.Bones[1].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[1].pos, entity.Bones[2].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[2].pos, entity.Bones[3].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[3].pos, entity.Bones[4].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[2].pos, entity.Bones[5].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[5].pos, entity.Bones[6].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[6].pos, entity.Bones[7].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[2].pos, entity.Bones[8].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[8].pos, entity.Bones[9].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[9].pos, entity.Bones[10].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[4].pos, entity.Bones[11].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[11].pos, entity.Bones[12].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[12].pos, entity.Bones[13].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[4].pos, entity.Bones[14].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[14].pos, entity.Bones[15].pos);
                g.DrawLineWorld(brush, stroke, preview, entity.Bones[15].pos, entity.Bones[16].pos);
            }
        }

        public static bool Scoped
        {
            get; set;
        } = true;

        public static bool Defusing
        {
            get; set;
        } = true;
    }
}
