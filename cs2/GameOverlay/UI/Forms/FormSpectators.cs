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
    internal class FormSpectators : UIForm
    {
        public FormSpectators() : base(Configuration.Current.FormSpectatorsPos.x, Configuration.Current.FormSpectatorsPos.y, "Spectators")
        {
            this.GameForm = true;
            this.MinWidth = 200;
            this.MinHeight = 60;
            this.Width = 200;
            this.Height = 60;

            onMove += (int x, int y) => { Configuration.Current.FormSpectatorsPos = new Vec2i(X, Y); };
            Add(labelSpectators = new UILabel(Fonts.Consolas, Brushes.White, ""));
        }

        public override void Update()
        {
            if (!Configuration.Current.EnableSpectators)
                return;
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            if (!Configuration.Current.EnableSpectators)
                return;
            labelSpectators.Text = string.Join("\n", SpectatorList.Get());
            Width = labelSpectators.Width + labelSpectators.Margin.Left * 2;
            Height = HEADER_SIZE + labelSpectators.Height + labelSpectators.Margin.Top * 2;
            base.Draw(g);
        }

        public override void ApplyConfig()
        {
            Position = new Point(Configuration.Current.FormSpectatorsPos.x, Configuration.Current.FormSpectatorsPos.y);
            base.ApplyConfig();
        }

        private UILabel labelSpectators;
    }
}
