using cs2.Game.Features;
using cs2.Game.Objects;
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
        }

        private void Draw(Graphics g)
        {
            WallHack.Draw(g);
            SpectatorList.Draw(g);

            Scoreboard.Draw(g);
        }

        private void OnDraw(Graphics g)
        {
            Update();
            g.ClearScene();
            Draw(g);
        }

        #region Events

        private void Window_SetupGraphics(object? sender, SetupGraphicsEventArgs e)
        {
            Brushes.Initialize(e.Graphics);
            Fonts.Initialize(e.Graphics);
        }

        private void Window_DestroyGraphics(object? sender, DestroyGraphicsEventArgs e)
        {
            Brushes.Dispose();
            Fonts.Dispose();
        }

        private void Window_DrawGraphics(object? sender, DrawGraphicsEventArgs e) => OnDraw(e.Graphics);

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

        private bool _disposed;
    }
}
