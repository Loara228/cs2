using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIRadioButton : UIControl
    {
        public UIRadioButton(string text, UIContainer owner) : base()
        {
            this.owner = owner;
            this.Text = text;

            MinWidth = 18;
            MinHeight = 18;
            Width = 0;
            Height = 0;

            Margin = new Margin(10, Margin.Top, Margin.Bottom);
        }
        public UIRadioButton(string text, UIContainer owner, bool @checked) : this(text, owner)
        {
            _checked = @checked;
        }

        public override void Update()
        {
            base.Update();

            if (Rect.IsMouseOn() && Input.MouseLeft.state == Input.KeyState.PRESSED)
            {
                Checked = true;
            }
        }

        public override void Draw(Graphics g)
        {
            _center = new Circle(X + Width / 2f, Y + Height / 2f, (Height / 2f));

            base.Draw(g);
            
            g.OutlineFillCircle(Checked ? Brushes.UIActiveColor : Brushes.UIActiveColorSecond, Brushes.UIHeaderColor, _center, 1.5f);
            if (Checked)
            {
                g.FillCircle(Brushes.UIActiveColor, _center.Location.X, _center.Location.Y, _center.Radius - 4);
            }
            Rectangle bounds = g.GetTextRect(Font, Text, out float num, X, Y);
            g.DrawText(Font, TextColor, Rect.Right + TEXT_OFFSET, Y + Height / 2 - bounds.Height / 2 + num, Text);
        }

        public void Check()
        {
            owner.Controls.OfType<UIRadioButton>().ToList().ForEach(x =>
            {
                x._checked = false;
            });
            _checked = true;
            OnChecked?.Invoke(true);
        }

        public bool Checked
        {
            get => _checked;
            set
            {
                if (value)
                    Check();
            }
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

        public Action<bool>? OnChecked
        {
            get; set;
        }

        private bool _checked;
        protected UIContainer owner;
        protected Circle _center;

        private const int TEXT_OFFSET = 5;

    }
}
