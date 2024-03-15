#pragma warning disable

using cs2.GameOverlay.UI.Controls;
using cs2.GameOverlay.UI.Controls.HSL_Sliders;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormColorPicker : UIForm
    {
        public FormColorPicker(int x, int y) : base(x, y, "", true)
        {
            DrawBackground = false;
            Width = 250;
            MinHeight = 250;

            this.Color = Color.Red;

            InitializeComponents();
        }


        private void InitializeComponents()
        {
            Add(_sliderH = new UISliderH(0) { Margin = new Margin(10, preview_height + 10, 8, 10) });
            Add(_sliderS = new UISliderS(100) { Margin = new Margin(8) });
            Add(_sliderL = new UISliderL(50) { Margin = new Margin(8) });
            Add(_sliderA = new UISlider(100) { Margin = new Margin(8) });
            Add(_labelHSL = UILabel.CreateWithoutGraphics("HSLA"));

            _labelHSL.FontSize = 12;

            _sliderH.Width = Width - _sliderH.Margin.Left * 2;
            _sliderS.Width = Width - _sliderS.Margin.Left * 2;
            _sliderL.Width = Width - _sliderL.Margin.Left * 2;
            _sliderA.Width = Width - _sliderA.Margin.Left * 2;

            _sliderH.onValueChanged += (val) => UpdateColor();
            _sliderS.onValueChanged += (val) => UpdateColor();
            _sliderL.onValueChanged += (val) => UpdateColor();
            _sliderA.onValueChanged += (val) => UpdateColor();

            UIButton btn = new UIButton("OK", new(() =>
            {
                Confirmed?.Invoke(Color);
                Close();
            }));

            UIButton btnRgb = new UIButton("RGB", new(() =>
            {
                Confirmed?.Invoke(new Color(0, 1, 0, (int)(Color.A * 255f)));
                Close();
            }));

            UIButton btnCancel = new UIButton("Cancel", new(() =>
            {
                Close();
            }));

            btn.Margin = new Margin(1, 20, 1, 1);
            btnRgb.Margin = new Margin(1, 1, 1, 1);
            btnCancel.Margin = new Margin(1, 1, 1, 1);

            btn.Width = this.Width - 2;
            btnRgb.Width = this.Width - 2;
            btnCancel.Width = this.Width - 2;

            Add(btn);
            Add(btnRgb);
            Add(btnCancel);

            UpdateColor();
        }

        private void UpdateResult()
        {
            _labelHSL.Text = $"HSLA: ({Math.Round(_sliderH.Value, 0)}, {Math.Round(_sliderS.Value, 2)}%, {Math.Round(_sliderL.Value, 2)}%, {Math.Round(_sliderA.Value, 2)}%)";
        }

        private void UpdateColor()
        {
            float
                h = _sliderH.Value / 360f,
                s = _sliderS.Value / 100f,
                l = _sliderL.Value / 100f;

            if (l <= 0.1f)
                l = 0;

            _sliderH.s = s;
            _sliderH.l = l;

            _sliderS.h = h;
            _sliderS.l = l;

            _sliderL.h = h;
            _sliderL.s = s;

            Color = ColorConverter.ColorFromHSL(h, s, l, _sliderA.Value / 100f);


            UpdateResult();
        }

        public override void FocusChanged()
        {
            if (UIForm.FocusedForm != this)
                Close();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            if (Overlay.drawUI)
            {
                Brushes.Share.Color = this.Color;
                g.FillRectangle(Brushes.Share, new Rectangle(X, Y, Rect.Right, Y + preview_height));
                g.FillRectangle(Brushes.UIBackgroundColor, new Rectangle(X, Y + preview_height, Rect.Right, Rect.Bottom));
            }
            base.Draw(g);
        }

        public Color Color
        {
            get; set;
        }

        public Action<Color> Confirmed = null!;

        private UISliderH _sliderH;
        private UISliderS _sliderS;
        private UISliderL _sliderL;
        private UISlider _sliderA;
        private UILabel _labelHSL;

        private const int preview_height = 60;
    }
}
