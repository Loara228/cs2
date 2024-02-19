using cs2.GameOverlay.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormAimAssist : UIForm
    {
        public FormAimAssist(int x) : base(x, 0, "Aim")
        {
            this.Width = 200;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            UISlider sliderFov;
            UISlider sliderAimMult;

            Add(new UISwitcher("Aim assist", new((x) => Configuration.Current.EnableAimAssist = x)));
            Add(new UISwitcher("Triggerbot", new((x) => Configuration.Current.EnableTriggerbot = x)));

            Add(new UILabel("FOV:") { Margin = new Margin(5, 5, -8) });
            Add(sliderFov = new UISlider(Configuration.Current.FOV_Radius));
            sliderFov.onValueChanged += SliderFOV_OnValueChanged;
            sliderFov.Value = Configuration.Current.FOV_Radius;
            sliderFov.MaxValue = 400;
            sliderFov.Width = Width - sliderFov.Margin.Left * 2;

            Add(new UILabel("Aim multiplier:"));
            Add(sliderAimMult = new UISlider(Configuration.Current.AimAssistMult));
            sliderAimMult.onValueChanged += SliderAimMult_OnValueChanged;
            sliderAimMult.Value = Configuration.Current.AimAssistMult;
            sliderAimMult.MaxValue = 17;
            sliderAimMult.Width = Width - sliderAimMult.Margin.Left * 2;
        }

        private void SliderFOV_OnValueChanged(float value) => Configuration.Current.FOV_Radius = value + 50;
        private void SliderAimMult_OnValueChanged(float value) => Configuration.Current.AimAssistMult = value + 1;
    }
}
