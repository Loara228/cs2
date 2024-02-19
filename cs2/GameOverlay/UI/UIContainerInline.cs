using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal class UIContainerInline : UIControl, IContainer
    {
        public UIContainerInline() : base()
        {
            _controls = new List<UIControl>();
        }
        public UIContainerInline(int x, int y) : base(x, y)
        {
            _controls = new List<UIControl>();
        }

        public UIContainerInline(int x, int y, IBrush backgroundBrush) : base(x, y, backgroundBrush)
        {
            _controls = new List<UIControl>();
        }
        
        /// <returns>on form pos: control.Y + control.Height + control.Margin.Bottom</returns>
        public int Add(UIControl control)
        {
            UIControl lastControl = null;
            bool first = false;

            if (_controls.Count == 0)
            {
                lastControl = this;
                first = true;
            }
            else
                lastControl = _controls[^1];
            control.Y = this.Y + control.Margin.Top;
            if (first)
                control.X = lastControl.X + control.Margin.Left;
            else
                control.X = (int)lastControl.Rect.Right + lastControl.Margin.Right + control.Margin.Left;
            _controls.Add(control);

            int w = (control.X + control.Width + control.Margin.Right) - (int)Rect.Left;
            int h = control.Height + control.Margin.Bottom + control.Margin.Bottom;
            if (h > Height)
                Height = h;
            Width = w;
            return w;
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            _controls.ForEach(x => x.Draw(g));
        }

        public void UpdateControlsPos()
        {
            UIControl lastControl = null;
            for (int i = 0; i < Controls.Count; i++)
            {
                UIControl control = _controls[i];
                if (i == 0)
                {
                    lastControl = this;
                }
                else
                    lastControl = _controls[i - 1];
                control.Y = this.Y + control.Margin.Top;
                if (i == 0)
                    control.X = lastControl.X + control.Margin.Left;
                else
                    control.X = (int)lastControl.Rect.Right + lastControl.Margin.Left + control.Margin.Right;
            }
        }

        public override void Update()
        {
            base.Update();
            _controls.ForEach(x => x.Update());
        }

        public IReadOnlyCollection<UIControl> Controls { get => _controls.AsReadOnly(); set { throw new Exception("u cannot"); } }

        private List<UIControl> _controls;
    }
}
