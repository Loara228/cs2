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
        public UIForm(Rectangle rect = new Rectangle(), string label = "form") : base(rect, Brushes.UIBackgroundColor)
        {
            Offset = 0;

            _header = new UIPanel(new Rectangle(rect.Left, rect.Top, 0, 0), Brushes.UIHeaderColor);
            _header.Width = (int)(rect.Right - rect.Left);
            _header.Height = HEADER_SIZE;
            _header.Offset = 0;

            UILabel uiLabel = new UILabel(Fonts.Consolas, Brushes.UITextColor, 14, label);
            _header.Add(uiLabel);

            this.Add(_header);
        }

        public int Width
        {
            get => base.Width;
            set
            {
                base.Width = value;
                _header.Width = value;
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
            if (_dragging)
            {
                Point currentPos = Input.CursorPos;

                Point offset = new Point(currentPos.X - _lastMousePos.X, currentPos.Y - _lastMousePos.Y);

                Position = new Point(X + offset.X, Y + offset.Y);

                _lastMousePos = currentPos;
                _header.UpdateControlsPos();
                UpdateControlsPos();
            }
            if (_header.Rect.IsMouseOn())
            {
                if (Input.MouseLeft.state == Input.KeyState.PRESSED)
                {
                    _dragging = true;
                    _lastMousePos = Input.CursorPos;
                }
                else if (Input.MouseLeft.state == Input.KeyState.NONE)
                {
                    _dragging = false;
                }
            }
            base.Update();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
        }

        protected bool Focused
        {
            get; set;
        }

        private bool _dragging;
        private Point _lastMousePos;

        private UIPanel _header;



        public const int HEADER_SIZE = 30;
    }
}
