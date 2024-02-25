using cs2.Config;
using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace cs2.Game.Features
{
    internal static class Radar
    {
        public static void Draw(Graphics g, Rectangle radarRect)
        {
            if (Overlay.drawUI)
                return;

            Vector2 radarCenter = new Vector2(radarRect.Left + radarRect.Width / 2, radarRect.Top + radarRect.Height / 2);

            g.FillRectangle(Brushes.UIBackgroundColor2, radarRect);
            g.DrawRectangle(Brushes.UIBackgroundColor2, radarRect, 1);

            g.DrawLine(Brushes.UIBorderColor2, new Line(new Point(radarRect.Left, radarCenter.Y),
                                                        new Point(radarRect.Right, radarCenter.Y)), 0.5f);
            g.DrawLine(Brushes.UIBorderColor2, new Line(new Point(radarCenter.X, radarRect.Top),
                                                        new Point(radarCenter.X, radarRect.Bottom)), 0.5f);

            foreach (Entity entity in Program.Entities)
            {
                if (!entity.IsAlive() || !entity.CheckTeam())
                    continue;

                Vector3 localPos = LocalPlayer.Current.Origin;
                Vector2 entityWorldPos = new Vector2(localPos.X - entity.Origin.X, localPos.Y - entity.Origin.Y);
                entityWorldPos /= Configuration.Current.RadarScale + 1;
                entityWorldPos.X *= -1;
                entityWorldPos += radarCenter;
                Vector2 entityRadarPos = TransformPosToRadar(entityWorldPos, radarCenter, radarRect, LocalPlayer.Current.ViewAngles.Y - 90);

                if (entityRadarPos == Vector2.Zero)
                    return;


                g.FillCircle(entity.TeamColor, entityRadarPos.X, entityRadarPos.Y, Configuration.Current.RadarEnemyRadius + 1);
            }
        }

        private static Vector2 TransformPosToRadar(Vector2 pointToRotate, Vector2 radarCenter, Rectangle radarRect, float angle)
        {
            Vector2 rotatedPoint = new Vector2();
            angle = (float)(angle * (Math.PI / (float)180));

            float cosTheta = (float)Math.Cos(angle);
            float sinTheta = (float)Math.Sin(angle);

            rotatedPoint.X = cosTheta * (pointToRotate.X - radarCenter.X) - sinTheta * (pointToRotate.Y - radarCenter.Y);
            rotatedPoint.Y = sinTheta * (pointToRotate.X - radarCenter.X) + cosTheta * (pointToRotate.Y - radarCenter.Y);

            rotatedPoint.X += radarCenter.X;
            rotatedPoint.Y += radarCenter.Y;


            if (rotatedPoint.X > radarRect.Left + radarRect.Width)
                rotatedPoint.X = radarRect.Left + radarRect.Width;

            if (rotatedPoint.Y > radarRect.Top + radarRect.Height)
                rotatedPoint.Y = radarRect.Top + radarRect.Height;

            if (rotatedPoint.X < radarRect.Left)
                rotatedPoint.X = radarRect.Left;

            if (rotatedPoint.Y < radarRect.Top)
                rotatedPoint.Y = radarRect.Top;

            return rotatedPoint;
        }
    }
}
