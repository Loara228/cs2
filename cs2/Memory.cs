using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2
{
    internal static class Memory
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out][MarshalAs(UnmanagedType.AsAny)] object lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        public static bool Initialize()
        {
            var procList = Process.GetProcessesByName("cs2");
            if (procList.Length == 0)
                return false;
            _proc = procList[0];
            foreach(ProcessModule module in _proc.Modules)
            {
                try
                {
                    if (module.ModuleName == "client.dll")
                    {
                        ClientPtr = module.BaseAddress;
                        return true;
                    }
                }
                catch { };
            }
            throw new DllNotFoundException("client.dll");
        }

        public static T Read<T>(IntPtr Address) where T : unmanaged
        {
            var size = Marshal.SizeOf<T>();
            var buffer = default(T) as object;
            ReadProcessMemory(_proc.Handle, Address, buffer, size, out var lpNumberOfBytesRead);
            return lpNumberOfBytesRead == size ? (T)buffer : default;
        }

        public  static byte[] ReadBytes(IntPtr hProcess, IntPtr lpBaseAddress, int maxLength)
        {
            var buffer = new byte[maxLength];
            ReadProcessMemory(hProcess, lpBaseAddress, buffer, maxLength, out _);
            return buffer;
        }
        public static string ReadString(IntPtr lpBaseAddress, int maxLength = 256)
        {
            var buffer = ReadBytes(_proc.Handle, lpBaseAddress, maxLength);
            var nullCharIndex = Array.IndexOf(buffer, (byte)'\0');
            return nullCharIndex >= 0
                ? Encoding.UTF8.GetString(buffer, 0, nullCharIndex).Trim()
                : Encoding.UTF8.GetString(buffer).Trim();
        }

        public static IntPtr ClientPtr
        {
            get; set;
        }

        private static Process _proc = null!;
    }
}
