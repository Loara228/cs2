using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_BasePlayerWeapon : InterfaceBase
    {
        public C_BasePlayerWeapon() : base("")
        {

        }

        public nint m_nNextPrimaryAttackTick = 0x15B8; // GameTick_t
        public nint m_flNextPrimaryAttackTickRatio = 0x15BC; // float
        public nint m_nNextSecondaryAttackTick = 0x15C0; // GameTick_t
        public nint m_flNextSecondaryAttackTickRatio = 0x15C4; // float
        public nint m_iClip1 = 0x15C8; // int32_t
        public nint m_iClip2 = 0x15CC; // int32_t
        public nint m_pReserveAmmo = 0x15D0; // int32_t[2]
    }
}
