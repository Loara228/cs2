using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static cs2.Input;

namespace cs2.PInvoke
{
    internal static class User32
    {
        [DllImport("User32.dll")]
        internal static extern short GetAsyncKeyState(int vKey);

        [DllImport("User32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [DllImport("User32.dll")]
        internal static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("User32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("User32.dll")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // https://github.com/dotnet/pinvoke/blob/main/src/User32/User32%2BWindowMessage.cs#L18
        [DllImport("User32.dll")]
        public static extern unsafe IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            SW_HIDE = 0,

            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            SW_SHOWNORMAL = 1,

            /// <summary>Activates the window and displays it as a minimized window.</summary>
            SW_SHOWMINIMIZED = 2,

            /// <summary>Activates the window and displays it as a maximized window.</summary>
            SW_SHOWMAXIMIZED = 3,

            /// <summary>Maximizes the specified window.</summary>
            SW_MAXIMIZE = 3,

            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            SW_SHOWNOACTIVATE = 4,

            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            SW_SHOW = 5,

            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            SW_MINIMIZE = 6,

            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            SW_SHOWMINNOACTIVE = 7,

            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            SW_SHOWNA = 8,

            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            SW_RESTORE = 9,

            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            SW_SHOWDEFAULT = 10,

            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            SW_FORCEMINIMIZE = 11,
        }
        //-20
        [Flags]
        internal enum ExtendedWindowStyle : uint
        {
            // Token: 0x040000D2 RID: 210
            Left = 0U,
            // Token: 0x040000D3 RID: 211
            LtrReading = 0U,
            // Token: 0x040000D4 RID: 212
            RightScrollbar = 0U,
            // Token: 0x040000D5 RID: 213
            DlgModalFrame = 1U,
            // Token: 0x040000D6 RID: 214
            NoParentNotify = 4U,
            // Token: 0x040000D7 RID: 215
            Topmost = 8U,
            // Token: 0x040000D8 RID: 216
            AcceptFiles = 16U,
            // Token: 0x040000D9 RID: 217
            Transparent = 32U,
            // Token: 0x040000DA RID: 218
            MdiChild = 64U,
            // Token: 0x040000DB RID: 219
            ToolWindow = 128U,
            // Token: 0x040000DC RID: 220
            WindowEdge = 256U,
            // Token: 0x040000DD RID: 221
            PaletteWindow = 392U,
            // Token: 0x040000DE RID: 222
            ClientEdge = 512U,
            // Token: 0x040000DF RID: 223
            OverlappedWindow = 768U,
            // Token: 0x040000E0 RID: 224
            ContextHelp = 1024U,
            // Token: 0x040000E1 RID: 225
            Right = 4096U,
            // Token: 0x040000E2 RID: 226
            RtlReading = 8192U,
            // Token: 0x040000E3 RID: 227
            LeftScrollbar = 16384U,
            // Token: 0x040000E4 RID: 228
            ControlParent = 65536U,
            // Token: 0x040000E5 RID: 229
            StaticEdge = 131072U,
            // Token: 0x040000E6 RID: 230
            AppWindow = 262144U,
            // Token: 0x040000E7 RID: 231
            Layered = 524288U,
            // Token: 0x040000E8 RID: 232
            NoInheritLayout = 1048576U,
            // Token: 0x040000E9 RID: 233
            NoRedirectionBitmap = 2097152U,
            // Token: 0x040000EA RID: 234
            LayoutRtl = 4194304U,
            // Token: 0x040000EB RID: 235
            Composited = 33554432U,
            // Token: 0x040000EC RID: 236
            NoActivate = 134217728U
        }
    }
}
