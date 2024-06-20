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

        public nint m_pPingServices = 0x12B0; // CCSPlayer_PingServices*
        public nint m_pViewModelServices = 0x12B8; // CPlayer_ViewModelServices*
        public nint m_fRenderingClipPlane = 0x12C0; // float32[4]
        public nint m_nLastClipPlaneSetupFrame = 0x12D0; // int32
        public nint m_vecLastClipCameraPos = 0x12D4; // Vector
        public nint m_vecLastClipCameraForward = 0x12E0; // Vector
        public nint m_bClipHitStaticWorld = 0x12EC; // bool
        public nint m_bCachedPlaneIsValid = 0x12ED; // bool
        public nint m_pClippingWeapon = 0x12F0; // C_CSWeaponBase*
        public nint m_previousPlayerState = 0x12F8; // CSPlayerState
        public nint m_iPlayerState = 0x12FC; // CSPlayerState
        public nint m_bIsRescuing = 0x1300; // bool
        public nint m_fImmuneToGunGameDamageTime = 0x1304; // GameTime_t
        public nint m_fImmuneToGunGameDamageTimeLast = 0x1308; // GameTime_t
        public nint m_bGunGameImmunity = 0x130C; // bool
        public nint m_bHasMovedSinceSpawn = 0x130D; // bool
        public nint m_fMolotovUseTime = 0x1310; // float32
        public nint m_fMolotovDamageTime = 0x1314; // float32
        public nint m_iThrowGrenadeCounter = 0x1318; // int32
        public nint m_flLastSpawnTimeIndex = 0x131C; // GameTime_t
        public nint m_iProgressBarDuration = 0x1320; // int32
        public nint m_flProgressBarStartTime = 0x1324; // float32
        public nint m_vecIntroStartEyePosition = 0x1328; // Vector
        public nint m_vecIntroStartPlayerForward = 0x1334; // Vector
        public nint m_flClientDeathTime = 0x1340; // GameTime_t
        public nint m_bScreenTearFrameCaptured = 0x1344; // bool
        public nint m_flFlashBangTime = 0x1348; // float32
        public nint m_flFlashScreenshotAlpha = 0x134C; // float32
        public nint m_flFlashOverlayAlpha = 0x1350; // float32
        public nint m_bFlashBuildUp = 0x1354; // bool
        public nint m_bFlashDspHasBeenCleared = 0x1355; // bool
        public nint m_bFlashScreenshotHasBeenGrabbed = 0x1356; // bool
        public nint m_flFlashMaxAlpha = 0x1358; // float32
        public nint m_flFlashDuration = 0x135C; // float32
        public nint m_iHealthBarRenderMaskIndex = 0x1360; // int32
        public nint m_flHealthFadeValue = 0x1364; // float32
        public nint m_flHealthFadeAlpha = 0x1368; // float32
        public nint m_flDeathCCWeight = 0x1378; // float32
        public nint m_flPrevRoundEndTime = 0x137C; // float32
        public nint m_flPrevMatchEndTime = 0x1380; // float32
        public nint m_angEyeAngles = 0x1388; // QAngle
        public nint m_fNextThinkPushAway = 0x13A0; // float32
        public nint m_bShouldAutobuyDMWeapons = 0x13A4; // bool
        public nint m_bShouldAutobuyNow = 0x13A5; // bool
        public nint m_iIDEntIndex = 0x13A8; // CEntityIndex
        public nint m_delayTargetIDTimer = 0x13B0; // CountdownTimer
        public nint m_iTargetItemEntIdx = 0x13C8; // CEntityIndex
        public nint m_iOldIDEntIndex = 0x13CC; // CEntityIndex
        public nint m_holdTargetIDTimer = 0x13D0; // CountdownTimer
        public nint m_flCurrentMusicStartTime = 0x13EC; // float32
        public nint m_flMusicRoundStartTime = 0x13F0; // float32
        public nint m_bDeferStartMusicOnWarmup = 0x13F4; // bool
        public nint m_cycleLatch = 0x13F8; // int32
        public nint m_serverIntendedCycle = 0x13FC; // float32
        public nint m_flLastSmokeOverlayAlpha = 0x1400; // float32
        public nint m_flLastSmokeAge = 0x1404; // float32
        public nint m_vLastSmokeOverlayColor = 0x1408; // Vector
        public nint m_nPlayerSmokedFx = 0x1414; // ParticleIndex_t
        public nint m_nPlayerInfernoBodyFx = 0x1418; // ParticleIndex_t
        public nint m_nPlayerInfernoFootFx = 0x141C; // ParticleIndex_t
        public nint m_flNextMagDropTime = 0x1420; // float32
        public nint m_nLastMagDropAttachmentIndex = 0x1424; // int32
        public nint m_vecLastAliveLocalVelocity = 0x1428; // Vector
        public nint m_bGuardianShouldSprayCustomXMark = 0x1450; // bool
        public nint m_hOriginalController = 0x1458; // CHandle<CCSPlayerController>
    }
}
