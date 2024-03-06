using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Config
{
    public enum ESP_Box_Type : byte
    {
        DrawRectangle = 0,
        DrawRectangleEdges = 1,
        DrawRoundedRectangle = 2,

        FillRectangle = 3,
        FillRoundedRectangle = 4,

        OutlineFillRectangle = 5,
        OutlineRectangle = 6,
    }
}
