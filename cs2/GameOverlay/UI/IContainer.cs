using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal abstract class UIContainer : UIControl
    {
        //public UIContainer(Rectangle rect, IBrush backgroundBrush) : base(rect, backgroundBrush)
        //{
        //    Controls = new List<UIControl>();
        //}
        public UIContainer(int x, int y) : base(x, y)
        {
            Controls = new List<UIControl>();
        }
        public UIContainer(int x, int y, IBrush backgroundBrush) : base(x, y, backgroundBrush)
        {
            Controls = new List<UIControl>();
        }

        public void Add(UIControl control)
        {
            UIControl lastControl = null;
            bool first = false;

            if (Controls.Count == 0)
            {
                lastControl = this;
                first = true;
            }
            else
                lastControl = Controls[^1];
            control.X = this.X + control.Margin.Left;
            if (first)
                control.Y = lastControl.Y + control.Margin.Top;
            else
                control.Y = (int)lastControl.Rect.Bottom + lastControl.Margin.Bottom + control.Margin.Top;
            Controls.Add(control);
            UpdateControlsPos();
        }

        public override void Update()
        {
            base.Update();

            Controls.ForEach(x => x.Update());
        }

        internal void UpdateControlsPos()
        {
            UIControl lastControl = null;
            for (int i = 0; i < Controls.Count; i++)
            {
                UIControl control = Controls[i];
                if (i == 0)
                {
                    lastControl = this;
                }
                else
                    lastControl = Controls[i - 1];
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

            Controls.ForEach(x => x.Draw(g));
        }

        private List<UIControl> Controls
        {
            get; set;
        }
    }
}
