using cs2.Config;
using cs2.Game.Features;
using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormCrosshair : UIForm
    {
        public FormCrosshair(int x, int y) : base(x, y, "crosshair")
        {
            Width = 190;
            Add(_switcherCrosshair = new UISwitcher("Enable", new((x) => Configuration.Current.Misc_Crosshair = x)));
            Add(_switcherSniperOnly = new UISwitcher("Sniper rifle only", new((x) => Configuration.Current.Crosshair_Sniper_Only = x)));

            Add(new UILabel("Length") { FontSize = 12, Margin = new Margin(10, 8 + PREVIEW_HEIGHT, -5, 5) });
            Add(_sliderLength = new UISlider(Configuration.Current.Crosshair_Length, this, 10) { Margin = new Margin(10, 8, 9, 10) });
            _sliderLength.onValueChanged += (v) => Configuration.Current.Crosshair_Length = v;

            Add(new UILabel("Thickness") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderThickness = new UISlider(Configuration.Current.Crosshair_Thickness, this, 10));
            _sliderThickness.onValueChanged += (v) => Configuration.Current.Crosshair_Thickness = v;

            Add(new UILabel("Gap") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderGap = new UISlider(Configuration.Current.Crosshair_Gap, this, 10));
            _sliderGap.onValueChanged += (v) => Configuration.Current.Crosshair_Gap = v;

            Add(new UILabel("Outline") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderOutLine = new UISlider(Configuration.Current.Crosshair_Outline, this, 2));
            _sliderOutLine.onValueChanged += (v) => Configuration.Current.Crosshair_Outline = v;

            Add(_labelFillColor = new UILabel2("Fill", Configuration.Current.Crosshair_Fill));
            Add(_labelStrokeColor = new UILabel2("Stroke", Configuration.Current.Crosshair_Stroke));

            _labelFillColor.ColorChanged += (color) => Configuration.Current.Crosshair_Fill = color;
            _labelStrokeColor.ColorChanged += (color) => Configuration.Current.Crosshair_Stroke = color;

            _labelFillColor.colorMarginLeft = Width - 10;
            _labelStrokeColor.colorMarginLeft = Width - 10;

            Width = 200;
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (Overlay.drawUI)
            {
                var bounds = new Rectangle(X + 5, _switcherSniperOnly.Rect.Bottom + 5, Rect.Right - 10, _switcherSniperOnly.Rect.Bottom + PREVIEW_HEIGHT - 5);
                g.FillRectangle(Brushes.UIActiveColorThird, bounds);
                Crosshair.DrawPreview(g, bounds);
            }
        }

        public override void ApplyConfig()
        {
            _switcherCrosshair.Checked = Configuration.Current.Misc_Crosshair;
            _switcherSniperOnly.Checked = Configuration.Current.Crosshair_Sniper_Only;

            _sliderLength.Value = Configuration.Current.Crosshair_Length;

            _labelFillColor.Color = Configuration.Current.Crosshair_Fill;
            _labelStrokeColor.Color = Configuration.Current.Crosshair_Stroke;
        }

        private UISwitcher _switcherCrosshair;
        private UISwitcher _switcherSniperOnly;

        private UISlider _sliderLength;
        private UISlider _sliderThickness;
        private UISlider _sliderGap;
        private UISlider _sliderOutLine;

        private UILabel2 _labelFillColor;
        private UILabel2 _labelStrokeColor;

        private const int PREVIEW_HEIGHT = 80;
    }
}
