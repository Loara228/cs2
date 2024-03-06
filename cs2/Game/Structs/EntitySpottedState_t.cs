using cs2.Game.Objects;
using cs2.Offsets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Structs
{

    [StructLayout(LayoutKind.Sequential)]
    internal unsafe struct EntitySpottedState_t
    {
        public bool m_bSpotted;
        //public fixed UInt32 m_bSpottedByMask[2];
        public ulong m_bSpottedByMask;
    }
}