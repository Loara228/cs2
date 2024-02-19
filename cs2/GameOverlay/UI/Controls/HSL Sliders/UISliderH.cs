using GameOverlay.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls.HSL_Sliders
{
    internal class UISliderH : UISlider
    {
        public UISliderH(float value) : base(value)
        {
            DrawBackground = false;
            MaxValue = 360;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics graphics)
        {   

            if (!_dragging)
            {
                UpdateCirclePos();
            }

            List<GradientStop> gradientStops = new List<GradientStop>();
            List<RawColor4> colors =
            [
                ColorConverter.ColorFromHSL(0   / 360f, s, l),
                ColorConverter.ColorFromHSL(60  / 360f, s, l),
                ColorConverter.ColorFromHSL(120 / 360f, s, l),
                ColorConverter.ColorFromHSL(180 / 360f, s, l),
                ColorConverter.ColorFromHSL(240 / 360f, s, l),
                ColorConverter.ColorFromHSL(300 / 360f, s, l),
                ColorConverter.ColorFromHSL(360 / 360f, s, l),
            ];

            for (int i = 0; i < colors.Count; i++)
            {
                float pos = 1f / (colors.Count - 1) * i;
                gradientStops.Add(new GradientStop() { Color = colors[i], Position = pos });
            }

            GradientStopCollection gradientStopCollection = new GradientStopCollection(graphics.GetRenderTarget(), gradientStops.ToArray(), Gamma.StandardRgb, ExtendMode.Clamp);

            LinearGradientBrushProperties props = new LinearGradientBrushProperties()
            {
                StartPoint = new RawVector2(Rect.Left, Rect.Top),
                EndPoint = new RawVector2(Rect.Right, Rect.Top)
            };

            SharpDX.Direct2D1.LinearGradientBrush linearGradientBrush = new SharpDX.Direct2D1.LinearGradientBrush(graphics.GetRenderTarget(), props, gradientStopCollection);
            graphics.GetRenderTarget().FillRoundedRectangle(new SharpDX.Direct2D1.RoundedRectangle() { Rect = this.Rect, RadiusX = Rect.Height / 2, RadiusY = Rect.Height / 2 }, linearGradientBrush);
            gradientStopCollection.Dispose();
            linearGradientBrush.Dispose();

            Brushes.Share.Color = ColorConverter.ColorFromHSL(Value / 360, s, l);
            graphics.FillCircle(Brushes.Share, _circle);
            if (_dragging)
            {
                graphics.DrawCircle(Brushes.White, _circle, 1);
            }
        }

        public float s = 1, l = 0.5f;

    }
}
