using cs2.Config;
using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormRadar : UIForm
    {
        public FormRadar() : base(Configuration.Current.FormRadarPos.x, Configuration.Current.FormRadarPos.y, "Radar")
        {
            this.MinWidth = 100;
            this.MinHeight = 100 + UIForm.HEADER_SIZE;    
            this.Width = 280;
            this.Height = 280 + HEADER_SIZE;
            this.Resizable = true;

            onMove += (int x, int y) => { Configuration.Current.FormRadarPos = new Vec2i(X, Y); };
            onResizing += (int w, int h) => { Configuration.Current.FormRadarSize = new Vec2i(Width, Height); };
        }

        public override void Update()
        {
            if (!Configuration.Current.EnableRadar)
                return;
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            if (!Configuration.Current.EnableRadar)
                return;
            base.Draw(g);
            Radar.Draw(g, RadarBounds);
        }

        public override void ApplyConfig()
        {
            Position = new Point(Configuration.Current.FormRadarPos.x, Configuration.Current.FormRadarPos.y);
            Width = Configuration.Current.FormRadarSize.x;
            Height = Configuration.Current.FormRadarSize.y;
            base.ApplyConfig();
        }

        public Rectangle RadarBounds
        {
            get => new Rectangle(Rect.Left, Rect.Top + HEADER_SIZE, Rect.Right, Rect.Bottom);
        }
    }
}
