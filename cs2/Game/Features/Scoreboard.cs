using cs2.Config;
using cs2.Game.Objects;
using cs2.GameOverlay;
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
        }

        public static void Draw(Graphics g)
        {
            if (!Configuration.Current.Misc_Scoreboard)
                return;

            if (TabKeyState != Input.KeyState.DOWN)
                return;

            int ScoreboardHeight = SCOREBOARD_ELEMENT_OFFSET + ((t.Count + ct.Count) * (SCOREBOARD_ELEMENT_HEIGHT + SCOREBOARD_ELEMENT_OFFSET)) + SCOREBOARD_ELEMENT_OFFSET_TEAMS;
            Rectangle scoreboardBounds = new Rectangle(g.Width - SCOREBOARD_WIDTH, 0, g.Width, ScoreboardHeight);
            g.FillRectangle(Brushes.HalfBlack, scoreboardBounds);

            float last = 0;

            if (LocalPlayer.Current.Team == Structs.Team.Terrorist)
            {
                foreach (PlayerData pData in t)
                {
                    Rectangle rectEl = GetScoreboardElementRect(scoreboardBounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
                last += SCOREBOARD_ELEMENT_OFFSET_TEAMS;
                foreach (PlayerData pData in ct)
                {
                    Rectangle rectEl = GetScoreboardElementRect(scoreboardBounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
            }
            else
            {
                foreach (PlayerData pData in ct)
                {
                    Rectangle rectEl = GetScoreboardElementRect(scoreboardBounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
                last += SCOREBOARD_ELEMENT_OFFSET_TEAMS;
                foreach (PlayerData pData in t)
                {
                    Rectangle rectEl = GetScoreboardElementRect(scoreboardBounds, last);
                    DrawScoreboardElement(g, rectEl, pData);
                    last = rectEl.Bottom;
                }
            }
        }

        private static Rectangle GetScoreboardElementRect(Rectangle scoreboardRect, float lastElementBottom)
        {
            int top = (int)lastElementBottom + SCOREBOARD_ELEMENT_OFFSET;
            return new Rectangle(scoreboardRect.Left + SCOREBOARD_ELEMENT_OFFSET, top, scoreboardRect.Right - SCOREBOARD_ELEMENT_OFFSET, top + SCOREBOARD_ELEMENT_HEIGHT);
        }

        private static void DrawScoreboardElement(Graphics g, Rectangle elementRect, PlayerData pData)
        {
            g.FillRectangle(pData.Color, new Rectangle(elementRect.Left, elementRect.Top, elementRect.Left + 5, elementRect.Bottom));

            int left = (int)elementRect.Left + SCOREBOARD_ELEMENT_OFFSET * 3;
            g.FillRectangle(Brushes.HalfBlack, elementRect);
            IBrush brush = pData.entity.Team == Structs.Team.Terrorist ? Brushes.ScoreboardElementT : Brushes.ScoreboardElementCT;

            g.DrawText(Fonts.Consolas, 14, brush, left, elementRect.Top + SCOREBOARD_ELEMENT_OFFSET, pData.Nickname);
            g.DrawText(Fonts.Consolas, 12, Brushes.White, new Point(left, elementRect.Top + 20), $"Money: {pData.m_iAccount}");
            g.DrawText(Fonts.Consolas, 12, Brushes.White, new Point(left, elementRect.Top + 30), $"Spent: {pData.m_iCashSpentThisRound}");

            Rectangle rectWins = g.GetTextRect(Fonts.Consolas, $"{pData.wins} Wins", out _, 0, 0);
            g.DrawText(Fonts.Consolas, Brushes.White, elementRect.Right - rectWins.Width - SCOREBOARD_ELEMENT_OFFSET, elementRect.Top + SCOREBOARD_ELEMENT_OFFSET, $"{pData.wins} Wins");

            Rectangle rectWinPred = g.GetTextRect(Fonts.Consolas, $"+{pData.predicted_Win}", out _, 0, 0);
            g.DrawText(Fonts.Consolas, Brushes.Green, elementRect.Right - rectWinPred.Width - SCOREBOARD_ELEMENT_OFFSET, elementRect.Top + 20 + SCOREBOARD_ELEMENT_OFFSET, $"+{pData.predicted_Win}");
            
            Rectangle rectLossPred = g.GetTextRect(Fonts.Consolas, $"{pData.predicted_Loss}", out _, 0, 0);
            g.DrawText(Fonts.Consolas, Brushes.Red, elementRect.Right - rectLossPred.Width - SCOREBOARD_ELEMENT_OFFSET, elementRect.Top + 30 + SCOREBOARD_ELEMENT_OFFSET, $"{pData.predicted_Loss}");

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
                    UpdateList();
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

                IntPtr m_pInGameMoneyServices = Memory.Read<IntPtr>(entity.ControllerBase + CCSPlayerController.m_pInGameMoneyServices);

                m_iAccount = Memory.Read<int>(m_pInGameMoneyServices + CCSPlayerController_InGameMoneyServices.m_iAccount);
                m_iCashSpentThisRound = Memory.Read<int>(m_pInGameMoneyServices + CCSPlayerController_InGameMoneyServices.m_iCashSpentThisRound);

                predicted_Win = win - currentMMR;
                predicted_Loss = loss - currentMMR;
                predicted_Tie = tie - currentMMR;
            }

            public string Nickname
            {
                get
                {
                    if (string.IsNullOrEmpty(entity.Nickname))
                        return "?";
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

        private const int SCOREBOARD_WIDTH = 400;
        private const int SCOREBOARD_ELEMENT_HEIGHT = 50;
        private const int SCOREBOARD_ELEMENT_OFFSET = 5;
        private const int SCOREBOARD_ELEMENT_OFFSET_TEAMS = 50;

    }
}