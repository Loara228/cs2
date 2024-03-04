using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UICheckbox : UISwitcher
    {
        public UICheckbox(string text, Action<bool> onChecked) : base(text, onChecked)
        {
            DrawBackground = false;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            _switcherRect = new Rectangle(X, Y, X + SWITCHER_H, Y + SWITCHER_H);
            g.FillRoundedRectangle(Brushes.UIHeaderColor, new RoundedRectangle(_switcherRect, SWITCHER_H / 4));
            Rectangle bounds = g.GetTextRect(Font, Text, out float num, X, Y);
            g.DrawText(Font, TextColor, X + SWITCHER_H + TEXT_OFFSET, Y + Height / 2 - bounds.Height / 2 + num, Text);

            if (Checked)
            {
                Point cbCenter = new Point(_switcherRect.Left + SWITCHER_H / 2, _switcherRect.Top + SWITCHER_H / 2);
                g.DrawLine(Brushes.UIActiveColor, cbCenter.X - 2, cbCenter.Y + 4, cbCenter.X + 5, cbCenter.Y - 2, 2);
                g.DrawLine(Brushes.UIActiveColor, cbCenter.X - 2, cbCenter.Y + 4, cbCenter.X - 4, cbCenter.Y - 0, 2);
            }
        }
    }
}
