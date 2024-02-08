using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CCSPlayerController_InGameMoneyServices : InterfaceBase
    {
        public CCSPlayerController_InGameMoneyServices() : base("")
        {
        }

        public nint m_iAccount = 0x40; // int32_t
        public nint m_iStartAccount = 0x44; // int32_t
        public nint m_iTotalCashSpent = 0x48; // int32_t
        public nint m_iCashSpentThisRound = 0x4C; // int32_t
        public nint m_nPreviousAccount = 0x50; // int32_t

    }
}
