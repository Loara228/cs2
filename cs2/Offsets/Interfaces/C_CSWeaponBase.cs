using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_CSWeaponBase : InterfaceBase
    {
        public C_CSWeaponBase() : base("")
        {

        }

        public nint m_flFireSequenceStartTime = 0x162C; // float
        public nint m_nFireSequenceStartTimeChange = 0x1630; // int32_t
        public nint m_nFireSequenceStartTimeAck = 0x1634; // int32_t
        public nint m_ePlayerFireEvent = 0x1638; // PlayerAnimEvent_t
        public nint m_ePlayerFireEventAttackType = 0x163C; // WeaponAttackType_t
        public nint m_seqIdle = 0x1640; // HSequence
        public nint m_seqFirePrimary = 0x1644; // HSequence
        public nint m_seqFireSecondary = 0x1648; // HSequence
        public nint m_thirdPersonFireSequences = 0x1650; // CUtlVector<HSequence>
        public nint m_hCurrentThirdPersonSequence = 0x1668; // HSequence
        public nint m_nSilencerBoneIndex = 0x166C; // int32_t
        public nint m_thirdPersonSequences = 0x1670; // HSequence[7]
        public nint m_ClientPreviousWeaponState = 0x16A8; // CSWeaponState_t
        public nint m_iState = 0x16AC; // CSWeaponState_t
        public nint m_flCrosshairDistance = 0x16B0; // float
        public nint m_iAmmoLastCheck = 0x16B4; // int32_t
        public nint m_iAlpha = 0x16B8; // int32_t
        public nint m_iScopeTextureID = 0x16BC; // int32_t
        public nint m_iCrosshairTextureID = 0x16C0; // int32_t
        public nint m_flGunAccuracyPosition = 0x16C4; // float
        public nint m_nLastEmptySoundCmdNum = 0x16C8; // int32_t
        public nint m_nViewModelIndex = 0x16CC; // uint32_t
        public nint m_bReloadsWithClips = 0x16D0; // bool
        public nint m_flTimeWeaponIdle = 0x16D4; // GameTime_t
        public nint m_bFireOnEmpty = 0x16D8; // bool
        public nint m_OnPlayerPickup = 0x16E0; // CEntityIOOutput
        public nint m_weaponMode = 0x1708; // CSWeaponMode
        public nint m_flTurningInaccuracyDelta = 0x170C; // float
        public nint m_vecTurningInaccuracyEyeDirLast = 0x1710; // Vector
        public nint m_flTurningInaccuracy = 0x171C; // float
        public nint m_fAccuracyPenalty = 0x1720; // float
        public nint m_flLastAccuracyUpdateTime = 0x1724; // GameTime_t
        public nint m_fAccuracySmoothedForZoom = 0x1728; // float
        public nint m_fScopeZoomEndTime = 0x172C; // GameTime_t
        public nint m_iRecoilIndex = 0x1730; // int32_t
        public nint m_flRecoilIndex = 0x1734; // float
        public nint m_bBurstMode = 0x1738; // bool
        public nint m_nPostponeFireReadyTicks = 0x173C; // GameTick_t
        public nint m_flPostponeFireReadyFrac = 0x1740; // float
        public nint m_bInReload = 0x1744; // bool
        public nint m_bReloadVisuallyComplete = 0x1745; // bool
        public nint m_flDroppedAtTime = 0x1748; // GameTime_t
        public nint m_bIsHauledBack = 0x174C; // bool
        public nint m_bSilencerOn = 0x174D; // bool
        public nint m_flTimeSilencerSwitchComplete = 0x1750; // GameTime_t
        public nint m_iOriginalTeamNumber = 0x1754; // int32_t
        public nint m_flNextAttackRenderTimeOffset = 0x1758; // float
        public nint m_bVisualsDataSet = 0x17E0; // bool
        public nint m_bOldFirstPersonSpectatedState = 0x17E1; // bool
        public nint m_hOurPing = 0x17E4; // CHandle<C_BaseEntity>
        public nint m_nOurPingIndex = 0x17E8; // CEntityIndex
        public nint m_vecOurPingPos = 0x17EC; // Vector
        public nint m_bGlowForPing = 0x17F8; // bool
        public nint m_bUIWeapon = 0x17F9; // bool
        public nint m_hPrevOwner = 0x1808; // CHandle<C_CSPlayerPawn>
        public nint m_nDropTick = 0x180C; // GameTick_t
        public nint m_donated = 0x182C; // bool
        public nint m_fLastShotTime = 0x1830; // GameTime_t
        public nint m_bWasOwnedByCT = 0x1834; // bool
        public nint m_bWasOwnedByTerrorist = 0x1835; // bool
        public nint m_gunHeat = 0x1838; // float
        public nint m_smokeAttachments = 0x183C; // uint32_t
        public nint m_lastSmokeTime = 0x1840; // GameTime_t
        public nint m_flNextClientFireBulletTime = 0x1844; // float
        public nint m_flNextClientFireBulletTime_Repredict = 0x1848; // float
        public nint m_IronSightController = 0x1920; // C_IronSightController
        public nint m_iIronSightMode = 0x19D0; // int32_t
        public nint m_flLastLOSTraceFailureTime = 0x19E0; // GameTime_t
        public nint m_iNumEmptyAttacks = 0x19E4; // int32_t
        public nint m_flLastMagDropRequestTime = 0x1A60; // GameTime_t
        public nint m_flWatTickOffset = 0x1A64; // float
    }
}
