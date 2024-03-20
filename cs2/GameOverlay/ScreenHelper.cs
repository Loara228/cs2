using cs2.PInvoke;
using GameOverlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2.GameOverlay
{

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics

    public class ScreenHelper
    {
        public static Rectangle GetBounds()
        {
            if (User32.GetSystemMetrics(80) != 0)
            {
                return new Rectangle(
                    User32.GetSystemMetrics(76),
                    User32.GetSystemMetrics(77),
                    User32.GetSystemMetrics(78),
                    User32.GetSystemMetrics(79));
            }
            return new Rectangle(0, 0, User32.GetSystemMetrics(0), User32.GetSystemMetrics(1));
        }
    }
}
