using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class EntitySpottedState_t : InterfaceBase
    {
        public EntitySpottedState_t() : base("")
        {
        }

        public nint m_bSpotted = 0x8; // bool
        public nint m_bSpottedByMask = 0xC; // uint32_t[2]

    }
}
