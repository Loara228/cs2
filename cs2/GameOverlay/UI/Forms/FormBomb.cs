using cs2.Config;
using cs2.Game;
using cs2.Game.Structs;
using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormBomb : UIForm
    {
        public FormBomb() : base(Configuration.Current.FormBombTimePos.x, Configuration.Current.FormBombTimePos.y, "bomb timer")
        {
            Width = 155;
            Height = 100;

            GameForm = true;

            Add(_bombTimer = new UIProgressbar(40) { Width = 145, MaxValue = 40, ActiveColor = Brushes.UIBombColor });
            Add(_defuseTimer = new UIProgressbar(0) { Width = 145, MaxValue = 40, ActiveColor = Brushes.UIDefuseColor });
            Add(_labelTimeLeft = new UILabel("bomb: 0.00") { TextColor = Brushes.White, Margin = new Margin(5, 0, 0, 0) });
            Add(_labelDefuse = new UILabel("defuse: 0.00") { TextColor = Brushes.White, Margin = new Margin(5, 0, 0, 0) });

            onMove += (int x, int y) => { Configuration.Current.FormBombTimePos = new Vec2i(X, Y); };
        }

        public override void Update()
        {
            if (!Configuration.Current.Misc_BombTimer)
                return;
            base.Update();
        }

        public override void ApplyConfig()
        {
            Position = new Point(Configuration.Current.FormBombTimePos.x, Configuration.Current.FormBombTimePos.y);
            UpdateControlsPos();
            base.ApplyConfig();
        }

        public override void Draw(Graphics g)
        {
            if (!Configuration.Current.Misc_BombTimer)
                return;
            var tempC4 = Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwPlantedC4);
            var plantedC4 = Memory.Read<IntPtr>(tempC4);

            var isBombPlanted = Memory.Read<bool>(Memory.ClientPtr + ClientOffsets.dwPlantedC4 - 0x8);

            if (!isBombPlanted && !Overlay.drawUI)
                return;

            BombSite site = Memory.Read<int>(plantedC4 + C_PlantedC4.m_nBombSite) == 1 ? BombSite.B : BombSite.A;
            //var timerLenght = Memory.Read<float>(plantedC4 + C_PlantedC4.m_flTimerLength); //40 sec
            //var m_flDefuseLength = Memory.Read<float>(plantedC4 + C_PlantedC4.m_flDefuseLength);
            //var m_fLastDefuseTime = Memory.Read<float>(plantedC4 + C_PlantedC4.m_fLastDefuseTime);
            var m_flDefuseCountDown = Memory.Read<float>(plantedC4 + C_PlantedC4.m_flDefuseCountDown);
            var m_flC4Blow = Memory.Read<float>(plantedC4 + C_PlantedC4.m_flC4Blow);
            var m_bBeingDefused = Memory.Read<bool>(plantedC4 + C_PlantedC4.m_bBeingDefused);

            var timeLeft = m_flC4Blow - GlobalVars.CurrentTime;
            var defuseLeft = m_flDefuseCountDown - GlobalVars.CurrentTime;

            if (timeLeft < 0)
                timeLeft = 0;
            if (defuseLeft < 0)
                defuseLeft = 0;

            if (!m_bBeingDefused)
                defuseLeft = 0;

            if (isBombPlanted)
            {
                SetTitle($"{site}");

                _labelTimeLeft.Text = $"bomb: {timeLeft.ToString("0.00")}";
                _labelDefuse.Text = $"defuse: {defuseLeft.ToString("0.00")}";

                _bombTimer.Value = timeLeft;
                _defuseTimer.Value = defuseLeft;
            }
            else
            {
                SetTitle($"bomb timer");

                _labelTimeLeft.Text = $"bomb: 0";
                _labelDefuse.Text = $"defuse: 0";

                _bombTimer.Value = 40;
                _defuseTimer.Value = 0;
            }

            base.Draw(g);
        }

        private UILabel _labelTimeLeft;
        private UILabel _labelDefuse;

        private UIProgressbar _bombTimer;
        private UIProgressbar _defuseTimer;
    }
}
