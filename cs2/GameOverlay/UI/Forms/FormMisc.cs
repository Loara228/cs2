using cs2.Config;
using cs2.GameOverlay.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormMisc : UIForm
    {
        public FormMisc(int x) : base(x, 0, "Misc")
        {
            this.Width = 200;

            Add(new UILabel("Radar") { FontSize = 16, Margin = new Margin(10, 5, 5, 5) });
            Add(_switcherRadar = new UISwitcher("Enable", new((x) => Configuration.Current.EnableRadar = x)));
            Add(new UILabel("Scale") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_radarScale = new UISlider(Configuration.Current.RadarScale));
            _radarScale.onValueChanged += (x) => { Configuration.Current.RadarScale = x; };
            _radarScale.Value = Configuration.Current.RadarScale;
            _radarScale.MaxValue = 25;
            _radarScale.Width = Width - 20;
            Add(new UILabel("Enemy scale") { FontSize = 12, Margin = new Margin(5, 0, -5, 5) });
            Add(_radarEnemyScale = new UISlider(Configuration.Current.RadarEnemyRadius));
            _radarEnemyScale.onValueChanged += (x) => { Configuration.Current.RadarEnemyRadius = x; };
            _radarEnemyScale.Value = Configuration.Current.RadarEnemyRadius;
            _radarEnemyScale.MaxValue = 8;
            _radarEnemyScale.Width = Width - 20;
            Add(new UILine(this));
            // ------------------------------------------------------------------------------------------------------------
            Add(_switcherSpectators = new UISwitcher("Spectators", new((x) => Configuration.Current.EnableSpectators = x)));
            Add(_hitMarker = new UISwitcher("Hit Marker", new((x) => Configuration.Current.HitMarker = x)));
            Add(_bhop = new UISwitcher("Bhop", new((x) => Configuration.Current.Bhop = x)));
            Add(new UILine(this));
            // ------------------------------------------------------------------------------------------------------------
            Add(_switcherDM = new UISwitcher("DM Mode", new((x) => Configuration.Current.DM_Mode_Enabled = x)));
            Add(new UILine(this));
        }

        public override void ApplyConfig()
        {
            _switcherRadar.Checked = Configuration.Current.EnableRadar;
            _switcherSpectators.Checked = Configuration.Current.EnableSpectators;
            _switcherDM.Checked = Configuration.Current.DM_Mode_Enabled;
            _hitMarker.Checked = Configuration.Current.HitMarker;
            _bhop.Checked = Configuration.Current.Bhop;

            _radarScale.Value = Configuration.Current.RadarScale;
            _radarEnemyScale.Value = Configuration.Current.RadarEnemyRadius;
        }

        private UISwitcher _switcherRadar;
        private UISwitcher _switcherSpectators;
        private UISwitcher _switcherDM;
        private UISwitcher _hitMarker;
        private UISwitcher _bhop;

        private UISlider _radarScale;
        private UISlider _radarEnemyScale;
    }
}
