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
        public int GetSpotted() => (int)m_bSpottedByMask;

        public bool m_bSpotted;
        //public fixed UInt32 m_bSpottedByMask[2];
        public ulong m_bSpottedByMask;
    }
}

//https://github.com/kristofhracza/tim_apple/blob/master/util/attributes.cpp#L212
//bool SharedFunctions::spottedCheck(C_CSPlayerPawn C_CSPlayerPawn, LocalPlayer localPlayer) {
//    if (C_CSPlayerPawn.getEntitySpotted() & (1 << (localPlayer.playerPawn)) || localPlayer.getEntitySpotted() & (1 << (C_CSPlayerPawn.playerPawn))) return 1;
//    return 0;
//}