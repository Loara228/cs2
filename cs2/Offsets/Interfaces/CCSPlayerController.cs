using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class CCSPlayerController : InterfaceBase
    {
        public CCSPlayerController() : base("")
        {
        }

        public nint m_pInGameMoneyServices = 0x6F8; // CCSPlayerController_InGameMoneyServices*
        public nint m_pInventoryServices = 0x700; // CCSPlayerController_InventoryServices*
        public nint m_pActionTrackingServices = 0x708; // CCSPlayerController_ActionTrackingServices*
        public nint m_pDamageServices = 0x710; // CCSPlayerController_DamageServices*
        public nint m_iPing = 0x718; // uint32_t
        public nint m_bHasCommunicationAbuseMute = 0x71C; // bool
        public nint m_szCrosshairCodes = 0x720; // CUtlSymbolLarge
        public nint m_iPendingTeamNum = 0x728; // uint8_t
        public nint m_flForceTeamTime = 0x72C; // GameTime_t
        public nint m_iCompTeammateColor = 0x730; // int32_t
        public nint m_bEverPlayedOnTeam = 0x734; // bool
        public nint m_flPreviousForceJoinTeamTime = 0x738; // GameTime_t
        public nint m_szClan = 0x740; // CUtlSymbolLarge
        public nint m_sSanitizedPlayerName = 0x748; // CUtlString
        public nint m_iCoachingTeam = 0x750; // int32_t
        public nint m_nPlayerDominated = 0x758; // uint64_t
        public nint m_nPlayerDominatingMe = 0x760; // uint64_t
        public nint m_iCompetitiveRanking = 0x768; // int32_t
        public nint m_iCompetitiveWins = 0x76C; // int32_t
        public nint m_iCompetitiveRankType = 0x770; // int8_t
        public nint m_iCompetitiveRankingPredicted_Win = 0x774; // int32_t
        public nint m_iCompetitiveRankingPredicted_Loss = 0x778; // int32_t
        public nint m_iCompetitiveRankingPredicted_Tie = 0x77C; // int32_t
        public nint m_nEndMatchNextMapVote = 0x780; // int32_t
        public nint m_unActiveQuestId = 0x784; // uint16_t
        public nint m_nQuestProgressReason = 0x788; // QuestProgress::Reason
        public nint m_unPlayerTvControlFlags = 0x78C; // uint32_t
        public nint m_iDraftIndex = 0x7B8; // int32_t
        public nint m_msQueuedModeDisconnectionTimestamp = 0x7BC; // uint32_t
        public nint m_uiAbandonRecordedReason = 0x7C0; // uint32_t
        public nint m_bCannotBeKicked = 0x7C4; // bool
        public nint m_bEverFullyConnected = 0x7C5; // bool
        public nint m_bAbandonAllowsSurrender = 0x7C6; // bool
        public nint m_bAbandonOffersInstantSurrender = 0x7C7; // bool
        public nint m_bDisconnection1MinWarningPrinted = 0x7C8; // bool
        public nint m_bScoreReported = 0x7C9; // bool
        public nint m_nDisconnectionTick = 0x7CC; // int32_t
        public nint m_bControllingBot = 0x7D8; // bool
        public nint m_bHasControlledBotThisRound = 0x7D9; // bool
        public nint m_bHasBeenControlledByPlayerThisRound = 0x7DA; // bool
        public nint m_nBotsControlledThisRound = 0x7DC; // int32_t
        public nint m_bCanControlObservedBot = 0x7E0; // bool
        public nint m_hPlayerPawn = 0x7E4; // CHandle<C_CSPlayerPawn>
        public nint m_hObserverPawn = 0x7E8; // CHandle<C_CSObserverPawn>
        public nint m_bPawnIsAlive = 0x7EC; // bool
        public nint m_iPawnHealth = 0x7F0; // uint32_t
        public nint m_iPawnArmor = 0x7F4; // int32_t
        public nint m_bPawnHasDefuser = 0x7F8; // bool
        public nint m_bPawnHasHelmet = 0x7F9; // bool
        public nint m_nPawnCharacterDefIndex = 0x7FA; // uint16_t
        public nint m_iPawnLifetimeStart = 0x7FC; // int32_t
        public nint m_iPawnLifetimeEnd = 0x800; // int32_t
        public nint m_iPawnBotDifficulty = 0x804; // int32_t
        public nint m_hOriginalControllerOfCurrentPawn = 0x808; // CHandle<CCSPlayerController>
        public nint m_iScore = 0x80C; // int32_t
        public nint m_vecKills = 0x810; // C_NetworkUtlVectorBase<EKillTypes_t>
        public nint m_iMVPs = 0x828; // int32_t
        public nint m_bIsPlayerNameDirty = 0x82C; // bool
    }
}
