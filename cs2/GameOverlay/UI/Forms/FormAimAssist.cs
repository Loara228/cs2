using cs2.Config;
using cs2.Game.Features;
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
            this.Width = 352;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            UIContainerInline inlineContainer = new UIContainerInline();
            inlineContainer.Margin = new Margin(1, -1, 5, 1);

            Add(inlineContainer);

            void AddButton(UIButton btn, WeaponConfigType type)
            {
                btn.MinWidth = Width / 5;
                btn.MinHeight = 30;
                btn.Width = 0;
                btn.Height = 0;
                btn.Margin = 0;
                btn.Font = Fonts.WeaponIcons;
                btn.FontSize = 16;
                btn.TextColor = Brushes.UIActiveColorSecond;

                btn.Clicked += () => SelectWeapon(type);

                inlineContainer.Add(btn);
            }

            AddButton(btnPistols = new UIButton("\uE001"), WeaponConfigType.PISTOL);
            AddButton(btnSMGs = new UIButton("\uE024"), WeaponConfigType.SMG);
            AddButton(btnShotguns = new UIButton("\uE019"), WeaponConfigType.SHOTGUN);
            AddButton(btnRifles = new UIButton("\uE007"), WeaponConfigType.RIFLE);
            AddButton(btnSniperRifles = new UIButton("\uE009"), WeaponConfigType.SNIPER_RIFLE);

            btnPistols.TextColor = Brushes.UIActiveColor;


            Add(aim = new UISwitcher("Aim assist", new((x) => SelectedWeapon.EnableAimAssist = x)));
            Add(tb = new UISwitcher("Triggerbot", new((x) => SelectedWeapon.EnableTriggerbot = x)));
            Add(rcs = new UISwitcher("RCS", new((x) => SelectedWeapon.RCS = x)));
            Add(velocityPrediction = new UISwitcher("Velocity prediction", new((x) => SelectedWeapon.VelocityPrediction = x)));

            Add(new UILine(this));

            Add(new UILabel("FOV:") { Margin = new Margin(5, 5, -8) });
            Add(sliderFov = new UISlider(SelectedWeapon.FOV));
            sliderFov.onValueChanged += SliderFOV_OnValueChanged;
            sliderFov.Value = SelectedWeapon.FOV;
            sliderFov.MaxValue = 400;
            sliderFov.Width = Width - sliderFov.Margin.Left * 2;

            Add(labelSmooth = new UILabel($"Smooth: {SelectedWeapon.Smoothing}"));
            Add(sliderAimMult = new UISlider(SelectedWeapon.Smoothing));
            sliderAimMult.onValueChanged += SliderAimMult_OnValueChanged;
            sliderAimMult.Value = SelectedWeapon.Smoothing;
            sliderAimMult.MaxValue = 4;
            sliderAimMult.Width = Width - sliderAimMult.Margin.Left * 2;


            Add(labelShotsFired = new UILabel($"ShotsFired: {SelectedWeapon.ShotsFired}"));
            Add(sliderShotsFired = new UISlider(SelectedWeapon.ShotsFired));
            sliderShotsFired.onValueChanged += SliderShotsFired;
            sliderShotsFired.Value = SelectedWeapon.ShotsFired;
            sliderShotsFired.MaxValue = 3;
            sliderShotsFired.Width = Width - sliderAimMult.Margin.Left * 2;

            UIButton btn;
            Add(btn = new UIButton("AnglePerPixel"));
            btn.Width = Width - btn.Margin.Left * 2;
            btn.Clicked += () =>
            {
                if (SelectedWeapon.EnableAimAssist)
                    new Thread(() =>
                    {
                        AimAssist.Calibrate();
                    }).Start();
            };

            ApplyConfig();
        }

        public override void ApplyConfig()
        {
            WeaponConfig weapon = Configuration.Current.GetWeaponFromType(_selectedWeaponType);

            sliderAimMult.Value = weapon.Smoothing;
            sliderShotsFired.Value = weapon.ShotsFired;

            SliderAimMult_OnValueChanged(weapon.Smoothing);
            SliderShotsFired(weapon.ShotsFired);
            sliderFov.Value = weapon.FOV;

            aim.Checked = weapon.EnableAimAssist;
            tb.Checked = weapon.EnableTriggerbot;
            rcs.Checked = weapon.RCS;
            velocityPrediction.Checked = weapon.VelocityPrediction;

        }

        private void SelectWeapon(WeaponConfigType type)
        {
            this._selectedWeaponType = type;
            btnPistols.TextColor = Brushes.UIActiveColorSecond;
            btnSMGs.TextColor = Brushes.UIActiveColorSecond;
            btnRifles.TextColor = Brushes.UIActiveColorSecond;
            btnSniperRifles.TextColor = Brushes.UIActiveColorSecond;
            btnShotguns.TextColor = Brushes.UIActiveColorSecond;

            if (type == WeaponConfigType.PISTOL)
                btnPistols.TextColor = Brushes.UIActiveColor;
            else if (type == WeaponConfigType.SMG)
                btnSMGs.TextColor = Brushes.UIActiveColor;
            else if (type == WeaponConfigType.RIFLE)
                btnRifles.TextColor = Brushes.UIActiveColor;
            else if (type == WeaponConfigType.SNIPER_RIFLE)
                btnSniperRifles.TextColor = Brushes.UIActiveColor;
            else if (type == WeaponConfigType.SHOTGUN)
                btnShotguns.TextColor = Brushes.UIActiveColor;

            ApplyConfig();
        }

        private void SliderFOV_OnValueChanged(float value) => SelectedWeapon.FOV = value;

        private void SliderAimMult_OnValueChanged(float value)
        {
            float iValue = (float)Math.Round(value, 3);
            labelSmooth.Text = $"Smooth: {iValue}";
            Configuration.Current.GetWeaponFromType(_selectedWeaponType).Smoothing = iValue;
        }

        private void SliderShotsFired(float value)
        {
            int iValue = (int)Math.Round(value, 0);
            labelShotsFired.Text = $"ShotsFired: {iValue}";
            Configuration.Current.GetWeaponFromType(_selectedWeaponType).ShotsFired = iValue;
        }

        public WeaponConfig SelectedWeapon
        {
            get => Configuration.Current.GetWeaponFromType(_selectedWeaponType);
        }

        WeaponConfigType _selectedWeaponType = WeaponConfigType.PISTOL;

        UIButton btnPistols, btnSMGs, btnShotguns, btnRifles, btnSniperRifles;

        UISwitcher aim;
        UISwitcher tb;
        UISwitcher rcs;
        UISwitcher velocityPrediction;

        UISlider sliderFov;
        UISlider sliderAimMult;
        UISlider sliderShotsFired;

        UILabel labelSmooth;
        UILabel labelShotsFired;
    }
}
