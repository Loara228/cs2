using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CPlayer_ObserverServices : InterfaceBase
    {
        public CPlayer_ObserverServices() : base("")
        {
        }

        public nint m_iObserverMode = 0x40; // uint8_t
        public nint m_hObserverTarget = 0x44; // CHandle<C_BaseEntity>
        public nint m_iObserverLastMode = 0x48; // ObserverMode_t
        public nint m_bForcedObserverMode = 0x4C; // bool
        public nint m_flObserverChaseDistance = 0x50; // float
        public nint m_flObserverChaseDistanceCalcTime = 0x54; // GameTime_t

    }
}
