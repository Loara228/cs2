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
        public ulong GetSpottedByMask()
        {
            //return m_bSpottedByMask[0] | ((ulong)m_bSpottedByMask[1] << 32);
            return m_bSpottedByMask;
        }


        public bool m_bSpotted;
        //public fixed UInt32 m_bSpottedByMask[2];
        public ulong m_bSpottedByMask;
    }
}

//bool SharedFunctions::spottedCheck(C_CSPlayerPawn C_CSPlayerPawn, LocalPlayer localPlayer) {
//    if (C_CSPlayerPawn.getEntitySpotted() & (1 << (localPlayer.playerPawn)) || localPlayer.getEntitySpotted() & (1 << (C_CSPlayerPawn.playerPawn))) return 1;
//    return 0;
//}
