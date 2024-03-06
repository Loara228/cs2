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
        public UILabel(string text = "text") : base(0, 0)
        {
            Font = Fonts.Consolas;
            FontSize = (int)Font.FontSize;
            TextColor = Brushes.White;
            Text = text;

            Rectangle r = initGraphics!.GetTextRect(Font, Text, out _, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            BrushBackground = Brushes.Black;

            Margin = new Margin(5, 5, 0);
        }

        public UILabel(Font font, IBrush textColor, string text = "text") : base(0, 0)
        {
            Font = font;
            FontSize = (int)Font.FontSize;
            TextColor = textColor;
            Text = text;

            Rectangle r = initGraphics!.GetTextRect(Font, Text, out _, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            BrushBackground = Brushes.Black;

            Margin = new Margin(5, 5, 0);
        }

        public UILabel(string text, bool ig)
        {
            Font = Fonts.Consolas;
            FontSize = (int)Font.FontSize;
            TextColor = Brushes.White;
            Text = text;
        }

        public static UILabel CreateWithoutGraphics(string text)
        {
            return new UILabel(text, true);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            Rectangle r = g.GetTextRect(Font, Text, out float num, X, Y);
            Width = (int)r.Width;
            Height = (int)r.Height;
            g.DrawText(Font, FontSize, TextColor, X + num, Y + num, Text);
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

        //[Obsolete(":/")]
        public float FontSize
        {
            get; set;
        }
    }
}
