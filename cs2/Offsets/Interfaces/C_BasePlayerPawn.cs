using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_BasePlayerPawn : InterfaceBase
    {
        public C_BasePlayerPawn() : base("")
        {
        }

        public nint m_pWeaponServices = 0x10A8; // CPlayer_WeaponServices*
        public nint m_pItemServices = 0x10B0; // CPlayer_ItemServices*
        public nint m_pAutoaimServices = 0x10B8; // CPlayer_AutoaimServices*
        public nint m_pObserverServices = 0x10C0; // CPlayer_ObserverServices*
        public nint m_pWaterServices = 0x10C8; // CPlayer_WaterServices*
        public nint m_pUseServices = 0x10D0; // CPlayer_UseServices*
        public nint m_pFlashlightServices = 0x10D8; // CPlayer_FlashlightServices*
        public nint m_pCameraServices = 0x10E0; // CPlayer_CameraServices*
        public nint m_pMovementServices = 0x10E8; // CPlayer_MovementServices*
        public nint m_ServerViewAngleChanges = 0x10F8; // C_UtlVectorEmbeddedNetworkVar<ViewAngleServerChange_t>
        public nint m_nHighestConsumedServerViewAngleChangeIndex = 0x1148; // uint32_t
        public nint v_angle = 0x114C; // QAngle
        public nint v_anglePrevious = 0x1158; // QAngle
        public nint m_iHideHUD = 0x1164; // uint32_t
        public nint m_skybox3d = 0x1168; // sky3dparams_t
        public nint m_flDeathTime = 0x11F8; // GameTime_t
        public nint m_vecPredictionError = 0x11FC; // Vector
        public nint m_flPredictionErrorTime = 0x1208; // GameTime_t
        public nint m_vecLastCameraSetupLocalOrigin = 0x120C; // Vector
        public nint m_flLastCameraSetupTime = 0x1218; // GameTime_t
        public nint m_flFOVSensitivityAdjust = 0x121C; // float
        public nint m_flMouseSensitivity = 0x1220; // float
        public nint m_vOldOrigin = 0x1224; // Vector
        public nint m_flOldSimulationTime = 0x1230; // float
        public nint m_nLastExecutedCommandNumber = 0x1234; // int32_t
        public nint m_nLastExecutedCommandTick = 0x1238; // int32_t
        public nint m_hController = 0x123C; // CHandle<CBasePlayerController>
        public nint m_bIsSwappingToPredictableController = 0x1240; // bool
    }
}
