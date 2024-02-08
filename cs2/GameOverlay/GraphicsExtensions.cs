using GameOverlay.Drawing;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{
    internal static class GraphicsExtensions
    {
        public static void DrawLineWorld(this Graphics g, SolidBrush brush, float stroke, params Vector3[] verticesWorld)
        {

            var verticesScreen = verticesWorld
                .Select(v => Program.LocalPlayer.MatrixViewProjectionViewport.Transform(v))
                .Where(v => v.Z < 1)
                .Select(v => new Vector2(v.X, v.Y)).ToArray();
            if (verticesScreen.Length < 2 || verticesScreen.Length % 2 != 0) return;
            g.DrawLine(brush,
                new Point(verticesScreen[0].X, verticesScreen[0].Y),
                new Point(verticesScreen[1].X, verticesScreen[1].Y), stroke);
        }
        public static Rectangle GetTextRect(this Graphics g, global::GameOverlay.Drawing.Font font, float fontSize, string text, float x, float y)
        {
            float num = (x < 0f) ? ((float)g.Width + x) : ((float)g.Width - x);
            float num2 = (y < 0f) ? ((float)g.Height + y) : ((float)g.Height - y);
            if (num <= fontSize)
            {
                num = (float)g.Width;
            }
            if (num2 <= fontSize)
            {
                num2 = (float)g.Height;
            }
            TextLayout textLayout = new TextLayout(Fonts.FontFactory, text, font.TextFormat, num, num2);
            float num3 = font.FontSize * 0.25f;
            RawRectangleF rect = new RawRectangleF(x - num3, y - num3, x + textLayout.Metrics.Width + num3, y + textLayout.Metrics.Height + num3);

            textLayout.Dispose();

            return new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static bool IsValidScreen(this Vector3 value)
        {
            return !value.X.IsInfinityOrNaN() && !value.Y.IsInfinityOrNaN() && value.Z >= 0 && value.Z < 1;
        }

        public static bool IsInfinityOrNaN(this float value)
        {
            return float.IsNaN(value) || float.IsInfinity(value);
        }
    }
}
