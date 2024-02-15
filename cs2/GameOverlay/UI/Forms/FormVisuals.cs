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
    internal class FormVisuals : UIForm
    {
        public FormVisuals() : base(1920 / 2 - SIZE_X / 2, 1080 / 2 - SIZE_Y / 2, "Visuals")
        {
            this.Width = SIZE_X;
            this.Height = SIZE_Y;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Add(new UILabel("UI settings:"));
            Add(new UISwitcher("Spectators", new((x) => Configuration.Current.EnableSpectators = x)));
            Add(new UISwitcher("Radar", new((x) => Configuration.Current.EnableRadar = x)));
            Add(new UILabel("Radar scale:"));
            UISlider sliderRadarScale;
            Add(sliderRadarScale = new UISlider(Configuration.Current.RadarScale));
            sliderRadarScale.onValueChanged += (x) => { Configuration.Current.RadarScale = x; };
            sliderRadarScale.Value = Configuration.Current.RadarScale;
            sliderRadarScale.MaxValue = 25;
            sliderRadarScale.Width = 130;

            Add(new UILabel("Radar enemy scale:"));
            UISlider sliderRadarEnemyRadius;
            Add(sliderRadarEnemyRadius = new UISlider(Configuration.Current.RadarEnemyRadius));
            sliderRadarEnemyRadius.onValueChanged += (x) => { Configuration.Current.RadarEnemyRadius = x; };
            sliderRadarEnemyRadius.Value = Configuration.Current.AimAssistMult;
            sliderRadarEnemyRadius.MaxValue = 8;
            sliderRadarEnemyRadius.Width = 130;

            Add(new UILabel("Enemy:"));
            Add(new UISwitcher("Box", new((x) => Configuration.Current.ESP_Boxes = x)));
            Add(new UISwitcher("Health", new((x) => Configuration.Current.ESP_Health = x)));
            Add(new UISwitcher("Flash", new((x) => Configuration.Current.ESP_Flash = x)));
            Add(new UISwitcher("Weapon", new((x) => Configuration.Current.ESP_Weapon = x)));
            Add(new UISwitcher("Ammo", new((x) => Configuration.Current.ESP_Ammo = x)));
            Add(new UISwitcher("Alerts", new((x) => Configuration.Current.ESP_Alerts = x)));
            Add(new UISwitcher("Skeleton", new((x) => Configuration.Current.ESP_Skeleton = x)));

            Add(new UILabel("Misc:"));
            Add(new UISwitcher("Crosshair", new((x) => Configuration.Current.Misc_Crosshair = x)));
            Add(new UISwitcher("Scoreboard", new((x) => Configuration.Current.Misc_Scoreboard = x)));

            entityPreview = new UIEntityPreview();
            entityPreview.Attach(this);
        }

        public override void Update()
        {
            base.Update();
            entityPreview.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (Overlay.drawUI)
                entityPreview.Draw(g);
        }

        private UIEntityPreview entityPreview = new UIEntityPreview();

        private const int SIZE_X = 380, SIZE_Y = 600;
    }
}
