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

        public nint m_pBulletServices = 0x1718; // CCSPlayer_BulletServices*
        public nint m_pHostageServices = 0x1720; // CCSPlayer_HostageServices*
        public nint m_pBuyServices = 0x1728; // CCSPlayer_BuyServices*
        public nint m_pGlowServices = 0x1730; // CCSPlayer_GlowServices*
        public nint m_pActionTrackingServices = 0x1738; // CCSPlayer_ActionTrackingServices*
        public nint m_flHealthShotBoostExpirationTime = 0x1740; // GameTime_t
        public nint m_flLastFiredWeaponTime = 0x1744; // GameTime_t
        public nint m_bHasFemaleVoice = 0x1748; // bool
        public nint m_flLandseconds = 0x174C; // float
        public nint m_flOldFallVelocity = 0x1750; // float
        public nint m_szLastPlaceName = 0x1754; // char[18]
        public nint m_bPrevDefuser = 0x1766; // bool
        public nint m_bPrevHelmet = 0x1767; // bool
        public nint m_nPrevArmorVal = 0x1768; // int32_t
        public nint m_nPrevGrenadeAmmoCount = 0x176C; // int32_t
        public nint m_unPreviousWeaponHash = 0x1770; // uint32_t
        public nint m_unWeaponHash = 0x1774; // uint32_t
        public nint m_bInBuyZone = 0x1778; // bool
        public nint m_bPreviouslyInBuyZone = 0x1779; // bool
        public nint m_aimPunchAngle = 0x177C; // QAngle
        public nint m_aimPunchAngleVel = 0x1788; // QAngle
        public nint m_aimPunchTickBase = 0x1794; // int32_t
        public nint m_aimPunchTickFraction = 0x1798; // float
        public nint m_aimPunchCache = 0x17A0; // CUtlVector<QAngle>
        public nint m_bInLanding = 0x17C0; // bool
        public nint m_flLandingTime = 0x17C4; // float
        public nint m_bInHostageRescueZone = 0x17C8; // bool
        public nint m_bInBombZone = 0x17C9; // bool
        public nint m_bIsBuyMenuOpen = 0x17CA; // bool
        public nint m_flTimeOfLastInjury = 0x17CC; // GameTime_t
        public nint m_flNextSprayDecalTime = 0x17D0; // GameTime_t
        public nint m_iRetakesOffering = 0x18E8; // int32_t
        public nint m_iRetakesOfferingCard = 0x18EC; // int32_t
        public nint m_bRetakesHasDefuseKit = 0x18F0; // bool
        public nint m_bRetakesMVPLastRound = 0x18F1; // bool
        public nint m_iRetakesMVPBoostItem = 0x18F4; // int32_t
        public nint m_RetakesMVPBoostExtraUtility = 0x18F8; // loadout_slot_t
        public nint m_bNeedToReApplyGloves = 0x1918; // bool
        public nint m_EconGloves = 0x1920; // C_EconItemView
        public nint m_nEconGlovesChanged = 0x1D68; // uint8_t
        public nint m_bMustSyncRagdollState = 0x1D69; // bool
        public nint m_nRagdollDamageBone = 0x1D6C; // int32_t
        public nint m_vRagdollDamageForce = 0x1D70; // Vector
        public nint m_vRagdollDamagePosition = 0x1D7C; // Vector
        public nint m_szRagdollDamageWeaponName = 0x1D88; // char[64]
        public nint m_bRagdollDamageHeadshot = 0x1DC8; // bool
        public nint m_vRagdollServerOrigin = 0x1DCC; // Vector
        public nint m_bLastHeadBoneTransformIsValid = 0x23E0; // bool
        public nint m_lastLandTime = 0x23E4; // GameTime_t
        public nint m_bOnGroundLastTick = 0x23E8; // bool
        public nint m_qDeathEyeAngles = 0x2404; // QAngle
        public nint m_bSkipOneHeadConstraintUpdate = 0x2410; // bool
    }
}
