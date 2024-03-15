using cs2.Config;
using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Features
{
    internal class Crosshair
    {
        public static void Draw(Graphics g)
        {
            if (!Configuration.Current.Misc_Crosshair)
                return;

            if (Configuration.Current.Crosshair_Sniper_Only && !LocalPlayer.Current.Weapon.IsSniperRifle)
                return;

            Point center = new(g.Width / 2 + 0.5f, g.Height / 2 + 0.5f);
            DrawCrosshair(g, center);
        }

        public static void DrawPreview(Graphics g, Rectangle bounds)
        {
            Point center = new(bounds.Left + bounds.Width / 2 + 0.5f, bounds.Top + bounds.Height / 2 + 0.5f);
            DrawCrosshair(g, center);
        }

        private static void DrawCrosshair(Graphics g, Point center)
        {
            float outline = Configuration.Current.Crosshair_Outline;

            Brushes.Share.Color = Configuration.Current.Crosshair_Fill;
            Brushes.Share2.Color = Configuration.Current.Crosshair_Stroke;
            var fillColor = Brushes.Share;
            var strokeColor = Brushes.Share2;

            g.OutlineFillRectangle(strokeColor, fillColor, GetHorizontalCrosshairElement(center, -1), outline);
            g.OutlineFillRectangle(strokeColor, fillColor, GetHorizontalCrosshairElement(center, 1), outline);
            g.OutlineFillRectangle(strokeColor, fillColor, GetVerticalCrosshairElement(center, 1), outline);
            g.OutlineFillRectangle(strokeColor, fillColor, GetVerticalCrosshairElement(center, -1), outline);
        }

        private static Rectangle GetHorizontalCrosshairElement(Point center, sbyte dir)
        {
            float gap = Configuration.Current.Crosshair_Gap;
            float length = Configuration.Current.Crosshair_Length;
            float thickness = Configuration.Current.Crosshair_Thickness;

            return new Rectangle(center.X + ((gap + length) * dir), center.Y - thickness, center.X + gap * dir, center.Y + thickness);
        }

        // 1    lower
        // -1   upper
        private static Rectangle GetVerticalCrosshairElement(Point center, sbyte dir)
        {
            float gap = Configuration.Current.Crosshair_Gap;
            float length = Configuration.Current.Crosshair_Length;
            float thickness = Configuration.Current.Crosshair_Thickness;

            return new Rectangle(center.X - thickness, center.Y + ((gap + length) * dir), center.X + thickness, center.Y + gap * dir);
        }
    }
}
