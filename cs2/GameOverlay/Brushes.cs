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
            _uiActiveColorThird = g.CreateSolidBrush(25, 25, 26, 255);
            _uiBorderColor = g.CreateSolidBrush(56, 56, 56, 255);
            _uiButtonMouseOn = g.CreateSolidBrush(66, 66, 66);

            _uiBombColor = g.CreateSolidBrush(232, 94, 94);
            _uiDefuceColor = g.CreateSolidBrush(94, 172, 232);

            // overlay hide
            _uiBorderColor2 = g.CreateSolidBrush(0, 0, 0, 70);
            _uiHeaderColor2 = g.CreateSolidBrush(0, 0, 0, 180);
            _uiBackgroundColor2 = g.CreateSolidBrush(0, 0, 0, 50);

            _fovColor = g.CreateSolidBrush(255, 255, 255, 100);

            _share = g.CreateSolidBrush(255, 0, 0, 255);
            _share2 = g.CreateSolidBrush(255, 0, 0, 255);
            _rgb = g.CreateSolidBrush(255, 0, 0, 255);

        }

        public static void Update()
        {
            if (r == 255 && g < 255 && b == 0)
                g += speed;
            else if (r > 0 && g == 255 && b == 0)
                r -= speed;
            else if (r == 0 && g == 255 && b < 255)
                b += speed;
            else if (r == 0 && g > 0 && b == 255)
                g -= speed;
            else if (r < 255 && g == 0 && b == 255)
                r += speed;
            else if (r == 255 && g == 0 && b > 0)
                b -= speed;
            RGB.Color = new Color(r, g, b);
        }

        public static void Dispose()
        {

        }

        public static SolidBrush RGB { get => _rgb; }
        public static SolidBrush FlashbangColor { get => _flashbangColor; }
        public static SolidBrush FOVColor { get => _fovColor; }
        public static SolidBrush Black { get => _black; }
        public static SolidBrush White { get => _white; }
        public static SolidBrush Red { get => _red; }
        public static SolidBrush Green { get => _green; }
        public static SolidBrush HalfBlack { get => _halfBlack; }
        public static SolidBrush ScoreboardElementT { get => _scoreboardElementT; }
        public static SolidBrush ScoreboardElementCT { get => _scoreboardElementCT; }

        public static SolidBrush Share
        {
            get
            {
                if (_share.Color.R == 0 && Math.Round(_share.Color.G, 4) == 0.0039d && _share.Color.B == 0)
                    _share.Color = new Color(RGB.Color.R, RGB.Color.G, RGB.Color.B, _share.Color.A);
                return _share;
            }
        }
        public static SolidBrush Share2
        {
            get
            {
                if (_share2.Color.R == 0 && Math.Round(_share2.Color.G, 4) == 0.0039d && _share2.Color.B == 0)
                    _share2.Color = new Color(RGB.Color.R, RGB.Color.G, RGB.Color.B, _share2.Color.A);
                return _share2;
            }
        }


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
        public static SolidBrush UIActiveColorThird { get => _uiActiveColorThird; }
        public static SolidBrush UIHeaderColor { get => _uiHeaderColor; }
        public static SolidBrush UITextColor { get => _uiTextColor; }
        public static SolidBrush UIBackgroundColor2 { get => _uiBackgroundColor2; }
        public static SolidBrush UIHeaderColor2 { get => _uiHeaderColor2; }
        public static SolidBrush UIBorderColor2 { get => _uiBorderColor2; }
        public static SolidBrush UIButtonMouseOn { get => _uiButtonMouseOn; }
        public static SolidBrush UIBombColor { get => _uiBombColor; }
        public static SolidBrush UIDefuseColor { get => _uiDefuceColor; }

        #endregion

        private static SolidBrush _fovColor = null!;

        private static SolidBrush _scoreboardElementT = null!;
        private static SolidBrush _scoreboardElementCT = null!;

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
        private static SolidBrush _uiActiveColorThird = null!;

        private static SolidBrush _uiHeaderColor = null!;
        private static SolidBrush _uiHeaderColor2 = null!;
        private static SolidBrush _uiTextColor = null!;
        private static SolidBrush _uiButtonMouseOn = null!;

        private static SolidBrush _uiBombColor = null!;
        private static SolidBrush _uiDefuceColor = null!;

        #endregion

        private static SolidBrush _share = null!;
        private static SolidBrush _share2 = null!;
        private static SolidBrush _rgb = null!;

        private static byte r = 255, g, b, speed = 5;
    }
}
