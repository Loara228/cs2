using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI
{
    internal abstract class UIControl : IComponent
    {
        public UIControl(Rectangle rect, IBrush backgroundBrush)
        {
            this.Rect = rect;
            this.Brush = backgroundBrush;

            DrawBackground = true;

        }

        public UIControl(Rectangle rect, IBrush backgroundBrush, int margin = 5, int minWidth = 0, int minHeight = 0)
        {
            this.Rect = rect;
            this.Brush = backgroundBrush;
            this.Offset = margin;
            this.MinWidth = minWidth;
            this.MinHeight = minHeight;

            DrawBackground = true;
        }

        public virtual void Draw(Graphics g)
        {
            if (DrawBackground)
                g.FillRectangle(Brush, Rect);
        }

        public virtual void Update()
        {
        }

        public UIControl? Owner
        {
            get; protected set;
        }

        public Rectangle Rect
        {
            get; private set;
        }

        public IBrush Brush
        {
            get; private set;
        } = null!;

        public int X
        {
            get => (int)Rect.Left;
            set => Rect = new Rectangle(value, Rect.Top, value + Width, Rect.Top + Height);
        }

        public int Y
        {
            get => (int)Rect.Top;
            set => Rect = new Rectangle(Rect.Left, value, Rect.Left + Width, value + Height);
        }

        public Point Position
        {
            get => new Point(Rect.Left, Rect.Top);
            set => Rect = new Rectangle(value.X, value.Y, value.X + Width, value.Y + Height);
        }

        public int Width
        {
            get
            {
                if (MinWidth > _width)
                    return MinWidth;
                return _width;
            }
            set
            {
                _width = value;
                Rect = new Rectangle(X, Y, X + Width, Y + Height);
            }
        }

        public int Height
        {
            get
            {
                if (MinHeight > _height)
                    return MinHeight;
                return _height;
            }
            set
            {
                _height = value;
                Rect = new Rectangle(X, Y, X + Width, Y + Height);
            }
        }

        public int MinWidth
        {
            get; set;
        } = 30;

        public int MinHeight
        {
            get; set;
        } = 30;

        public bool DrawBackground
        {
            get; protected set;
        }

        /// <summary>
        /// Default = 5
        /// </summary>
        public int Offset
        {
            get; set;
        } = 5;

        private int _width, _height;
    }
}
