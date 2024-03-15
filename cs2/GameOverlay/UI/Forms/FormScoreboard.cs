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
    internal class FormScoreboard : UIForm
    {
        public FormScoreboard() : base(Configuration.Current.FormScoreboardPos.x, Configuration.Current.FormScoreboardPos.y, "scoreboard")
        {
            MinWidth = 300;
            MinHeight = 100;

            Width = 300;
            Height = 100;
            Resizable = true;

            Current = this;

            onMove += (int x, int y) => { Configuration.Current.FormScoreboardPos = new Vec2i(X, Y); };
        }

        public override void ApplyConfig()
        {
            Position = new Point(Configuration.Current.FormScoreboardPos.x, Configuration.Current.FormScoreboardPos.y);
            base.ApplyConfig();
        }

        public override void Update()
        {
            if (!Configuration.Current.Misc_Scoreboard)
                return;
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            if (!Configuration.Current.Misc_Scoreboard)
                return;
            base.Draw(g);
            Scoreboard.Draw(g, new Rectangle(Rect.Left, Rect.Top + HEADER_SIZE, Rect.Right, Rect.Bottom), out int h);
            if (h != 0)
            {
                Height = h + HEADER_SIZE;
            }

        }

        public static FormScoreboard Current = null!;
    }
}
