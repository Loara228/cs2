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

        public nint m_nodeToWorld = 0x10; // CTransform
        public nint m_pOwner = 0x30; // CEntityInstance*
        public nint m_pParent = 0x38; // CGameSceneNode*
        public nint m_pChild = 0x40; // CGameSceneNode*
        public nint m_pNextSibling = 0x48; // CGameSceneNode*
        public nint m_hParent = 0x70; // CGameSceneNodeHandle
        public nint m_vecOrigin = 0x80; // CNetworkOriginCellCoordQuantizedVector
        public nint m_angRotation = 0xB8; // QAngle
        public nint m_flScale = 0xC4; // float
        public nint m_vecAbsOrigin = 0xC8; // Vector
        public nint m_angAbsRotation = 0xD4; // QAngle
        public nint m_flAbsScale = 0xE0; // float
        public nint m_nParentAttachmentOrBone = 0xE4; // int16_t
        public nint m_bDebugAbsOriginChanges = 0xE6; // bool
        public nint m_bDormant = 0xE7; // bool
        public nint m_bForceParentToBeNetworked = 0xE8; // bool
        public nint m_bDirtyHierarchy = 0x0; // bitfield:1
        public nint m_bDirtyBoneMergeInfo = 0x0; // bitfield:1
        public nint m_bNetworkedPositionChanged = 0x0; // bitfield:1
        public nint m_bNetworkedAnglesChanged = 0x0; // bitfield:1
        public nint m_bNetworkedScaleChanged = 0x0; // bitfield:1
        public nint m_bWillBeCallingPostDataUpdate = 0x0; // bitfield:1
        public nint m_bBoneMergeFlex = 0x0; // bitfield:1
        public nint m_nLatchAbsOrigin = 0x0; // bitfield:2
        public nint m_bDirtyBoneMergeBoneToRoot = 0x0; // bitfield:1
        public nint m_nHierarchicalDepth = 0xEB; // uint8_t
        public nint m_nHierarchyType = 0xEC; // uint8_t
        public nint m_nDoNotSetAnimTimeInInvalidatePhysicsCount = 0xED; // uint8_t
        public nint m_name = 0xF0; // CUtlStringToken
        public nint m_hierarchyAttachName = 0x130; // CUtlStringToken
        public nint m_flZOffset = 0x134; // float
        public nint m_vRenderOrigin = 0x138; // Vector

    }
}
