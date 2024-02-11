using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay.UI.Controls;
using cs2.GameOverlay.UI.Forms;
using cs2.Offsets;
using cs2.Offsets.Interfaces;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{
    internal class Overlay : IDisposable
    {
        public Overlay()
        {
            Graphics g = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true
            };

            Window = new GraphicsWindow(0, 0, 1920, 1080, g)
            {
                FPS = 60,
                IsTopmost = true,
                IsVisible = true
            };

            Window.DestroyGraphics += Window_DestroyGraphics;
            Window.DrawGraphics += Window_DrawGraphics;
            Window.SetupGraphics += Window_SetupGraphics;

            Window.Create();
            Window.Join();
        }

        private void Update()
        {
            Input.Update();

            Program.Entities.Clear();
            Program.LocalPlayer.Update();
            for (int i = 0; i < 12; i++)
            {
                Entity entity = new(i);
                entity.Update();
                Program.Entities.Add(entity);
            }

            SpectatorList.Update();
            Scoreboard.Update();

            keyHome.Update();

            if (keyHome.state == Input.KeyState.PRESSED)
                _enableUI = !_enableUI;

            if (_enableUI)
            {
                foreach(var form in Forms)
                {
                    form.Update();
                }
            }
        }

        private void Draw(Graphics g)
        {
            WallHack.Draw(g);
            SpectatorList.Draw(g);
            AimAssist.Draw(g);

            Scoreboard.Draw(g);
        }

        private void OnDraw(Graphics g)
        {
            Update();
            g.ClearScene();

            Draw(g);

            if (_enableUI)
            {
                g.FillRectangle(Brushes.HalfBlack, new Rectangle(0, 0, g.Width, g.Height));
                foreach (var form in Forms)
                {
                    form.Draw(g);
                }
            }
        }

        #region Events

        private void Window_SetupGraphics(object? sender, SetupGraphicsEventArgs e)
        {
            Brushes.Initialize(e.Graphics);
            Fonts.Initialize(e.Graphics);
            InitializeForms();
        }

        private void Window_DestroyGraphics(object? sender, DestroyGraphicsEventArgs e)
        {
            Brushes.Dispose();
            Fonts.Dispose();
        }

        private void Window_DrawGraphics(object? sender, DrawGraphicsEventArgs e) => OnDraw(e.Graphics);

        #endregion

        #region Forms

        private void InitializeForms()
        {
            Forms = [new FormSpectators()];
        }

        #endregion

        #region Dispose

        ~Overlay()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Window.Dispose();

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public GraphicsWindow Window
        {
            get; private set;
        }

        public List<UIForm> Forms
        {
            get; private set;
        }

        private Input.Key keyHome = new Input.Key(36);
        private bool _enableUI;
        private bool _disposed;
    }
}
