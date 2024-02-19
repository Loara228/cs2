using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal abstract class UIContainer : UIControl, IContainer
    {
        public UIContainer(int x, int y) : base(x, y)
        {
            _controls = new List<UIControl>();
        }
        public UIContainer(int x, int y, IBrush backgroundBrush) : base(x, y, backgroundBrush)
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
            control.X = this.X + control.Margin.Left;
            if (first)
                control.Y = lastControl.Y + control.Margin.Top;
            else
                control.Y = (int)lastControl.Rect.Bottom + lastControl.Margin.Bottom + control.Margin.Top;
            _controls.Add(control);

            int h = (control.Y + control.Height + control.Margin.Bottom) - (int)Rect.Top;
            Height = h;
            return h;
        }

        public override void Update()
        {
            base.Update();
            _controls.ForEach(x => x.Update());
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
                control.X = this.X + control.Margin.Left;
                if (i == 0)
                    control.Y = lastControl.Y + control.Margin.Top;
                else
                    control.Y = (int)lastControl.Rect.Bottom + lastControl.Margin.Bottom + control.Margin.Top;
            }
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            _controls.ForEach(x => x.Draw(g));
        }

        public IReadOnlyCollection<UIControl> Controls { get => _controls.AsReadOnly(); set { throw new Exception("u cannot"); }  } 

        protected List<UIControl> _controls;
    }
}
