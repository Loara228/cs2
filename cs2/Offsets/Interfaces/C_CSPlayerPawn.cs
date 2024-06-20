using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_CSPlayerPawn : InterfaceBase
    {
        public C_CSPlayerPawn() : base("")
        {

        }

        public nint m_pBulletServices = 0x1468; // CCSPlayer_BulletServices*
        public nint m_pHostageServices = 0x1470; // CCSPlayer_HostageServices*
        public nint m_pBuyServices = 0x1478; // CCSPlayer_BuyServices*
        public nint m_pGlowServices = 0x1480; // CCSPlayer_GlowServices*
        public nint m_pActionTrackingServices = 0x1488; // CCSPlayer_ActionTrackingServices*
        public nint m_flHealthShotBoostExpirationTime = 0x1490; // GameTime_t
        public nint m_flLastFiredWeaponTime = 0x1494; // GameTime_t
        public nint m_bHasFemaleVoice = 0x1498; // bool
        public nint m_flLandingTimeSeconds = 0x149C; // float32
        public nint m_flOldFallVelocity = 0x14A0; // float32
        public nint m_szLastPlaceName = 0x14A4; // char[18]
        public nint m_bPrevDefuser = 0x14B6; // bool
        public nint m_bPrevHelmet = 0x14B7; // bool
        public nint m_nPrevArmorVal = 0x14B8; // int32
        public nint m_nPrevGrenadeAmmoCount = 0x14BC; // int32
        public nint m_unPreviousWeaponHash = 0x14C0; // uint32
        public nint m_unWeaponHash = 0x14C4; // uint32
        public nint m_bInBuyZone = 0x14C8; // bool
        public nint m_bPreviouslyInBuyZone = 0x14C9; // bool
        public nint m_aimPunchAngle = 0x14CC; // QAngle
        public nint m_aimPunchAngleVel = 0x14D8; // QAngle
        public nint m_aimPunchTickBase = 0x14E4; // int32
        public nint m_aimPunchTickFraction = 0x14E8; // float32
        public nint m_aimPunchCache = 0x14F0; // CUtlVector<QAngle>
        public nint m_bInLanding = 0x1510; // bool
        public nint m_flLandingStartTime = 0x1514; // float32
        public nint m_bInHostageRescueZone = 0x1518; // bool
        public nint m_bInBombZone = 0x1519; // bool
        public nint m_bIsBuyMenuOpen = 0x151A; // bool
        public nint m_flTimeOfLastInjury = 0x151C; // GameTime_t
        public nint m_flNextSprayDecalTime = 0x1520; // GameTime_t
        public nint m_iRetakesOffering = 0x1640; // int32
        public nint m_iRetakesOfferingCard = 0x1644; // int32
        public nint m_bRetakesHasDefuseKit = 0x1648; // bool
        public nint m_bRetakesMVPLastRound = 0x1649; // bool
        public nint m_iRetakesMVPBoostItem = 0x164C; // int32
        public nint m_RetakesMVPBoostExtraUtility = 0x1650; // loadout_slot_t
        public nint m_bNeedToReApplyGloves = 0x1670; // bool
        public nint m_EconGloves = 0x1678; // C_EconItemView
        public nint m_nEconGlovesChanged = 0x1AC0; // uint8
        public nint m_bMustSyncRagdollState = 0x1AC1; // bool
        public nint m_nRagdollDamageBone = 0x1AC4; // int32
        public nint m_vRagdollDamageForce = 0x1AC8; // Vector
        public nint m_vRagdollDamagePosition = 0x1AD4; // Vector
        public nint m_szRagdollDamageWeaponName = 0x1AE0; // char[64]
        public nint m_bRagdollDamageHeadshot = 0x1B20; // bool
        public nint m_vRagdollServerOrigin = 0x1B24; // Vector
        public nint m_bLastHeadBoneTransformIsValid = 0x2138; // bool
        public nint m_lastLandTime = 0x213C; // GameTime_t
        public nint m_bOnGroundLastTick = 0x2140; // bool
        public nint m_qDeathEyeAngles = 0x215C; // QAngle
        public nint m_bSkipOneHeadConstraintUpdate = 0x2168; // bool
        public nint m_bLeftHanded = 0x2169; // bool
        public nint m_fSwitchedHandednessTime = 0x216C; // GameTime_t
        public nint m_flViewmodelOffsetX = 0x2170; // float32
        public nint m_flViewmodelOffsetY = 0x2174; // float32
        public nint m_flViewmodelOffsetZ = 0x2178; // float32
        public nint m_flViewmodelFOV = 0x217C; // float32
        public nint m_vecPlayerPatchEconIndices = 0x2180; // uint32[5]
        public nint m_GunGameImmunityColor = 0x21B8; // Color
        public nint m_vecBulletHitModels = 0x2208; // CUtlVector<C_BulletHitModel*>
        public nint m_bIsWalking = 0x2220; // bool
        public nint m_thirdPersonHeading = 0x2228; // QAngle
        public nint m_flSlopeDropOffset = 0x2240; // float32
        public nint m_flSlopeDropHeight = 0x2250; // float32
        public nint m_vHeadConstraintOffset = 0x2260; // Vector
        public nint m_entitySpottedState = 0x2278; // EntitySpottedState_t
        public nint m_bIsScoped = 0x2290; // bool
        public nint m_bResumeZoom = 0x2291; // bool
        public nint m_bIsDefusing = 0x2292; // bool
        public nint m_bIsGrabbingHostage = 0x2293; // bool
        public nint m_iBlockingUseActionInProgress = 0x2294; // CSPlayerBlockingUseAction_t
        public nint m_flEmitSoundTime = 0x2298; // GameTime_t
        public nint m_bInNoDefuseArea = 0x229C; // bool
        public nint m_nWhichBombZone = 0x22A0; // int32
        public nint m_iShotsFired = 0x22A4; // int32
        public nint m_flVelocityModifier = 0x22A8; // float32
        public nint m_flHitHeading = 0x22AC; // float32
        public nint m_nHitBodyPart = 0x22B0; // int32
        public nint m_bWaitForNoAttack = 0x22B4; // bool
        public nint m_ignoreLadderJumpTime = 0x22B8; // float32
        public nint m_bKilledByHeadshot = 0x22BD; // bool
        public nint m_ArmorValue = 0x22C0; // int32
        public nint m_unCurrentEquipmentValue = 0x22C4; // uint16
        public nint m_unRoundStartEquipmentValue = 0x22C6; // uint16
        public nint m_unFreezetimeEndEquipmentValue = 0x22C8; // uint16
        public nint m_nLastKillerIndex = 0x22CC; // CEntityIndex
        public nint m_bOldIsScoped = 0x22D0; // bool
        public nint m_bHasDeathInfo = 0x22D1; // bool
        public nint m_flDeathInfoTime = 0x22D4; // float32
        public nint m_vecDeathInfoOrigin = 0x22D8; // Vector
        public nint m_grenadeParameterStashTime = 0x22E4; // GameTime_t
        public nint m_bGrenadeParametersStashed = 0x22E8; // bool
        public nint m_angStashedShootAngles = 0x22EC; // QAngle
        public nint m_vecStashedGrenadeThrowPosition = 0x22F8; // Vector
        public nint m_vecStashedVelocity = 0x2304; // Vector
        public nint m_angShootAngleHistory = 0x2310; // QAngle[2]
        public nint m_vecThrowPositionHistory = 0x2328; // Vector[2]
        public nint m_vecVelocityHistory = 0x2340; // Vector[2]
    }
}
