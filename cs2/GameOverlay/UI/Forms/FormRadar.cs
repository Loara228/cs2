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
            this.GameForm = true;
            this.MinWidth = 300;
            this.MinHeight = 60;    
            this.Width = 280;
            this.Height = 280 + HEADER_SIZE;
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

        public Rectangle RadarBounds
        {
            get => new Rectangle(Rect.Left, Rect.Top + HEADER_SIZE, Rect.Right, Rect.Bottom);
        }
    }
}
