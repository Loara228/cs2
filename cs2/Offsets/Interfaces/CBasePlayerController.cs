using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CBasePlayerController : InterfaceBase
    {
        public CBasePlayerController() : base("")
        {
        }

        public nint m_nFinalPredictedTick = 0x548; // int32_t
        public nint m_CommandContext = 0x550; // C_CommandContext
        public nint m_nInButtonsWhichAreToggles = 0x600; // uint64_t
        public nint m_nTickBase = 0x608; // uint32_t
        public nint m_hPawn = 0x60C; // CHandle<C_BasePlayerPawn>
        public nint m_hPredictedPawn = 0x610; // CHandle<C_BasePlayerPawn>
        public nint m_nSplitScreenSlot = 0x614; // CSplitScreenSlot
        public nint m_hSplitOwner = 0x618; // CHandle<CBasePlayerController>
        public nint m_hSplitScreenPlayers = 0x620; // CUtlVector<CHandle<CBasePlayerController>>
        public nint m_bIsHLTV = 0x638; // bool
        public nint m_iConnected = 0x63C; // PlayerConnectedState
        public nint m_iszPlayerName = 0x640; // char[128]
        public nint m_steamID = 0x6C8; // uint64_t
        public nint m_bIsLocalPlayerController = 0x6D0; // bool
        public nint m_iDesiredFOV = 0x6D4; // uint32_t
    }
}
