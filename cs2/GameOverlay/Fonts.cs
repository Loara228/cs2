using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{
    internal static class Fonts
    {
        public static void Initialize(Graphics g)
        {
            _consolas = g.CreateFont("Consolas", 11);
            _weapons = g.CreateFont("csgo_icons", 16);
            //obs_icons.ttf
            _fontFactory = new SharpDX.DirectWrite.Factory();
        }

        public static void Dispose()
        {
            _consolas.Dispose();
            _fontFactory.Dispose();
        }

        public static Font Consolas
        {
            get => _consolas;
        }

        public static Font WeaponIcons
        {
            get => _weapons;
        }

        public static SharpDX.DirectWrite.Factory FontFactory
        {
            get => _fontFactory;
        }

        private static SharpDX.DirectWrite.Factory _fontFactory = null!;
        private static Font _consolas = null!;
        private static Font _weapons = null!;
    }
}
