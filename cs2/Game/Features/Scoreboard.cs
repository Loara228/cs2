using cs2.Config;
using cs2.Game.Objects;
using cs2.GameOverlay;
using cs2.GameOverlay.UI.Forms;
using cs2.Offsets;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class Scoreboard
    {
        public static void Update()
        {
            if (!Configuration.Current.Misc_Scoreboard)
                return;
            if (Input.IsKeyDown(9)) // Tab vk
            {
                if (TabKeyState == 0)
                    TabKeyState = Input.KeyState.PRESSED;
                else if (TabKeyState == Input.KeyState.PRESSED)
                    TabKeyState = Input.KeyState.DOWN;
            }
            else
            {
                if (TabKeyState == Input.KeyState.DOWN)
                    TabKeyState = Input.KeyState.RELEASE;
                else if (TabKeyState == Input.KeyState.RELEASE)
                    TabKeyState = Input.KeyState.NONE;
            }
            if (TabKeyState == Input.KeyState.DOWN)
            {
                foreach (var e in t)
                {
                    e.Update();
                }
                foreach (var e in ct)
                {
                    e.Update();
                }
            }
        }

        public static void Draw(Graphics g, Rectangle bounds, out int scoreboardHeight)
        {
            scoreboardHeight = 0;
            if (TabKeyState != Input.KeyState.DOWN)
                return;

            scoreboardHeight = SCOREBOARD_ELEMENT_OFFSET + ((t.Count + ct.Count) * (SCOREBOARD_ELEMENT_HEIGHT + SCOREBOARD_ELEMENT_OFFSET)) + SCOREBOARD_ELEMENT_OFFSET_TEAMS;
            scoreboardHeight += SCOREBOARD_ELEMENT_OFFSET;

            float last = bounds.Top + SCOREBOARD_ELEMENT_OFFSET;

            if (LocalPlayer.Current.Team == Structs.Team.Terrorist)
            {
                foreach (PlayerData pData in t)
                {
                    Rectangle rectEl = GetScoreboardElementRect(bounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
                last += SCOREBOARD_ELEMENT_OFFSET_TEAMS;
                foreach (PlayerData pData in ct)
                {
                    Rectangle rectEl = GetScoreboardElementRect(bounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
            }
            else
            {
                foreach (PlayerData pData in ct)
                {
                    Rectangle rectEl = GetScoreboardElementRect(bounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
                last += SCOREBOARD_ELEMENT_OFFSET_TEAMS;
                foreach (PlayerData pData in t)
                {
                    Rectangle rectEl = GetScoreboardElementRect(bounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
            }
            return;
        }

        private static Rectangle GetScoreboardElementRect(Rectangle scoreboardRect, float lastElementBottom)
        {
            return new Rectangle(
                scoreboardRect.Left + COLOR_RECT_W + SCOREBOARD_ELEMENT_OFFSET * 2,
                lastElementBottom + SCOREBOARD_ELEMENT_OFFSET,
                scoreboardRect.Right - MONEY_RECT_W - MONEY_SPENT_RECT_W - SCOREBOARD_ELEMENT_OFFSET * 3,
                lastElementBottom + SCOREBOARD_ELEMENT_OFFSET + SCOREBOARD_ELEMENT_HEIGHT);
        }

        private static void DrawScoreboardElement(Graphics g, Rectangle elementRect, PlayerData pData)
        {
            // COLOR RECT
            g.FillRectangle(pData.Color, new Rectangle(elementRect.Left - SCOREBOARD_ELEMENT_OFFSET - COLOR_RECT_W,
                elementRect.Top,
                elementRect.Left - SCOREBOARD_ELEMENT_OFFSET,
                elementRect.Bottom));

            //NICKNAME RECT
            var nicknameRect = g.GetTextRect(Fonts.Consolas, pData.Nickname, out float rectOffset);
            g.FillRectangle(Brushes.HalfBlack,
                elementRect);

            //MONEY_SPENT RECT
            var moneySpentRect = new Rectangle(
                elementRect.Right + SCOREBOARD_ELEMENT_OFFSET,
                elementRect.Top,
                elementRect.Right + SCOREBOARD_ELEMENT_OFFSET + MONEY_SPENT_RECT_W,
                elementRect.Bottom);
            g.FillRectangle(Brushes.HalfBlack, moneySpentRect);
            g.DrawText(moneySpentRect, Fonts.Consolas, pData.m_iCashSpentThisRound.ToString(), Brushes.White);

            //MONEY RECT
            var moneyRect = new Rectangle(
                moneySpentRect.Right + SCOREBOARD_ELEMENT_OFFSET,
                moneySpentRect.Top,
                moneySpentRect.Right + SCOREBOARD_ELEMENT_OFFSET + MONEY_RECT_W,
                moneySpentRect.Bottom);
            g.FillRectangle(Brushes.HalfBlack, moneyRect);
            g.DrawText(moneyRect, Fonts.Consolas, pData.m_iAccount.ToString(), Brushes.White);

            g.DrawText(Fonts.Consolas,
                pData.entity.Team == Structs.Team.Terrorist ? Brushes.ScoreboardElementT : Brushes.ScoreboardElementCT,
                elementRect.Left + SCOREBOARD_ELEMENT_OFFSET,
                elementRect.Top + elementRect.Height / 2 - nicknameRect.Height / 2f + rectOffset,
                pData.Nickname + $" {pData.wins}");
        }

        private static void UpdateList()
        {
            t.Clear();
            ct.Clear();
            foreach (var entity in Program.Entities)
            {
                if (entity.AddressBase == 0)
                    continue;
                if (entity.Team == Structs.Team.Terrorist)
                    t.Add(new PlayerData(entity));
                else if (entity.Team == Structs.Team.CounterTerrorist)
                    ct.Add(new PlayerData(entity));
            }
        }

        private static Input.KeyState TabKeyState
        {
            get => _tabKeyState;
            set
            {
                _tabKeyState = value;
                if (value == Input.KeyState.PRESSED)
                {
                    FormScoreboard.Current.GameForm = true;
                    UpdateList();
                }
                else if (value == Input.KeyState.RELEASE)
                {
                    FormScoreboard.Current.GameForm = false;
                }
            }
        }

        private static Input.KeyState _tabKeyState;

        private static List<PlayerData> t = new List<PlayerData>(), 
                                        ct = new List<PlayerData>();

        internal class PlayerData
        {
            public PlayerData(Entity entity)
            {
                this.entity = entity;
                int currentMMR = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompetitiveRanking);
                int win = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompetitiveRankingPredicted_Win);
                int loss = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompetitiveRankingPredicted_Loss);
                int tie = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompetitiveRankingPredicted_Tie);

                wins = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompetitiveWins);
                color = Memory.Read<int>(entity.ControllerBase + CCSPlayerController.m_iCompTeammateColor);

                predicted_Win = win - currentMMR;
                predicted_Loss = loss - currentMMR;
                predicted_Tie = tie - currentMMR;
            }

            public void Update()
            {
                IntPtr m_pInGameMoneyServices = Memory.Read<IntPtr>(entity.ControllerBase + CCSPlayerController.m_pInGameMoneyServices);

                m_iAccount = Memory.Read<int>(m_pInGameMoneyServices + CCSPlayerController_InGameMoneyServices.m_iAccount);
                m_iCashSpentThisRound = Memory.Read<int>(m_pInGameMoneyServices + CCSPlayerController_InGameMoneyServices.m_iCashSpentThisRound);

                //IntPtr m_pWeaponServices = Memory.Read<IntPtr>(entity.AddressBase + C_BasePlayerPawn.m_pWeaponServices);
                //int ammo = Memory.Read<int>(m_pWeaponServices + 0x40 + 0x15C8 + 22); // + m_hMyWeapons + m_iClip1 
                //wins = ammo;
            }

            public string Nickname
            {
                get
                {
                    if (string.IsNullOrEmpty(entity.Nickname))
                        return "LOCAL_PLAYER";
                    return entity.Nickname;
                }
            }

            public IBrush Color
            {
                get
                {
                    if (color == 0)
                        return Brushes.TeamBlue;
                    else if (color == 1)
                        return Brushes.Green;
                    else if (color == 2)
                        return Brushes.TeamYellow;
                    else if (color == 3)
                        return Brushes.TeamOrange;
                    else if (color == 4)
                        return Brushes.TeamPurple;
                    return Brushes.HalfBlack;
                }
            }

            public Entity entity;

            public int predicted_Win, predicted_Loss, predicted_Tie;
            public int wins;
            public int m_iAccount, m_iCashSpentThisRound;

            private int color;
        }

        private const int SCOREBOARD_ELEMENT_HEIGHT = 25;
        private const int SCOREBOARD_ELEMENT_OFFSET = 5;
        private const int SCOREBOARD_ELEMENT_OFFSET_TEAMS = 10;

        private const int COLOR_RECT_W = 5;
        private const int MONEY_RECT_W = 50;
        private const int MONEY_SPENT_RECT_W = 50;

    }
}