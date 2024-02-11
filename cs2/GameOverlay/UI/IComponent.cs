using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal interface IComponent
    {
        public abstract void Update();

        public abstract void Draw(Graphics g);

    }
}
