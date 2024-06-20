using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CSkeletonInstance : InterfaceBase
    {
        public CSkeletonInstance() : base("")
        {

        }

        public nint m_modelState = 0x170; // CModelState
    }
}
