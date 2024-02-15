using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UIForm : UIContainer
    {
        public UIForm(int x, int y, string label = "form") : base(x, y, Brushes.UIBackgroundColor)
        {
            BrushBorder = Brushes.UIBorderColor;
            Margin = new Margin(0);

            _header = new UIPanel((int)Rect.Left, (int)Rect.Top, Brushes.UIHeaderColor);
            _header.Width = (int)(Rect.Right - Rect.Left);
            _header.Height = HEADER_SIZE;
            _header.Margin = new Margin(1);

            _title = new UILabel(Fonts.Consolas, Brushes.UITextColor, label);
            _title.Height = HEADER_SIZE;
            _header.Add(_title);

            this.Add(_header);
        }

        public int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                _header.Width = base.Width - _header.Margin.Left * 2;
            }
        }

        public int Height
        {
            get => base.Height;
            set
            {
                base.Height = value;
            }
        }

        public override void Update()
        {
            if (Rect.IsMouseOn() && Input.MouseLeft.state == Input.KeyState.PRESSED)
                UpdateLayer();

            if (_dragging)
            {
                Point currentPos = Input.CursorPos;

                Point offset = new Point(currentPos.X - _lastMousePos.X, currentPos.Y - _lastMousePos.Y);
                onMove?.Invoke((int)offset.X, (int)offset.Y);
                Position = new Point(X + offset.X, Y + offset.Y);

                _lastMousePos = currentPos;
                UpdateControlsPos();
                _header.UpdateControlsPos();
            }
            if (Focused && _header.Rect.IsMouseOn())
            {
                if (Input.MouseLeft.state == Input.KeyState.PRESSED)
                {
                    _dragging = true;
                    _lastMousePos = Input.CursorPos;
                }
            }
            if (_dragging && Input.MouseLeft.state == Input.KeyState.NONE)
            {
                _dragging = false;
            }

            if (Focused)
                base.Update();
        }

        public override void Draw(Graphics g)
        {
            BrushBorder = Focused ? Brushes.UIActiveColor : Brushes.UIBorderColor;
            if (Overlay.drawUI && GameForm)
            {
                BrushBackground = Brushes.UIBackgroundColor;
                _header.BrushBackground = Brushes.UIHeaderColor;
            }
            else if (!Overlay.drawUI && GameForm)
            {
                BrushBorder = Brushes.UIBorderColor2;
                BrushBackground = Brushes.UIBackgroundColor2;
                _header.BrushBackground = Brushes.UIHeaderColor2;
            }
            if (Overlay.drawUI || GameForm)
                base.Draw(g);
        }

        private bool UpdateLayer()
        {
            if (!FocusedFrame)
            {
                BrushBorder = Brushes.UIActiveColor;
                FocusedForm = this;
                _layerIndex++;
                Layer = _layerIndex;
                FocusedFrame = true;

                return true;
            }
            return false;
        }

        public static UIForm? FocusedForm
        {
            get; set;
        } = null!;

        internal static bool FocusedFrame
        {
            get; set;
        }

        public bool Focused
        {
            get => Overlay.drawUI && FocusedForm is not null && FocusedForm == this;
        }

        public uint Layer
        {
            get; private set;
        } = 0;

        public bool GameForm
        {
            get; protected set;
        }

        public Action<int, int> onMove;

        private static uint _layerIndex;
        private bool _dragging;
        private Point _lastMousePos;
        private UIPanel _header;
        private UILabel _title;

        public const int HEADER_SIZE = 25;
    }
}
