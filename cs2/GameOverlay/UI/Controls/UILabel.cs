using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UILabel : UIControl
    {
        public UILabel(Font font, IBrush textColor, uint fontSize = 12, string text = "") : base(new Rectangle(0, 0, 0, 0), Brushes.Black)
        {
            DrawBackground = false;

            Font = font;
            FontSize = fontSize;
            TextColor = textColor;
            Text = text;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.DrawText(Font, FontSize, TextColor, X, Y, Text);
        }

        public string Text
        {
            get; set;
        } = "";

        public uint FontSize
        {
            get; private set;
        }

        public IBrush TextColor
        {
            get; set;
        }

        public Font Font
        {
            get; set;
        }
    }
}
