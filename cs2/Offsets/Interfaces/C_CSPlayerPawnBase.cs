using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_CSPlayerPawnBase : InterfaceBase
    {
        public C_CSPlayerPawnBase() : base("")
        {
        }

        public nint m_pPingServices = 0x12C0; // CCSPlayer_PingServices*
        public nint m_pViewModelServices = 0x12C8; // CPlayer_ViewModelServices*
        public nint m_fRenderingClipPlane = 0x12D8; // float[4]
        public nint m_nLastClipPlaneSetupFrame = 0x12E8; // int32_t
        public nint m_vecLastClipCameraPos = 0x12EC; // Vector
        public nint m_vecLastClipCameraForward = 0x12F8; // Vector
        public nint m_bClipHitStaticWorld = 0x1304; // bool
        public nint m_bCachedPlaneIsValid = 0x1305; // bool
        public nint m_pClippingWeapon = 0x1308; // C_CSWeaponBase*
        public nint m_previousPlayerState = 0x1310; // CSPlayerState
        public nint m_flLastCollisionCeiling = 0x1314; // float
        public nint m_flLastCollisionCeilingChangeTime = 0x1318; // float
        public nint m_grenadeParameterStashTime = 0x1338; // GameTime_t
        public nint m_bGrenadeParametersStashed = 0x133C; // bool
        public nint m_angStashedShootAngles = 0x1340; // QAngle
        public nint m_vecStashedGrenadeThrowPosition = 0x134C; // Vector
        public nint m_vecStashedVelocity = 0x1358; // Vector
        public nint m_angShootAngleHistory = 0x1364; // QAngle[2]
        public nint m_vecThrowPositionHistory = 0x137C; // Vector[2]
        public nint m_vecVelocityHistory = 0x1394; // Vector[2]
        public nint m_thirdPersonHeading = 0x13B0; // QAngle
        public nint m_flSlopeDropOffset = 0x13C8; // float
        public nint m_flSlopeDropHeight = 0x13D8; // float
        public nint m_vHeadConstraintOffset = 0x13E8; // Vector
        public nint m_bIsScoped = 0x1400; // bool
        public nint m_bIsWalking = 0x1401; // bool
        public nint m_bResumeZoom = 0x1402; // bool
        public nint m_iPlayerState = 0x1404; // CSPlayerState
        public nint m_bIsDefusing = 0x1408; // bool
        public nint m_bIsGrabbingHostage = 0x1409; // bool
        public nint m_iBlockingUseActionInProgress = 0x140C; // CSPlayerBlockingUseAction_t
        public nint m_bIsRescuing = 0x1410; // bool
        public nint m_fImmuneToGunGameDamageTime = 0x1414; // GameTime_t
        public nint m_fImmuneToGunGameDamageTimeLast = 0x1418; // GameTime_t
        public nint m_bGunGameImmunity = 0x141C; // bool
        public nint m_GunGameImmunityColor = 0x141D; // Color
        public nint m_bHasMovedSinceSpawn = 0x1421; // bool
        public nint m_fMolotovUseTime = 0x1424; // float
        public nint m_fMolotovDamageTime = 0x1428; // float
        public nint m_nWhichBombZone = 0x142C; // int32_t
        public nint m_bInNoDefuseArea = 0x1430; // bool
        public nint m_iThrowGrenadeCounter = 0x1434; // int32_t
        public nint m_bWaitForNoAttack = 0x1438; // bool
        public nint m_flGuardianTooFarDistFrac = 0x143C; // float
        public nint m_flDetectedByEnemySensorTime = 0x1440; // GameTime_t
        public nint m_flNextGuardianTooFarWarning = 0x1444; // float
        public nint m_bSuppressGuardianTooFarWarningAudio = 0x1448; // bool
        public nint m_bKilledByTaser = 0x1449; // bool
        public nint m_iMoveState = 0x144C; // int32_t
        public nint m_bCanMoveDuringFreezePeriod = 0x1450; // bool
        public nint m_flLowerBodyYawTarget = 0x1454; // float
        public nint m_bStrafing = 0x1458; // bool
        public nint m_flLastSpawnTimeIndex = 0x145C; // GameTime_t
        public nint m_flEmitSoundTime = 0x1460; // GameTime_t
        public nint m_iAddonBits = 0x1464; // int32_t
        public nint m_iPrimaryAddon = 0x1468; // int32_t
        public nint m_iSecondaryAddon = 0x146C; // int32_t
        public nint m_iProgressBarDuration = 0x1470; // int32_t
        public nint m_flProgressBarStartTime = 0x1474; // float
        public nint m_iDirection = 0x1478; // int32_t
        public nint m_iShotsFired = 0x147C; // int32_t
        public nint m_bNightVisionOn = 0x1480; // bool
        public nint m_bHasNightVision = 0x1481; // bool
        public nint m_flVelocityModifier = 0x1484; // float
        public nint m_flHitHeading = 0x1488; // float
        public nint m_nHitBodyPart = 0x148C; // int32_t
        public nint m_iStartAccount = 0x1490; // int32_t
        public nint m_vecIntroStartEyePosition = 0x1494; // Vector
        public nint m_vecIntroStartPlayerForward = 0x14A0; // Vector
        public nint m_flClientDeathTime = 0x14AC; // GameTime_t
        public nint m_flNightVisionAlpha = 0x14B0; // float
        public nint m_bScreenTearFrameCaptured = 0x14B4; // bool
        public nint m_flFlashBangTime = 0x14B8; // float
        public nint m_flFlashScreenshotAlpha = 0x14BC; // float
        public nint m_flFlashOverlayAlpha = 0x14C0; // float
        public nint m_bFlashBuildUp = 0x14C4; // bool
        public nint m_bFlashDspHasBeenCleared = 0x14C5; // bool
        public nint m_bFlashScreenshotHasBeenGrabbed = 0x14C6; // bool
        public nint m_flFlashMaxAlpha = 0x14C8; // float
        public nint m_flFlashDuration = 0x14CC; // float
        public nint m_lastStandingPos = 0x14D0; // Vector
        public nint m_vecLastMuzzleFlashPos = 0x14DC; // Vector
        public nint m_angLastMuzzleFlashAngle = 0x14E8; // QAngle
        public nint m_hMuzzleFlashShape = 0x14F4; // CHandle<C_BaseEntity>
        public nint m_iHealthBarRenderMaskIndex = 0x14F8; // int32_t
        public nint m_flHealthFadeValue = 0x14FC; // float
        public nint m_flHealthFadeAlpha = 0x1500; // float
        public nint m_nMyCollisionGroup = 0x1504; // int32_t
        public nint m_ignoreLadderJumpTime = 0x1508; // float
        public nint m_ladderSurpressionTimer = 0x1510; // CountdownTimer
        public nint m_lastLadderNormal = 0x1528; // Vector
        public nint m_lastLadderPos = 0x1534; // Vector
        public nint m_flDeathCCWeight = 0x1548; // float
        public nint m_bOldIsScoped = 0x154C; // bool
        public nint m_flPrevRoundEndTime = 0x1550; // float
        public nint m_flPrevMatchEndTime = 0x1554; // float
        public nint m_unCurrentEquipmentValue = 0x1558; // uint16_t
        public nint m_unRoundStartEquipmentValue = 0x155A; // uint16_t
        public nint m_unFreezetimeEndEquipmentValue = 0x155C; // uint16_t
        public nint m_vecThirdPersonViewPositionOverride = 0x1560; // Vector
        public nint m_nHeavyAssaultSuitCooldownRemaining = 0x156C; // int32_t
        public nint m_ArmorValue = 0x1570; // int32_t
        public nint m_angEyeAngles = 0x1578; // QAngle
        public nint m_fNextThinkPushAway = 0x1590; // float
        public nint m_bShouldAutobuyDMWeapons = 0x1594; // bool
        public nint m_bShouldAutobuyNow = 0x1595; // bool
        public nint m_bHud_MiniScoreHidden = 0x1596; // bool
        public nint m_bHud_RadarHidden = 0x1597; // bool
        public nint m_nLastKillerIndex = 0x1598; // CEntityIndex
        public nint m_nLastConcurrentKilled = 0x159C; // int32_t
        public nint m_nDeathCamMusic = 0x15A0; // int32_t
        public nint m_iIDEntIndex = 0x15A4; // CEntityIndex
        public nint m_delayTargetIDTimer = 0x15A8; // CountdownTimer
        public nint m_iTargetedWeaponEntIndex = 0x15C0; // CEntityIndex
        public nint m_iOldIDEntIndex = 0x15C4; // CEntityIndex
        public nint m_holdTargetIDTimer = 0x15C8; // CountdownTimer
        public nint m_flCurrentMusicStartTime = 0x15E4; // float
        public nint m_flMusicRoundStartTime = 0x15E8; // float
        public nint m_bDeferStartMusicOnWarmup = 0x15EC; // bool
        public nint m_cycleLatch = 0x15F0; // int32_t
        public nint m_serverIntendedCycle = 0x15F4; // float
        public nint m_vecPlayerPatchEconIndices = 0x15F8; // uint32_t[5]
        public nint m_bHideTargetID = 0x1614; // bool
        public nint m_flLastSmokeOverlayAlpha = 0x1618; // float
        public nint m_vLastSmokeOverlayColor = 0x161C; // Vector
        public nint m_nPlayerSmokedFx = 0x1628; // ParticleIndex_t
        public nint m_nPlayerInfernoBodyFx = 0x162C; // ParticleIndex_t
        public nint m_nPlayerInfernoFootFx = 0x1630; // ParticleIndex_t
        public nint m_flNextMagDropTime = 0x1634; // float
        public nint m_nLastMagDropAttachmentIndex = 0x1638; // int32_t
        public nint m_vecBulletHitModels = 0x1640; // CUtlVector<C_BulletHitModel*>
        public nint m_vecPickupModelSlerpers = 0x1658; // CUtlVector<C_PickUpModelSlerper*>
        public nint m_vecLastAliveLocalVelocity = 0x1670; // Vector
        public nint m_entitySpottedState = 0x1698; // EntitySpottedState_t
        public nint m_nSurvivalTeamNumber = 0x16B0; // int32_t
        public nint m_bGuardianShouldSprayCustomXMark = 0x16B4; // bool
        public nint m_bHasDeathInfo = 0x16B5; // bool
        public nint m_flDeathInfoTime = 0x16B8; // float
        public nint m_vecDeathInfoOrigin = 0x16BC; // Vector
        public nint m_bKilledByHeadshot = 0x16C8; // bool
        public nint m_hOriginalController = 0x16CC; // CHandle<CCSPlayerController>
    }
}
