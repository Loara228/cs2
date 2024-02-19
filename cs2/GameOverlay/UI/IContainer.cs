using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal interface IContainer
    {
        public IReadOnlyCollection<UIControl> Controls
        {
            get; set;
        }

        public int Add(UIControl control);

        public void UpdateControlsPos();
    }
}
