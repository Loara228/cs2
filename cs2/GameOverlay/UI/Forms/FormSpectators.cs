using cs2.GameOverlay.UI.Controls;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay.UI.Forms
{
    internal class FormSpectators : UIForm
    {
        public FormSpectators() : base(new Rectangle(30, 30, 0, 0), "Spec")
        {
            this.MinWidth = 300;
            this.MinHeight = 60;
            this.Width = 300;
            this.Height = 300;

            InitializeComponents();
        }

        private void InitializeComponents()
        {
            UIButton testButton;
            Add(testButton = new UIButton(Brushes.UIHeaderColor, Brushes.UITextColor, Fonts.Consolas, 12, "BUTTON"));
            UIButton testButton2;
            Add(testButton2 = new UIButton(Brushes.UIHeaderColor, Brushes.UITextColor, Fonts.Consolas, 12, "BUTTON"));
            testButton.Clicked += new Action(() => { Console.WriteLine("ButtonClicked"); });
        }
    }
}
