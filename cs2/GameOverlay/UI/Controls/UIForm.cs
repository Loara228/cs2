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
        public UIForm(int x, int y, string label = "form", bool disableHeader = false) : base(x, y, Brushes.UIBackgroundColor)
        {
            BrushBorder = Brushes.UIBorderColor;
            Margin = new Margin(0);
            if (!disableHeader)
            {
                _header = new UIPanel((int)Rect.Left, (int)Rect.Top, Brushes.UIHeaderColor);
                _header.Width = (int)(Rect.Right - Rect.Left);
                _header.MinHeight = HEADER_SIZE;
                _header.Height = HEADER_SIZE;
                _header.Margin = new Margin(1);

                _title = new UILabel(Fonts.Consolas, Brushes.UITextColor, label);
                _title.Height = HEADER_SIZE;

                _header.Add(_title);

                Add(_header);
            }
            HeaderDisabled = disableHeader;
        }

        public int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                if (!HeaderDisabled)
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
                Position = new Point(X + offset.X, Y + offset.Y);

                onMove?.Invoke((int)offset.X, (int)offset.Y);
                _lastMousePos = currentPos;
                UpdateControlsPos();
                _header.UpdateControlsPos();
                foreach (IContainer container in _controls.OfType<IContainer>())
                {
                    container.UpdateControlsPos();
                }
            }
            else if (_resizing)
            {
                Point currentPos = Input.CursorPos;

                Point offset = new Point(currentPos.X - _lastMousePos.X, currentPos.Y - _lastMousePos.Y);
                Width += (int)offset.X;
                Height += (int)offset.Y;

                onResizing?.Invoke((int)offset.X, (int)offset.Y);
                _lastMousePos = currentPos;
            }
            if (!HeaderDisabled && Focused && _header.Rect.IsMouseOn())
            {
                if (Input.MouseLeft.state == Input.KeyState.PRESSED)
                {
                    _dragging = true;
                    _lastMousePos = Input.CursorPos;
                }
            }
            else if (Focused && Resizable && Input.MouseLeft.state == Input.KeyState.PRESSED && new Rectangle(Rect.Right - 15, Rect.Bottom - 15, Rect.Right, Rect.Bottom).IsMouseOn())
            {
                _resizing = true;
                _lastMousePos = Input.CursorPos;
            }
            if (_dragging && Input.MouseLeft.state == Input.KeyState.NONE)
            {
                _dragging = false;
            }
            if (_resizing && Input.MouseLeft.state == Input.KeyState.NONE)
            {
                _resizing = false;
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
            {
                base.Draw(g);
            }
            if (Overlay.drawUI && Resizable)
            {
                if (Resizable)
                {
                    g.FillRoundedRectangle(_resizing ? Brushes.UIActiveColor : Brushes.UIActiveColorSecond, Rect.Right - 15, Rect.Bottom - 4, Rect.Right - 2, Rect.Bottom - 2, 1);
                    g.FillRoundedRectangle(_resizing ? Brushes.UIActiveColor : Brushes.UIActiveColorSecond, Rect.Right - 4, Rect.Bottom - 15, Rect.Right - 2, Rect.Bottom - 2, 1);
                }
            }
        }

        public override void ApplyConfig()
        {
            base.ApplyConfig();
            UpdateControlsPos();
            _header.UpdateControlsPos();
        }

        public virtual void FocusChanged()
        {

        }

        public void SetTitle(string text)
        {
            _title.Text = text;
        }

        public void Focus()
        {
            BrushBorder = Brushes.UIActiveColor;
            FocusedForm = this;
            _layerIndex++;
            Layer = _layerIndex;
            FocusedFrame = true;
            Overlay.Current.FormFocusChanged();
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
                Overlay.Current.FormFocusChanged();

                return true;
            }
            return false;
        }

        public void Close() => Overlay.Current.RemoveForm(this);

        public static UIForm? FocusedForm
        {
            get => _focusedForm;
            set
            {
                _focusedForm = value!;

            }
        }

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
            get; set;
        }

        public bool Resizable
        {
            get; set;
        }

        public bool HeaderDisabled
        {
            get; private set;
        }

        public int Y
        {
            get => base.Y;
            set
            {
                if (value > Overlay.ScreenSize.y - HEADER_SIZE)
                {
                    base.Y = Overlay.ScreenSize.y - HEADER_SIZE;
                }
                else
                    base.Y = value;
            }
        }

        public Point Position
        {
            get => base.Position;
            set
            {
                int y = (int)value.Y;

                if (value.Y > Overlay.ScreenSize.y - HEADER_SIZE - 2)
                {
                    y = Overlay.ScreenSize.y - HEADER_SIZE - 2;
                }
                else
                    y = (int)value.Y;

                base.Position = new Point(value.X, y);
            }
        }

        /// <summary>
        /// <para>не позиция формы, а её изменение по X и Y</para>
        /// <para>1 int — X</para>
        /// <para>2 int — Y</para>
        /// </summary>
        public Action<int, int> onMove = null!;
        /// <summary>
        /// <para>не размер формы, а изменение Width и Height</para>
        /// <para>1 int — W</para>
        /// <para>2 int — H</para>
        /// </summary>
        public Action<int, int> onResizing = null!;

        private static uint _layerIndex;
        private bool _dragging;
        private bool _resizing;
        private Point _lastMousePos;
        private UIPanel _header;
        private UILabel _title;

        private static UIForm _focusedForm = null!;

        public const int HEADER_SIZE = 25;
    }
}
