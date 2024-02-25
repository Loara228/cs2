using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIButton : UIControl
    {
        public UIButton(string text) : base()
        {
            DrawBackground = true;
            Font = Fonts.Consolas;
            Text = text;
            TextColor = Brushes.White;
            BrushBackground = Brushes.UIHeaderColor;

            MinWidth = 80;
            MinHeight = 30;
        }
        public UIButton(string text, Action clicked) : base()
        {
            DrawBackground = true;
            Font = Fonts.Consolas;
            Text = text;
            TextColor = Brushes.White;
            BrushBackground = Brushes.UIHeaderColor;

            MinWidth = 80;
            MinHeight = 30;

            this.Clicked = clicked;
        }

        public UIButton(Font font, string text = "") : base()
        {
            DrawBackground = true;

            Font = font;
            BrushBackground = Brushes.UIHeaderColor;
            TextColor = Brushes.White;
            Text = text;

            MinWidth = 80;
            MinHeight = 50;
        }

        public override void Update()
        {
            base.Update();

            if (Rect.IsMouseOn())
            {
                BrushBackground = Brushes.UIButtonMouseOn;

                if (Input.MouseLeft.state == Input.KeyState.PRESSED)
                    Clicked?.Invoke();
            }
            else
            {
                BrushBackground = Brushes.UIHeaderColor;
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            Rectangle rectText = g.GetTextRect(Font, FontSize, Text, out float num);
            g.DrawText(Font, FontSize, TextColor, new Point(this.X + this.Width / 2 - rectText.Width / 2 + num, this.Y + this.Height / 2 - rectText.Height / 2 + num), Text);
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

        public float FontSize
        {
            get; set;
        } = Fonts.Consolas.FontSize;

        public Action? Clicked;
    }
}
