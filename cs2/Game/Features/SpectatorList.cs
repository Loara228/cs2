using cs2.Game.Objects;
using cs2.GameOverlay;
using GameOverlay.Drawing;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class SpectatorList
    {
        public static List<string> Get()
        {
            if (!LocalPlayer.Current.IsAlive())
                return new List<string>() { "empty" };

            List<string> spectators = new List<string>();
            foreach (var entity in Program.Entities)
            {
                if (entity.AddressBase == 0 || entity.IsAlive() || entity.Team != LocalPlayer.Current.Team)
                    continue;

                IntPtr obs = Memory.Read<IntPtr>(entity.AddressBase + C_BasePlayerPawn.m_pObserverServices);
                IntPtr playerPawn = Memory.Read<IntPtr>(obs + CPlayer_ObserverServices.m_hObserverTarget);
                IntPtr addressBase = ReadAddressBase(playerPawn);

                if (addressBase == LocalPlayer.Current.AddressBase)
                    spectators.Add(entity.Nickname);
            }

            return spectators;
        }

        private static IntPtr ReadAddressBase(IntPtr playerPawn)
        {
            IntPtr entityList = Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwEntityList);
            var listEntrySecond = Memory.Read<IntPtr>(entityList + 0x8 * ((playerPawn & 0x7FFF) >> 9) + 16);
            return listEntrySecond == IntPtr.Zero
                ? IntPtr.Zero
                : Memory.Read<IntPtr>(listEntrySecond + 120 * (playerPawn & 0x1FF));
        }
    }
}
