using cs2.Offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game
{
    internal static class GlobalVars
    {
        public static void Update()
        {
            IntPtr globalVarsPtr = Memory.Read<IntPtr>(Memory.ClientPtr + OffsetsLoader.ClientOffsets.dwGlobalVars);
            CurrentTime = Memory.Read<float>(globalVarsPtr + 0x2C);
            CurrentTime2 = Memory.Read<float>(globalVarsPtr + 0x30);
        }

        public static float CurrentTime { get; private set; }
        public static float CurrentTime2 { get; private set; }
    }
}


//DWORD RealTime = 0x00;
//DWORD FrameCount = 0x04;
//DWORD MaxClients = 0x10;
//DWORD IntervalPerTick = 0x14;
//DWORD CurrentTime = 0x2C;
//DWORD CurrentTime2 = 0x30;
//DWORD TickCount = 0x40;
//DWORD IntervalPerTick2 = 0x44;
//DWORD CurrentNetchan = 0x0048;
//DWORD CurrentMap = 0x0180;
//DWORD CurrentMapName = 0x0188;

//[StructLayout(LayoutKind.Sequential)]
//public unsafe struct globalVars_t
//{
//    float real_time;
//    int frame_count;
//    fixed byte padding_0[0x8];
//    int max_clients;
//    float interval_per_tick;
//    fixed byte padding_1[0x14];
//    float current_time;
//    float current_time_2;
//    fixed byte padding_2[0xC];
//    int tick_count;
//    float interval_per_tick_2;
//    fixed byte padding_3[0x138];
//    ulong current_map;
//    ulong current_map_name;
//}