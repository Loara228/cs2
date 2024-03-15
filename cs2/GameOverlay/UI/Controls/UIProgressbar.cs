using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIProgressbar : UIControl
    {
        public UIProgressbar()
        {
            MinWidth = 50;
            MinHeight = 10;

            Width = MinWidth;
            Height = MinHeight;

            ActiveColor = Brushes.UIActiveColor;
        }

        public UIProgressbar(float value) : this()
        {
            _value = value;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            g.FillRoundedRectangle(Brushes.UIActiveColorThird, new RoundedRectangle(Rect, Rect.Height / 2));
            Rectangle fillRect = new Rectangle(Rect.Left, Rect.Top, Rect.Left + (Rect.Width / MaxValue * _value), Rect.Bottom);
            g.FillRoundedRectangle(ActiveColor, new RoundedRectangle(fillRect, Rect.Height / 2));

        }

        public IBrush ActiveColor
        {
            get; set;
        }

        public float Value
        {
            get => Value;
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 100)
                    value = 0;
                _value = value;
            }
        }

        public float MaxValue
        {
            get; set;
        }

        private float _value = 0;
    }
}
