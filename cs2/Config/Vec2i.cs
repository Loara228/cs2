using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Config
{
    [Serializable]
    public struct Vec2i
    {
        public Vec2i() { }

        public Vec2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x, y;
    }
}
