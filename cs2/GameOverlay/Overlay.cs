﻿using cs2.Config;
using cs2.Game;
using cs2.Game.Features;
using cs2.Game.Objects;
using cs2.GameOverlay.UI;
using cs2.GameOverlay.UI.Controls;
using cs2.GameOverlay.UI.Forms;
using cs2.Offsets;
using cs2.Offsets.Interfaces;
using cs2.PInvoke;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static cs2.PInvoke.User32;

namespace cs2.GameOverlay
{
    internal class Overlay : IDisposable
    {
        public Overlay()
        {
            Current = this;
            Graphics g = new Graphics()
            {
                MeasureFPS = true,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = true,
                VSync = true
            };
            var screen = ScreenHelper.GetBounds();
            Window = new GraphicsWindow((int)screen.Left, (int)screen.Top, (int)screen.Width, (int)screen.Height)
            {
                FPS = Config.Configuration.Current.FPS_Max,
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
            Brushes.Update();
            GlobalVars.Update();
            Program.Entities.Clear();
            LocalPlayer.Current.Update();
            HitMarker.Update();
            for (int i = 0; i < Program.ENTITY_LIST_COUNT; i++)
            {
                Entity entity = new(i);
                entity.Update();
                Program.Entities.Add(entity);
            }

            Input.Update();
            Scoreboard.Update();
            keyHome.Update();

            if (keyHome.state == Input.KeyState.PRESSED)
            {
                drawUI = !drawUI;
                if (drawUI)
                {
                    var attributes = ExtendedWindowStyle.Topmost | ExtendedWindowStyle.Transparent /*| ExtendedWindowStyle.Layered | ExtendedWindowStyle.NoActivate*/;
                    Input.PressKey(Input.ScanCodeShort.ESCAPE);
                    User32.SetWindowLong(Window.Handle, -20, (int)attributes);
                    User32.SendMessage(Window.Handle, 0x0020, IntPtr.Zero, IntPtr.Zero);
                    // WM_SETCURSOR 
                    // костыльное обновление курсора :)
                }
                else
                {
                    Input.PressKey(Input.ScanCodeShort.ESCAPE);
                    var attributes = ExtendedWindowStyle.Topmost | ExtendedWindowStyle.Transparent | ExtendedWindowStyle.Layered | ExtendedWindowStyle.NoActivate;
                    User32.SetWindowLong(Window.Handle, -20, (int)attributes);
                    //User32.SendMessage(Memory.WindowHandle, 0x0018, IntPtr.Zero, IntPtr.Zero);
                    //0x0007
                }
            }
            _removeForms.ForEach(x => Forms.Remove(x));
            if (drawUI)
            {
                UIForm.FocusedFrame = false;
                Forms.OrderByDescending(x => x.Layer).ToList().ForEach(x => x.Update());
            }
        }

        private void Draw(Graphics g)
        {
            WorldEsp.Draw(g);
            WallHack.Draw(g);
            AimAssist.Draw(g);
            Crosshair.Draw(g);
            HitMarker.Draw(g);
        }

        private void OnDraw(Graphics g)
        {
            Update();
            g.ClearScene();
            Draw(g);

            if (drawUI)
            {
                g.FillRectangle(Brushes.HalfBlack, new Rectangle(0, 0, g.Width, g.Height));
            }
            Forms.OrderBy(x => x.Layer).ToList().ForEach(x => x.Draw(g));
        }

        #region Events

        private void Window_SetupGraphics(object? sender, SetupGraphicsEventArgs e)
        {
            ScreenSize = new Vec2i(e.Graphics.Width, e.Graphics.Height);
            Program.Log($"Window_SetupGraphics {e.Graphics.Width}, {e.Graphics.Height}");
            Brushes.Initialize(e.Graphics);
            Fonts.Initialize(e.Graphics);
            InitializeForms(e.Graphics);
        }

        private void Window_DestroyGraphics(object? sender, DestroyGraphicsEventArgs e)
        {
            Brushes.Dispose();
            Fonts.Dispose();
            Program.Log("setup graphics");
        }

        private void Window_DrawGraphics(object? sender, DrawGraphicsEventArgs e) => OnDraw(e.Graphics);

        #endregion

        #region Forms

        private void InitializeForms(Graphics g)
        {
            UIControl.initGraphics = g;

            int y = Overlay.ScreenSize.y - UIForm.HEADER_SIZE - 2;

            UIForm cfgForm = new FormConfig(ScreenSize.x - 330, 10);

            Forms = new List<UIForm>()
            {
                cfgForm,

                new FormSpectators(),
                new FormRadar(),
                new FormBomb(),
                new FormVisuals(0, y),
            };
            Forms.Add(new FormMisc((int)Forms[^1].Rect.Right + 4, y));
            Forms.Add(new FormAimAssist((int)Forms[^1].Rect.Right + 4, y));
            Forms.Add(new FormCrosshair((int)Forms[^1].Rect.Right + 4, y));

            Forms.Add(new FormScoreboard());

            UIControl.initGraphics = null;
        }

        public void ApplyConfiguration()
        {
            foreach (var form in Forms)
            {
                form.ApplyConfig();
            }
        }

        public void FormFocusChanged()
        {
            foreach (var form in Forms)
            {
                form.FocusChanged();
            }
        }

        public void Open(UIForm form)
        {
            Forms.Add(form);
            form.Focus();
        }

        public void RemoveForm(UIForm form)
        {
            _removeForms.Add(form);
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

        public static Overlay Current { get; private set; } = null!;

        public GraphicsWindow Window
        {
            get; private set;
        }

        public List<UIForm> Forms
        {
            get; private set;
        } = null!;

        private List<UIForm> _removeForms
        {
            get; set;
        } = new List<UIForm>();

        public static Vec2i ScreenSize
        {
            get; private set;
        }

        public static bool drawUI;

        private Input.Key keyHome = new Input.Key(36);
        private bool _disposed;
    }
}
