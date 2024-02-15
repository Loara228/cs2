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
        public UILabel(string text = "") : base(0, 0)
        {
            Font = Fonts.Consolas;
            TextColor = Brushes.White;
            Text = text;

            Rectangle r = initGraphics!.GetTextRect(Font, Text, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            BrushBackground = Brushes.Black;

            Margin = new Margin(5, 5, 0);
        }

        public UILabel(Font font, IBrush textColor, string text = "") : base(0, 0)
        {
            Font = font;
            TextColor = textColor;
            Text = text;

            Rectangle r = initGraphics!.GetTextRect(Font, Text, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            BrushBackground = Brushes.Black;

            Margin = new Margin(5, 5, 0);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            Rectangle r = g.GetTextRect(Font, Text, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            g.DrawText(Font, TextColor, X, Y, Text);
        }

        public string Text
        {
            get; set;
        } = "";

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
