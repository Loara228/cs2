using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CGameSceneNode : InterfaceBase
    {
        public CGameSceneNode() : base("")
        {
        }

        public nint m_bDormant = 0xE7; // bool

    }
}
