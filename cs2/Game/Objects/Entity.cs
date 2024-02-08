using cs2.Game.Features;
using cs2.Game.Structs;
using cs2.Offsets;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Objects
{
    internal class Entity : EntityBase
    {
        public Entity(int index)
        {
            this.Index = index;
        }

        protected override IntPtr ReadControllerBase()
        {
            var listEntryFirst = Memory.Read<IntPtr>(EntityList + ((8 * (Index & 0x7FFF)) >> 9) + 16);
            return listEntryFirst == IntPtr.Zero
                ? IntPtr.Zero
                : Memory.Read<IntPtr>(listEntryFirst + 120 * (Index & 0x1FF));
        }

        protected override IntPtr ReadAddressBase()
        {
            var playerPawn = Memory.Read<int>(ControllerBase + CBasePlayerController.m_hPawn);
            var listEntrySecond = Memory.Read<IntPtr>(EntityList + 0x8 * ((playerPawn & 0x7FFF) >> 9) + 16);
            return listEntrySecond == IntPtr.Zero
                ? IntPtr.Zero
                : Memory.Read<IntPtr>(listEntrySecond + 120 * (playerPawn & 0x1FF));
        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Program.LocalPlayer.AddressBase == AddressBase)
                return true;
            UpdateBones();
            Dormant = Memory.Read<bool>(AddressBase + CGameSceneNode.m_bDormant);
            Nickname = Memory.ReadString(ControllerBase + CBasePlayerController.m_iszPlayerName);
            Origin = Memory.Read<Vector3>(AddressBase + C_BasePlayerPawn.m_vOldOrigin);
            WeaponPtr = Memory.Read<IntPtr>(AddressBase + C_CSPlayerPawnBase.m_pClippingWeapon); // C_CSWeaponBase
            WeaponIndex = (WeaponDefIndex)Memory.Read<short>(WeaponPtr + 0x1098 + 0x50 + 0x1BA); // C_EconEntity.m_AttributeManager + m_Item + m_iItemDefinitionIndex 0x1158

            return true;
        }

        private void UpdateBones()
        {
            if (Team == Program.LocalPlayer.Team)
                return;
            IntPtr gameSceneNode = Memory.Read<IntPtr>(AddressBase + C_BaseEntity.m_pGameSceneNode);
            IntPtr boneArray = Memory.Read<IntPtr>(gameSceneNode + 0x160 + 128);


            var bone = Bones[0];
            IntPtr boneAddress = boneArray + bone.id * 32;
            Vector3 bonePos = Memory.Read<Vector3>(boneAddress);
            HeadPos = bonePos;

            for (int i = 0; i < Bones.Count; i++)
            {
                bone = Bones[i];
                boneAddress = boneArray + bone.id * 32;
                bonePos = Memory.Read<Vector3>(boneAddress);
                Bones[i] = new(bone.name, bonePos, bone.id);
                if (i == 0)
                    HeadPos = bonePos;
            }
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && !Dormant;
        }

        private IntPtr WeaponPtr
        {
            get; set;
        }

        public WeaponDefIndex WeaponIndex
        {
            get; set;
        }

        public Vector3 HeadPos
        {
            get; private set;
        } = Vector3.Zero;

        public Vector3 Origin
        {
            get; private set;
        } = Vector3.Zero;

        public List<(string name, Vector3 pos, int id)> Bones
        {
            get; set;
        } = new List<(string name, Vector3 pos, int id)>()
            {
                ("head", Vector3.Zero, 6),           // 0
                ("neck_0", Vector3.Zero, 5),         // 1
                ("spine_1", Vector3.Zero, 4),        // 2
                ("spine_2", Vector3.Zero, 2),        // 3
                ("pelvis", Vector3.Zero, 0),         // 4
                ("arm_upper_L", Vector3.Zero, 8),    // 5
                ("arm_lower_L", Vector3.Zero, 9),    // 6
                ("hand_L", Vector3.Zero, 10),        // 7
                ("arm_upper_R", Vector3.Zero, 13),   // 8
                ("arm_lower_R", Vector3.Zero, 14),   // 9
                ("hand_R", Vector3.Zero, 15),        // 10
                ("leg_upper_L", Vector3.Zero, 22),   // 11
                ("leg_lower_L", Vector3.Zero, 23),   // 12
                ("ankle_L", Vector3.Zero, 24),       // 13
                ("leg_upper_R", Vector3.Zero, 25),   // 14
                ("leg_lower_R", Vector3.Zero, 26),   // 15
                ("ankle_R", Vector3.Zero, 27)        // 16
            };

        public int Index { get; private set; }
        public string Nickname { get; private set; }

        private bool Dormant { get; set; }
    }
}
