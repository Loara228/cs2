#pragma warning disable

using cs2.Config;
using cs2.GameOverlay.UI.Controls;
using cs2.Offsets;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormConfig : UIForm
    {
        public FormConfig(int x, int y) : base(x, y, "cs2.exe")
        {
            this.MinWidth = 320;
            InitializeComponents();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }

        private void InitializeComponents()
        {
            Add(new UILabel($"Configuration"));
            UIContainerInline inlineContainer = new UIContainerInline(0, HEADER_SIZE + 5);
            Add(inlineContainer);
            inlineContainer.Add(new UIButton("Load", Load) { Width = MinWidth / 2 - 5 * 3 });
            inlineContainer.Add(new UIButton("Save", Save) { Width = MinWidth / 2 - 5 * 3 });

            Add(new UIButton(@"updates (github.com)", new Action(() => Process.Start("explorer", "https://github.com/blyatArtem/cs2/blob/master/cs2/Content/Updates.md"))) { Width = MinWidth - 10 });

            Add(new UILabel($"Offsets"));
            Add(new UIButton(@"currentDir\generated", LoadOffsets) { Width = MinWidth - 10 });

            //var dumpTimeList = OffsetsLoader.DumpTime.Replace("//\n", "").Replace("//\n", "").Split('\n').ToList();
            //dumpTimeList.RemoveAt(dumpTimeList.Count - 1);
            //Add(offsetsInfo1 = new UILabel(dumpTimeList[0]) { TextColor = Brushes.UIButtonMouseOn, FontSize = 10, Margin = new(-5) });
            //Add(offsetsInfo2 = new UILabel(dumpTimeList[1]) { TextColor = Brushes.UIButtonMouseOn, FontSize = 10, Margin = new(-5) });

            Add(new UILine(this));

            Add(labelFps = new UILabel($"fps max: {Overlay.Current.Window.FPS}") {  FontSize = 10, Margin = new Margin(5, 5, -5, 0)});
            Add(sliderFps = new UISlider(Configuration.Current.FPS_Max - 30) { MaxValue = 60 });

            Add(labelThreadAim = new UILabel($"aim thread delay: {Configuration.Current.THR_DELAY_AIM}") { FontSize = 10, Margin = new Margin(5, 5, -5, 0) });
            Add(sliderThreadAim = new UISlider(Configuration.Current.THR_DELAY_AIM) { MaxValue = 50 });

            Add(labelThreadTb = new UILabel($"triggerbot thread delay: {Configuration.Current.THR_DELAY_TB}") { FontSize = 10, Margin = new Margin(5, 5, -5, 0) });
            Add(sliderThreadTb = new UISlider(Configuration.Current.THR_DELAY_TB) { MaxValue = 50 });

            Add(labelThreadBhop = new UILabel($"bhop thread delay: {Configuration.Current.THR_DELAY_BHOP}") { FontSize = 10, Margin = new Margin(5, 5, -5, 0) });
            Add(sliderThreadBhop = new UISlider(Configuration.Current.THR_DELAY_BHOP) { MaxValue = 50 });

            sliderFps.onValueChanged += (value) =>
            {
                Overlay.Current.Window.FPS = (int)value + 30;
                labelFps.Text = $"fps max: {Overlay.Current.Window.FPS}";
            };
            sliderThreadAim.onValueChanged += (value) =>
            {
                Configuration.Current.THR_DELAY_AIM = (int)value;
                labelThreadAim.Text = $"aim thread delay: {Configuration.Current.THR_DELAY_AIM}";
            }; sliderThreadTb.onValueChanged += (value) =>
            {
                Configuration.Current.THR_DELAY_TB = (int)value;
                labelThreadTb.Text = $"triggerbot thread delay: {Configuration.Current.THR_DELAY_TB}";
            }; sliderThreadBhop.onValueChanged += (value) =>
            {
                Configuration.Current.THR_DELAY_BHOP = (int)value;
                labelThreadBhop.Text = $"bhop thread delay: {Configuration.Current.THR_DELAY_BHOP}";
            };

            Width = Width;

            sliderFps.Width = Width - 25;
            sliderThreadAim.Width = Width - 25;
            sliderThreadBhop.Width = Width - 25;
            sliderThreadTb.Width = Width - 25;
        }

        private void LoadOffsets()
        {
            OffsetsLoader.Initialize(LoadType.FROM_DIR);
            var dumpTimeList = OffsetsLoader.DumpTime.Replace("//\n", "").Replace("//\n", "").Split('\n').ToList();
            dumpTimeList.RemoveAt(dumpTimeList.Count - 1);
            offsetsInfo1.Text = dumpTimeList[0];
            offsetsInfo2.Text = dumpTimeList[1];
        }

        private void Save()
        {
            Configuration.Save();
        }

        private void Load()
        {
            Configuration.Load();
            Overlay.Current.ApplyConfiguration();
            ApplyCfg();
        }

        private void ApplyCfg()
        {
            Overlay.Current.Window.FPS = Configuration.Current.FPS_Max;
            labelFps.Text = $"fps max: {Overlay.Current.Window.FPS}";
            //labelThreadAim.Text = $"aim thread delay: {Configuration.Current.THR_DELAY_AIM}";
            //labelThreadTb.Text = $"triggerbot thread delay: {Configuration.Current.THR_DELAY_TB}";
            //labelThreadBhop.Text = $"bhop thread delay: {Configuration.Current.THR_DELAY_BHOP}";
            sliderThreadBhop.Value = Configuration.Current.THR_DELAY_BHOP;
            sliderThreadTb.Value = Configuration.Current.THR_DELAY_TB;
            sliderThreadAim.Value = Configuration.Current.THR_DELAY_AIM;


        }

        private UILabel offsetsInfo1, offsetsInfo2;

        private UISlider sliderFps;
        private UILabel labelFps;

        private UISlider sliderThreadAim;
        private UILabel labelThreadAim;

        private UISlider sliderThreadBhop;
        private UILabel labelThreadBhop;

        private UISlider sliderThreadTb;
        private UILabel labelThreadTb;
    }
}
