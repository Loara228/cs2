﻿using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Controls
{
    internal class UISlider : UIControl
    {
        public UISlider(float value, int width = 100)
        {
            MinHeight = 0;

            Width = width;
            Height = 4;
            Margin = new Margin(10, 8, 9);

            _circle = new Circle(-1, -1, 6);

            _value = value;
        }

        /// <param name="owner">auto width</param>
        public UISlider(float value, UIForm owner)
        {
            MinHeight = 0;

            Height = 4;
            Margin = new Margin(10, 8, 14);

            Width = owner.Width - Margin.Left - Margin.Right;

            _circle = new Circle(-1, -1, 6);

            _value = value;
        }

        /// <param name="owner">auto width</param>
        public UISlider(float value, UIForm owner, float maxValue) : this(value, owner)
        {
            MaxValue = maxValue;
        }

        public override void Update()
        {
            base.Update();

            if (_dragging)
            {
                int mouseX = (int)Input.CursorPos.X;
                if (mouseX < X)
                    mouseX = X;
                else if (mouseX > Rect.Right)
                    mouseX = (int)Rect.Right;

                _circle.Location.X = mouseX;

                PosToValue(mouseX);

                if (Input.MouseLeft.state != Input.KeyState.DOWN)
                    _dragging = false;
            }

            if (Input.MouseLeft.state == Input.KeyState.PRESSED)
            {
                if (new Rectangle(Rect.Left, Rect.Top - _circle.Radius, Rect.Right, Rect.Bottom + _circle.Radius).IsMouseOn() ||
                    _circle.IsMouseOn())
                {
                    _dragging = true;
                }
            }
        }

        public override void Draw(Graphics g)
        {
            if (!_dragging)
            {
                UpdateCirclePos();
            }

            base.Draw(g);
            g.FillRoundedRectangle(Brushes.UIBorderColor, new RoundedRectangle(Rect, Height / 2));
            g.FillCircle(_dragging ? Brushes.UIActiveColor : Brushes.UIActiveColorSecond, _circle);
        }

        private void PosToValue(int mouseX)
        {
            Value = (mouseX - Rect.Left) / Width * MaxValue;
        }

        protected void UpdateCirclePos()
        {
            float mouseX = Width / MaxValue * Value;
            _circle.Location = new Point(X + mouseX, Y + Height / 2);
        }

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                onValueChanged?.Invoke(value);
            }
        }

        public float MaxValue { get; set; } = 100f;

        public Action<float> onValueChanged = null!;

        protected Circle _circle;
        private float _value;
        protected bool _dragging;
    }
}
