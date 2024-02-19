using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UILine : UIControl
    {
        public UILine(UIForm owner)
        {
            DrawBackground = true;
            BrushBackground = Brushes.UIActiveColorThird;
            Margin = new Margin(1, 5, 5, 1);
            Width = owner.Width - 2;
            MinHeight = 1;
            Height = 4;
        }
    }
}
