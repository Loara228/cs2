#pragma warning disable 

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
    internal class FormVisuals : UIForm
    {
        public FormVisuals(int x) : base(x, 0, "Visuals")
        {
            //this.Width = SIZE_X;
            this.Width = 500;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Add(new UILabel("Rectangle") { FontSize = 16, Margin = new Margin(10, 5, 5, 5) });
            Add(_switcherBox = new UISwitcher2("Enable", Configuration.Current.ESP_Boxes_Color, new((x) => Configuration.Current.ESP_Boxes = x)));
            Add(new UILabel("Stroke") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderBoxsStroke = new UISlider(Configuration.Current.ESP_Boxes_Stroke));
            _sliderBoxsStroke.onValueChanged += (x) => { Configuration.Current.ESP_Boxes_Stroke = x; };
            _sliderBoxsStroke.MaxValue = 4;
            Add(new UILine(this));
            // ------------------------------------------------------------------------------------------------------------
            Add(new UILabel("Skeleton") { FontSize = 16, Margin = new Margin(10, 5, 5, 5) });
            Add(_switcherSkeleton = new UISwitcher2("Enable", Configuration.Current.ESP_Skeleton_Color, new((x) => Configuration.Current.ESP_Skeleton = x)));
            Add(new UILabel("Stroke") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderBonesStroke = new UISlider(Configuration.Current.ESP_Bone_Stroke));
            _sliderBonesStroke.onValueChanged += (x) => { Configuration.Current.ESP_Bone_Stroke = x; };
            _sliderBonesStroke.MaxValue = 4;
            _sliderBonesStroke.Width = 130;
            Add(new UILine(this));
            // ------------------------------------------------------------------------------------------------------------
            Add(new UILabel("Weapon") { FontSize = 16, Margin = new Margin(10, 5, 5, 5) });
            Add(_switcherWeapon = new UISwitcher("Enable", new((x) => Configuration.Current.ESP_Weapon = x)));
            Add(_switcherAmmo = new UISwitcher("Ammo", new((x) => Configuration.Current.ESP_Ammo = x)));
            Add(new UILabel("Text size") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_sliderWeaponFontSize = new UISlider(Configuration.Current.ESP_Weapon_Font_Size));
            _sliderWeaponFontSize.onValueChanged += (x) => { Configuration.Current.ESP_Weapon_Font_Size = x; };
            _sliderWeaponFontSize.MaxValue = 20;
            _sliderWeaponFontSize.Width = 130;
            Add(_weaponColor = new UILabel2("Text color", Configuration.Current.ESP_Weapon_Color));
            Add(new UILine(this));
            // ------------------------------------------------------------------------------------------------------------
            Add(new UILabel("Info") { FontSize = 16, Margin = new Margin(10, 5, 5, 5) });
            Add(_switcherHealth = new UISwitcher("Health bar", new((x) => Configuration.Current.ESP_Health = x)));
            Add(_switcherFlash = new UISwitcher("Flash bar", new((x) => Configuration.Current.ESP_Flash = x)));
            //Add(_switcherAlerts = new UISwitcher("Scope", new((x) => Configuration.Current.ESP_Alerts = x)));

            Add(new UILabel("Misc"));
            Add(_worldEspWeapons = new UISwitcher2("World", Configuration.Current.ESP_World_Weapons_Color, new((x) => Configuration.Current.ESP_World_Weapons = x)));
            Add(_switcherCrosshair = new UISwitcher("Crosshair", new((x) => Configuration.Current.Misc_Crosshair = x)));
            Add(_switcherScoreboard = new UISwitcher("Scoreboard", new((x) => Configuration.Current.Misc_Scoreboard = x)));

            entityPreview = new UIEntityPreview();
            entityPreview.Attach(this);

            _weaponColor.colorMarginLeft = entityPreview.X - 10 - (int)Rect.Left;
            _weaponColor.ColorChanged += (color) => Configuration.Current.ESP_Weapon_Color = color;

            _switcherBox.colorMarginLeft = entityPreview.X - 10 - (int)Rect.Left;
            _switcherBox.ColorChanged += (color) => { Configuration.Current.ESP_Boxes_Color = color; };
            _switcherSkeleton.colorMarginLeft = entityPreview.X - 10 - (int)Rect.Left;
            _switcherSkeleton.ColorChanged += (color) => { Configuration.Current.ESP_Skeleton_Color = color; };
            _worldEspWeapons.colorMarginLeft = entityPreview.X - 10 - (int)Rect.Left;
            _worldEspWeapons.ColorChanged += (color) => { Configuration.Current.ESP_World_Weapons_Color = color; };

            _sliderBoxsStroke.Width = entityPreview.X - 20 - (int)Rect.Left;
            _sliderBonesStroke.Width = entityPreview.X - 20 - (int)Rect.Left;
            _sliderWeaponFontSize.Width = entityPreview.X - 20 - (int)Rect.Left;
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

        public override void ApplyConfig()
        {
            _switcherBox.Checked = Configuration.Current.ESP_Boxes;

            _switcherHealth.Checked = Configuration.Current.ESP_Health;
            _switcherFlash.Checked = Configuration.Current.ESP_Flash;
            //_switcherAlerts.Checked = Configuration.Current.ESP_Alerts;
            _switcherWeapon.Checked = Configuration.Current.ESP_Weapon;
            _switcherAmmo.Checked = Configuration.Current.ESP_Ammo;
            _switcherSkeleton.Checked = Configuration.Current.ESP_Skeleton;
            _worldEspWeapons.Checked = Configuration.Current.ESP_World_Weapons;

            _switcherCrosshair.Checked = Configuration.Current.Misc_Crosshair;
            _switcherScoreboard.Checked = Configuration.Current.Misc_Scoreboard;

            _sliderBoxsStroke.Value = Configuration.Current.ESP_Boxes_Stroke;
            _sliderBonesStroke.Value = Configuration.Current.ESP_Bone_Stroke;
            _sliderWeaponFontSize.Value = Configuration.Current.ESP_Weapon_Font_Size;

            _switcherBox.Color = Configuration.Current.ESP_Boxes_Color;
            _switcherSkeleton.Color = Configuration.Current.ESP_Skeleton_Color;
            _worldEspWeapons.Color = Configuration.Current.ESP_World_Weapons_Color;

            _weaponColor.Color = Configuration.Current.ESP_Weapon_Color;
        }

        private UIEntityPreview entityPreview = new UIEntityPreview();
        private UISwitcher2 _switcherBox;
        private UISwitcher _switcherHealth;
        private UISwitcher _switcherFlash;
        private UISwitcher _switcherAlerts;
        private UISwitcher _switcherWeapon;
        private UISwitcher _switcherAmmo;
        private UISwitcher2 _switcherSkeleton;
        private UISwitcher _switcherCrosshair;
        private UISwitcher _switcherScoreboard;
        private UISwitcher2 _worldEspWeapons;

        private UILabel2 _weaponColor;

        private UISlider _sliderBoxsStroke;
        private UISlider _sliderBonesStroke;
        private UISlider _sliderWeaponFontSize;

        private const int SIZE_X = 380, SIZE_Y = 600;
    }
}
