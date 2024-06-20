using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_EconItemView : InterfaceBase
    {
        public C_EconItemView() : base("")
        {

        }

        public nint m_iItemDefinitionIndex = 0x1BA; // uint16
    }
}
