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
        }

        public static void Dispose()
        {
            _consolas.Dispose();
        }

        public static Font Consolas
        {
            get => _consolas;
        }

        private static Font _consolas = null!;
    }
}
