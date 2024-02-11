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
        public UIContainer(Rectangle rect, IBrush backgroundBrush) : base(rect, backgroundBrush)
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
            control.X = this.X + control.Offset;
            if (first)
                control.Y = lastControl.Y + control.Offset;
            else
                control.Y = (int)lastControl.Rect.Bottom + lastControl.Offset + control.Offset;
            Controls.Add(control);
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
                control.X = this.X + control.Offset;
                if (i == 0)
                    control.Y = lastControl.Y + control.Offset;
                else
                    control.Y = (int)lastControl.Rect.Bottom + lastControl.Offset + control.Offset;
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
