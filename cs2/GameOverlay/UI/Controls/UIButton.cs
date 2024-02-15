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
        public UIButton(IBrush backgroundBrush, IBrush textColor, Font font, string text = "") : base(new Rectangle(0, 0, 0, 0), backgroundBrush)
        {
            DrawBackground = true;

            Font = font;
            TextColor = textColor;
            Text = text;

            MinWidth = 100;
            MinHeight = 100;

            Margin = new Margin(10);
        }

        public override void Update()
        {
            base.Update();

            if (Rect.IsMouseOn() && Input.MouseLeft.state == Input.KeyState.PRESSED)
            {
                Clicked?.Invoke();
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            Rectangle rectText = g.GetTextRect(Font, Text, 0, 0);
            g.DrawText(Font, TextColor, new Point(this.X + this.Width / 2 - rectText.Width / 2, this.Y + this.Height / 2 - rectText.Height / 2), Text);
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

        public Action Clicked;
    }
}
