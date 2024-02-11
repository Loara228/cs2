using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Structs
{
    internal enum Bone : sbyte
    {
        UNKNOWN = -1,
        head = 6,
        neck_0 = 5,
        spine_1 = 4,
        spine_2 = 2,
        pelvis = 0,
        arm_upper_L = 8,
        arm_lower_L = 9,
        hand_L = 10,
        arm_upper_R = 13,
        arm_lower_R = 14,
        hand_R = 15,
        leg_upper_L = 22,
        leg_lower_L = 23,
        ankle_L = 24,
        leg_upper_R = 25,
        leg_lower_R = 26,
        ankle_R = 27,
    }
}
