using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class C_PlantedC4 : InterfaceBase
    {
        public C_PlantedC4() : base("")
        {

        }

        public nint m_bBombTicking = 0xED8; // bool
        public nint m_nBombSite = 0xEDC; // int32_t
        public nint m_nSourceSoundscapeHash = 0xEE0; // int32_t
        public nint m_entitySpottedState = 0xEE8; // EntitySpottedState_t
        public nint m_flNextGlow = 0xF00; // GameTime_t
        public nint m_flNextBeep = 0xF04; // GameTime_t
        public nint m_flC4Blow = 0xF08; // GameTime_t
        public nint m_bCannotBeDefused = 0xF0C; // bool
        public nint m_bHasExploded = 0xF0D; // bool
        public nint m_flTimerLength = 0xF10; // float
        public nint m_bBeingDefused = 0xF14; // bool
        public nint m_bTriggerWarning = 0xF18; // float
        public nint m_bExplodeWarning = 0xF1C; // float
        public nint m_bC4Activated = 0xF20; // bool
        public nint m_bTenSecWarning = 0xF21; // bool
        public nint m_flDefuseLength = 0xF24; // float
        public nint m_flDefuseCountDown = 0xF28; // GameTime_t
        public nint m_bBombDefused = 0xF2C; // bool
        public nint m_hBombDefuser = 0xF30; // CHandle<C_CSPlayerPawn>
        public nint m_hControlPanel = 0xF34; // CHandle<C_BaseEntity>
        public nint m_hDefuserMultimeter = 0xF38; // CHandle<C_Multimeter>
        public nint m_flNextRadarFlashTime = 0xF3C; // GameTime_t
        public nint m_bRadarFlash = 0xF40; // bool
        public nint m_pBombDefuser = 0xF44; // CHandle<C_CSPlayerPawn>
        public nint m_fLastDefuseTime = 0xF48; // GameTime_t
        public nint m_pPredictionOwner = 0xF50; // CBasePlayerController*
        public nint m_vecC4ExplodeSpectatePos = 0xF58; // Vector
        public nint m_vecC4ExplodeSpectateAng = 0xF64; // QAngle
        public nint m_flC4ExplodeSpectateDuration = 0xF70; // float
    }
}
