#pragma warning disable

using cs2.GameOverlay.UI.Controls;
using cs2.Offsets;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormConfig : UIForm
    {
        public FormConfig(int x) : base(x, 0, "cs2.exe")
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

            Add(new UILabel($"Offsets"));
            Add(new UIButton(@"currentDir\generated", LoadOffsets) { Width = MinWidth - 10 });

            var dumpTimeList = OffsetsLoader.DumpTime.Replace("/*\n", "").Replace("*/\n", "").Split('\n').ToList();
            dumpTimeList.RemoveAt(dumpTimeList.Count - 1);
            Add(offsetsInfo1 = new UILabel(dumpTimeList[0]) { TextColor = Brushes.UIButtonMouseOn, FontSize = 10, Margin = new(-5) });
            Add(offsetsInfo2 = new UILabel(dumpTimeList[1]) { TextColor = Brushes.UIButtonMouseOn, FontSize = 10, Margin = new(-5) });

            Width = Width;
        }

        private void LoadOffsets()
        {
            OffsetsLoader.Initialize(LoadType.FROM_DIR);
            var dumpTimeList = OffsetsLoader.DumpTime.Replace("/*\n", "").Replace("*/\n", "").Split('\n').ToList();
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
        }

        private UILabel offsetsInfo1, offsetsInfo2;
    }
}
