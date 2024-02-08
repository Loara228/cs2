using GameOverlay.Drawing;
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
