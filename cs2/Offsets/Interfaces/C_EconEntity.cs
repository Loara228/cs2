using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_EconEntity : InterfaceBase
    {
        public C_EconEntity() : base("")
        {

        }

        public nint m_flFlexDelayTime = 0x1078; // float32
        public nint m_flFlexDelayedWeight = 0x1080; // float32*
        public nint m_bAttributesInitialized = 0x1088; // bool
        public nint m_AttributeManager = 0x1090; // C_AttributeContainer
        public nint m_OriginalOwnerXuidLow = 0x1538; // uint32
        public nint m_OriginalOwnerXuidHigh = 0x153C; // uint32
        public nint m_nFallbackPaintKit = 0x1540; // int32
        public nint m_nFallbackSeed = 0x1544; // int32
        public nint m_flFallbackWear = 0x1548; // float32
        public nint m_nFallbackStatTrak = 0x154C; // int32
        public nint m_bClientside = 0x1550; // bool
        public nint m_bParticleSystemsCreated = 0x1551; // bool
        public nint m_vecAttachedParticles = 0x1558; // CUtlVector<int32>
        public nint m_hViewmodelAttachment = 0x1570; // CHandle<CBaseAnimGraph>
        public nint m_iOldTeam = 0x1574; // int32
        public nint m_bAttachmentDirty = 0x1578; // bool
        public nint m_nUnloadedModelIndex = 0x157C; // int32
        public nint m_iNumOwnerValidationRetries = 0x1580; // int32
        public nint m_hOldProvidee = 0x1590; // CHandle<C_BaseEntity>
        public nint m_vecAttachedModels = 0x1598; // CUtlVector<C_EconEntity::AttachedModelData_t>
    }
}
