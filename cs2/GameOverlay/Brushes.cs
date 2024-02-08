using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{
    internal static class Brushes
    {
        public static void Initialize(Graphics g)
        {
            _bones = g.CreateSolidBrush(255, 0, 0, 125);
            _boxes = g.CreateSolidBrush(106, 244, 255, 50);

            _halfBlack = g.CreateSolidBrush(0, 0, 0, 127);

            _black = g.CreateSolidBrush(0, 0, 0, 255);
            _white = g.CreateSolidBrush(255, 255, 255, 255);
        }

        public static void Dispose()
        {
            _bones.Dispose();
        }

        public static SolidBrush Bones
        {
            get => _bones;
        }

        public static SolidBrush Boxes
        {
            get => _boxes;
        }

        public static SolidBrush Black
        {
            get => _black;
        }

        public static SolidBrush White
        {
            get => _white;
        }

        public static SolidBrush HalfBlack
        {
            get => _halfBlack;
        }

        private static SolidBrush _bones = null!;
        private static SolidBrush _boxes = null!;

        private static SolidBrush _halfBlack = null!;

        private static SolidBrush _white = null!;
        private static SolidBrush _black = null!;
    }
}
