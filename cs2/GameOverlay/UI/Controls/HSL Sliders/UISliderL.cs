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
    internal class UISliderL : UISlider
    {
        public UISliderL(float value) : base(value)
        {
            DrawBackground = false;
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
            RawColor4 color1 = ColorConverter.ColorFromHSL(h, s, 0f);
            RawColor4 color2 = ColorConverter.ColorFromHSL(h, s, 0.5f);
            RawColor4 color3 = ColorConverter.ColorFromHSL(h, s, 1f);
            List<RawColor4> colors = new List<RawColor4>()
            {
                color1,
                color2,
                color3
            };

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

            Brushes.Share.Color = ColorConverter.ColorFromHSL(h, s, Value / 100f);
            graphics.FillCircle(Brushes.Share, _circle);
            if (_dragging)
            {
                graphics.DrawCircle(Brushes.White, _circle, 1);
            }
        }

        public float h = 0, s = 1f;
    }
}
