using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2
{
    internal static class Keyboard
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool KeyDown(int vKey) => GetAsyncKeyState(vKey) < 0;

        public enum KeyState : sbyte
        {
            NONE = 0,
            PRESSED = 1,
            DOWN = 2,
            RELEASE = 3
        }
    }
}
