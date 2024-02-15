using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UISwitcher : UIControl
    {
        public UISwitcher(string text, Action<bool> onSwitched) : base()
        {
            BrushBackground = Brushes.Black;
            this.Text = text;
            Rectangle bounds = initGraphics!.GetTextRect(Font, Text, X, Y);
            Width = (int)bounds.Width + TEXT_OFFSET + SWITCHER_W;
            Height = SWITCHER_H;
            OnSwitched = onSwitched;
        }

        public override void Update()
        {
            base.Update();

            if (Rect.IsMouseOn() && Input.MouseLeft.state == Input.KeyState.PRESSED)
            {
                Checked = !Checked;
            }
        }

        public override void Draw(Graphics g)
        {
            _switcherRect = new Rectangle(X, Y, X + SWITCHER_W, Y + SWITCHER_H);
            base.Draw(g);
            //g.FillRectangle(Brushes.Red, _switcherRect);
            g.FillRoundedRectangle(Brushes.UIHeaderColor, new RoundedRectangle(_switcherRect, SWITCHER_H / 2));
            float circlePoint = Checked ? _switcherRect.Right - SwitcherRaduis - 3 :
                                          _switcherRect.Left + SwitcherRaduis + 3;
            Circle circle = new Circle(circlePoint, _switcherRect.Top + SwitcherRaduis + 2, SwitcherRaduis);
            g.FillCircle(Checked ? Brushes.UIActiveColor : Brushes.UIActiveColorSecond, circle);
            g.DrawText(Font, TextColor, X + SWITCHER_W + TEXT_OFFSET, Y, Text);
        }

        public Font Font
        {
            get; set;
        } = Fonts.Consolas;

        public IBrush TextColor
        {
            get; set;
        } = Brushes.White;

        public string Text
        {
            get; set;
        } = "";

        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                OnSwitched?.Invoke(value);
            }
        }

        public bool CanSwitch
        {
            get; set;
        }

        private float SwitcherRaduis
        {
            get => SWITCHER_H / 2 - 2;
        }

        public Action<bool>? OnSwitched
        {
            get; set;
        }

        private Rectangle _switcherRect;
        private bool _checked;

        private const int SWITCHER_W = 45, SWITCHER_H = 18, TEXT_OFFSET = 5;
    }
}
