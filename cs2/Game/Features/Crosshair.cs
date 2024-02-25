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

            if (LocalPlayer.Current.Weapon.IsSniperRifle)
            {
                Point center = new(g.Width / 2 + 0.5f, g.Height / 2 + 0.5f);
                g.DrawCrosshair(Brushes.Black, center, 5f, 4f, CrosshairStyle.Plus);
                g.DrawCrosshair(Brushes.White, center, 4f, 2f, CrosshairStyle.Plus);
            }
        }
    }
}
