using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_BaseEntity : InterfaceBase
    {
        public C_BaseEntity() : base("")
        {

        }

        public nint m_CBodyComponent = 0x30; // CBodyComponent*
        public nint m_NetworkTransmitComponent = 0x38; // CNetworkTransmitComponent
        public nint m_nLastThinkTick = 0x308; // GameTick_t
        public nint m_pGameSceneNode = 0x310; // CGameSceneNode*
        public nint m_pRenderComponent = 0x318; // CRenderComponent*
        public nint m_pCollision = 0x320; // CCollisionProperty*
        public nint m_iMaxHealth = 0x328; // int32_t
        public nint m_iHealth = 0x32C; // int32_t
        public nint m_lifeState = 0x330; // uint8_t
        public nint m_bTakesDamage = 0x331; // bool
        public nint m_nTakeDamageFlags = 0x334; // TakeDamageFlags_t
        public nint m_ubInterpolationFrame = 0x338; // uint8_t
        public nint m_hSceneObjectController = 0x33C; // CHandle<C_BaseEntity>
        public nint m_nNoInterpolationTick = 0x340; // int32_t
        public nint m_nVisibilityNoInterpolationTick = 0x344; // int32_t
        public nint m_flProxyRandomValue = 0x348; // float
        public nint m_iEFlags = 0x34C; // int32_t
        public nint m_nWaterType = 0x350; // uint8_t
        public nint m_bInterpolateEvenWithNoModel = 0x351; // bool
        public nint m_bPredictionEligible = 0x352; // bool
        public nint m_bApplyLayerMatchIDToModel = 0x353; // bool
        public nint m_tokLayerMatchID = 0x354; // CUtlStringToken
        public nint m_nSubclassID = 0x358; // CUtlStringToken
        public nint m_nSimulationTick = 0x368; // int32_t
        public nint m_iCurrentThinkContext = 0x36C; // int32_t
        public nint m_aThinkFunctions = 0x370; // CUtlVector<thinkfunc_t>
        public nint m_flAnimTime = 0x388; // float
        public nint m_flSimulationTime = 0x38C; // float
        public nint m_nSceneObjectOverrideFlags = 0x390; // uint8_t
        public nint m_bHasSuccessfullyInterpolated = 0x391; // bool
        public nint m_bHasAddedVarsToInterpolation = 0x392; // bool
        public nint m_bRenderEvenWhenNotSuccessfullyInterpolated = 0x393; // bool
        public nint m_nInterpolationLatchDirtyFlags = 0x394; // int32_t[2]
        public nint m_ListEntry = 0x39C; // uint16_t[11]
        public nint m_flCreateTime = 0x3B4; // GameTime_t
        public nint m_flSpeed = 0x3B8; // float
        public nint m_EntClientFlags = 0x3BC; // uint16_t
        public nint m_bClientSideRagdoll = 0x3BE; // bool
        public nint m_iTeamNum = 0x3BF; // uint8_t
        public nint m_spawnflags = 0x3C0; // uint32_t
        public nint m_nNextThinkTick = 0x3C4; // GameTick_t
        public nint m_fFlags = 0x3C8; // uint32_t
        public nint m_vecAbsVelocity = 0x3CC; // Vector
        public nint m_vecVelocity = 0x3D8; // CNetworkVelocityVector
        public nint m_vecBaseVelocity = 0x408; // Vector
        public nint m_hEffectEntity = 0x414; // CHandle<C_BaseEntity>
        public nint m_hOwnerEntity = 0x418; // CHandle<C_BaseEntity>
        public nint m_MoveCollide = 0x41C; // MoveCollide_t
        public nint m_MoveType = 0x41D; // MoveType_t
        public nint m_flWaterLevel = 0x420; // float
        public nint m_fEffects = 0x424; // uint32_t
        public nint m_hGroundEntity = 0x428; // CHandle<C_BaseEntity>
        public nint m_flFriction = 0x42C; // float
        public nint m_flElasticity = 0x430; // float
        public nint m_flGravityScale = 0x434; // float
        public nint m_flTimeScale = 0x438; // float
        public nint m_bSimulatedEveryTick = 0x43C; // bool
        public nint m_bAnimatedEveryTick = 0x43D; // bool
        public nint m_flNavIgnoreUntilTime = 0x440; // GameTime_t
        public nint m_hThink = 0x444; // uint16_t
        public nint m_fBBoxVisFlags = 0x450; // uint8_t
        public nint m_bPredictable = 0x451; // bool
        public nint m_bRenderWithViewModels = 0x452; // bool
        public nint m_nSplitUserPlayerPredictionSlot = 0x454; // CSplitScreenSlot
        public nint m_nFirstPredictableCommand = 0x458; // int32_t
        public nint m_nLastPredictableCommand = 0x45C; // int32_t
        public nint m_hOldMoveParent = 0x460; // CHandle<C_BaseEntity>
        public nint m_Particles = 0x468; // CParticleProperty
        public nint m_vecPredictedScriptFloats = 0x490; // CUtlVector<float>
        public nint m_vecPredictedScriptFloatIDs = 0x4A8; // CUtlVector<int32_t>
        public nint m_nNextScriptVarRecordID = 0x4D8; // int32_t
        public nint m_vecAngVelocity = 0x4E8; // QAngle
        public nint m_DataChangeEventRef = 0x4F4; // int32_t
        public nint m_dependencies = 0x4F8; // CUtlVector<CEntityHandle>
        public nint m_nCreationTick = 0x510; // int32_t
        public nint m_bAnimTimeChanged = 0x529; // bool
        public nint m_bSimulationTimeChanged = 0x52A; // bool
        public nint m_sUniqueHammerID = 0x538; // CUtlString
    }
}
