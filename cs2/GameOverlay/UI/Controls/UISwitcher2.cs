using cs2.GameOverlay.UI.Forms;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UISwitcher2 : UISwitcher
    {
        public UISwitcher2(string text, Color color, Action<bool> onSwitched) : base(text, onSwitched)
        {
            this.Color = color;
        }

        public override void Update()
        {
            base.Update();
            if (Input.MouseLeft.state == Input.KeyState.PRESSED && _circle.IsMouseOn())
            {
                Overlay.Current.Open(
                    new FormColorPicker((int)_circle.Location.X, (int)_circle.Location.Y)
                    { Confirmed = new Action<Color>(OnColorChanged) });
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            _circle = new Circle(X + colorMarginLeft - COLOR_RADIUS / 2, Y + 2 + Height / 2 - COLOR_RADIUS / 2f, COLOR_RADIUS);
            Brushes.Share.Color = Color;
            g.FillCircle(Brushes.Share, _circle);
        }

        private void OnColorChanged(Color color)
        {
            this.Color = color;
            ColorChanged?.Invoke(color);
        }

        public Color Color
        {
            get; set;
        }

        public Action<Color> ColorChanged = null!;

        private Circle _circle;
        public int colorMarginLeft;
        private const int COLOR_RADIUS = 6;
    }
}
