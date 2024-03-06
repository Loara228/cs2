using cs2.GameOverlay.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormVisualsDialog : UIForm
    {
        public FormVisualsDialog(UIForm owner) : base(0, 0, "", true)
        {
            this._owner = owner;
            Width = 200;

            Add(new UILabel("Draw", true) { FontSize = 12 });
            Add(_radioButton_drawRect = new UIRadioButton("Rectangle", this));
            Add(_radioButton_drawEdges = new UIRadioButton("Rectangle edges", this));
            Add(_radioButton_drawRouned = new UIRadioButton("Rounded rectangle", this));
            Add(_radioButton_drawOutline = new UIRadioButton("Outline", this));
            Add(new UILabel("Fill", true) { FontSize = 12 });
            Add(_radioButton_fillRect = new UIRadioButton("Rectangle", this));
            Add(_radioButton_fillRounded = new UIRadioButton("RoundedRectangle", this));
            Add(_radioButton_fillOutline = new UIRadioButton("Outline", this));

            void AddAction(UIRadioButton sender, Config.ESP_Box_Type type)
            {
                sender.OnChecked += (flag) =>
                {
                    if (flag)
                        Config.Configuration.Current.ESP_Box_Type = type;
                };
            }

            AddAction(_radioButton_drawRect, Config.ESP_Box_Type.DrawRectangle);
            AddAction(_radioButton_drawEdges, Config.ESP_Box_Type.DrawRectangleEdges);
            AddAction(_radioButton_drawRouned, Config.ESP_Box_Type.DrawRoundedRectangle);

            AddAction(_radioButton_fillRect, Config.ESP_Box_Type.FillRectangle);
            AddAction(_radioButton_fillRounded, Config.ESP_Box_Type.FillRoundedRectangle);
            AddAction(_radioButton_fillOutline, Config.ESP_Box_Type.OutlineFillRectangle);

            AddAction(_radioButton_drawOutline, Config.ESP_Box_Type.OutlineRectangle);
            AddAction(_radioButton_fillOutline, Config.ESP_Box_Type.OutlineFillRectangle);

            Show();
        }

        public override void FocusChanged()
        {
            base.FocusChanged();
            if (UIForm.FocusedForm != this)
                Close();
        }

        public void Show()
        {
            X = _owner.X + 5;
            Y = _owner.Y + _owner.Height / 2 - Height / 2;
            UpdateControlsPos();
            Overlay.Current.Open(this);
        }

        private UIForm _owner;

        private UIRadioButton
            _radioButton_drawRect, _radioButton_drawEdges, _radioButton_drawRouned,
            _radioButton_fillRect, _radioButton_fillRounded, _radioButton_fillOutline, _radioButton_drawOutline;
    }
}
