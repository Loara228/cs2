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
        public UIControl()
        {
            this.Rect = new Rectangle();
        }

        public UIControl(Rectangle rect, IBrush backgroundBrush)
        {
            this.Rect = rect;
            this.BrushBackground = backgroundBrush;

            DrawBackground = true;
        }

        public UIControl(int x, int y)
        {
            this.Rect = new Rectangle(x, y, x + 30, y + 30);
        }

        public UIControl(int x, int y, IBrush backgroundBrush)
        {
            this.Rect = new Rectangle(x, y, x + 30, y + 30);
            this.BrushBackground = backgroundBrush;

            DrawBackground = true;
        }


        public virtual void Draw(Graphics g)
        {
            if (DrawBackground)
                g.FillRectangle(BrushBackground, Rect);
            if (BrushBorder != null)
                g.DrawRectangle(BrushBorder, Rect, BorderSize);
        }

        public virtual void Update()
        {
        }

        public virtual void ApplyConfig()
        {

        }

        public Rectangle Rect
        {
            get; protected set;
        }

        public IBrush BrushBackground
        {
            get; set;
        } = null!;

        public IBrush? BrushBorder
        {
            get; set;
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
        } = 10;

        public int MinHeight
        {
            get; set;
        } = 10;

        public bool DrawBackground
        {
            get; protected set;
        }

        public float BorderSize
        {
            get; set;
        } = 1;

        /// <summary>
        /// Default = 5
        /// </summary>
        public Margin Margin
        {
            get; set;
        } = new(5);

        public static Graphics? initGraphics;

        private int _width, _height;
    }
}
