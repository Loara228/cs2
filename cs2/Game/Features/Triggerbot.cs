using cs2.Config;
using cs2.Game.Objects;
using cs2.Game.Structs;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Features
{
    internal static class Triggerbot
    {
        public static void Update(Input.KeyState state)
        {
            if (state != Input.KeyState.DOWN)
                return;

            var entityId = Memory.Read<int>(Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerPawn) +
                                                         C_CSPlayerPawnBase.m_iIDEntIndex);

            if (entityId < 0) return;

            var entityEntry = Memory.Read<IntPtr>(Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwEntityList) +
                                                               0x8 * (entityId >> 9) + 0x10);
            var entity = Memory.Read<IntPtr>(entityEntry + 120 * (entityId & 0x1FF));
            Team entityTeam = (Team)Memory.Read<int>(entity + C_BaseEntity.m_iTeamNum);


            if (entityTeam == Team.CounterTerrorist || entityTeam == Team.Terrorist)
            {
                if (Config.Configuration.Current.DM_Mode_Enabled ? true : entityTeam != LocalPlayer.Current.Team)
                    Shot();
            }
        }

        public static void Shot()
        {
            Input.MouseClick();
            AimAssist.Waiting = true;
            Thread.Sleep(50);
            AimAssist.Waiting = false;
            //AimAssist._targetBone = Bone.UNKNOWN;
            //AimAssist._targetPos = System.Numerics.Vector3.Zero;
            //AimAssist._targetPtr = IntPtr.Zero;
        }
    }
//(byte) m_nHitboxSet = CSkeletonInstance->m_nHitboxSet
//https://www.unknowncheats.me/forum/counter-strike-2-a/612049-vector-rotate-transform-hitbox.html
}
