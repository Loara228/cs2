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
            _halfBlack = g.CreateSolidBrush(0, 0, 0, 90);

            _boxes = g.CreateSolidBrush(106, 244, 255, 100);
            _bones = g.CreateSolidBrush(255, 255, 0, 150);
            _flashbangColor = g.CreateSolidBrush(120, 209, 240);

            _black = g.CreateSolidBrush(0, 0, 0, 255);
            _white = g.CreateSolidBrush(255, 255, 255, 255);
            _red = g.CreateSolidBrush(255, 100, 100, 255);
            _green = g.CreateSolidBrush(100, 255, 100, 255);

            _teamYellow = g.CreateSolidBrush(241, 228, 66, 255);
            _teamBlue = g.CreateSolidBrush(137, 206, 245, 255);
            _teamOrange = g.CreateSolidBrush(230, 129, 43, 255);
            _teamPurple = g.CreateSolidBrush(190, 45, 151, 255);
            _teamGreen = g.CreateSolidBrush(0, 159, 129, 255);

            _scoreboardElementT = g.CreateSolidBrush(231, 208, 138, 255);
            _scoreboardElementCT = g.CreateSolidBrush(182, 212, 238, 255);

            _uiBackgroundColor = g.CreateSolidBrush(31, 31, 31, 255);
            _uiHeaderColor = g.CreateSolidBrush(46, 46, 46, 255);
            _uiTextColor = _white;
            _uiActiveColor = g.CreateSolidBrush(113, 96, 232, 255);
            _uiActiveColorSecond = g.CreateSolidBrush(153, 153, 153, 255);
            _uiBorderColor = g.CreateSolidBrush(56, 56, 56, 255);

            // opacity
            _uiBorderColor2 = g.CreateSolidBrush(0, 0, 0, 70);
            _uiHeaderColor2 = g.CreateSolidBrush(0, 0, 0, 180);
            _uiBackgroundColor2 = g.CreateSolidBrush(0, 0, 0, 50);

            _fovColor = g.CreateSolidBrush(255, 255, 255, 100);

        }

        public static void Dispose()
        {
            _bones.Dispose();
        }

        public static SolidBrush Bones { get => _bones; }
        public static SolidBrush Boxes { get => _boxes; }
        public static SolidBrush FlashbangColor { get => _flashbangColor; }
        public static SolidBrush FOVColor { get => _fovColor; }
        public static SolidBrush Black { get => _black; }
        public static SolidBrush White { get => _white; }
        public static SolidBrush Red { get => _red; }
        public static SolidBrush Green { get => _green; }
        public static SolidBrush HalfBlack { get => _halfBlack; }
        public static SolidBrush ScoreboardElementT { get => _scoreboardElementT; }
        public static SolidBrush ScoreboardElementCT { get => _scoreboardElementCT; }

        #region TeamColor

        public static SolidBrush TeamYellow { get => _teamYellow; }
        public static SolidBrush TeamBlue { get => _teamBlue; }
        public static SolidBrush TeamOrange { get => _teamOrange; }
        public static SolidBrush TeamPurple { get => _teamPurple; }
        public static SolidBrush TeamGreen { get => _teamGreen; }

        #endregion

        #region UI

        public static SolidBrush UIBackgroundColor { get => _uiBackgroundColor; }
        public static SolidBrush UIBorderColor { get => _uiBorderColor; }
        public static SolidBrush UIActiveColor { get => _uiActiveColor; }
        public static SolidBrush UIActiveColorSecond { get => _uiActiveColorSecond; }
        public static SolidBrush UIHeaderColor { get => _uiHeaderColor; }
        public static SolidBrush UITextColor { get => _uiTextColor; }
        public static SolidBrush UIBackgroundColor2 { get => _uiBackgroundColor2; }
        public static SolidBrush UIHeaderColor2 { get => _uiHeaderColor2; }
        public static SolidBrush UIBorderColor2 { get => _uiBorderColor2; }

        #endregion

        private static SolidBrush _fovColor = null!;

        private static SolidBrush _scoreboardElementT = null!;
        private static SolidBrush _scoreboardElementCT = null!;

        private static SolidBrush _bones = null!;
        private static SolidBrush _boxes = null!;
        private static SolidBrush _flashbangColor = null!;

        private static SolidBrush _halfBlack = null!;

        private static SolidBrush _white = null!;
        private static SolidBrush _black = null!;
        private static SolidBrush _red = null!;
        private static SolidBrush _green = null!;

        #region TeamColor

        private static SolidBrush _teamYellow = null!;
        private static SolidBrush _teamBlue = null!;
        private static SolidBrush _teamOrange = null!;
        private static SolidBrush _teamPurple = null!;
        private static SolidBrush _teamGreen = null!;

        #endregion

        #region UI Brushes

        private static SolidBrush _uiBackgroundColor = null!;
        private static SolidBrush _uiBackgroundColor2 = null!;

        private static SolidBrush _uiBorderColor = null!;
        private static SolidBrush _uiBorderColor2 = null!;
        private static SolidBrush _uiActiveColor = null!;
        private static SolidBrush _uiActiveColorSecond = null!;

        private static SolidBrush _uiHeaderColor = null!;
        private static SolidBrush _uiHeaderColor2 = null!;
        private static SolidBrush _uiTextColor = null!;

        #endregion
    }
}
