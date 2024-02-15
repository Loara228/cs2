using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIPanel : UIContainer
    {
        public UIPanel(int x, int y) : base(x, y)
        {

        }

        public UIPanel(int x, int y, IBrush backgroundBrush) : base(x, y, backgroundBrush)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }
    }
}
