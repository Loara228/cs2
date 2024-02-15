using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal struct Margin
    {
        public Margin(int offset)
        {
            Left = offset;
            Top = offset;
            Bottom = offset;
        }

        public Margin(int left, int top, int bottom)
        {
            Left = left;
            Top = top;
            Bottom = bottom;
        }

        public int Left, Top, Bottom;
    }
}
