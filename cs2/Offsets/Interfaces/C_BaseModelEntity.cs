using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_BaseModelEntity : InterfaceBase
    {
        public C_BaseModelEntity() : base("")
        {

        }

        public nint m_CHitboxComponent = 0xA28; // CHitboxComponent
        public nint m_vecViewOffset = 0xC58; // CNetworkViewOffsetVector
    }
}
