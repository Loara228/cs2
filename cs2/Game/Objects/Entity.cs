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
        public Entity(int index, bool bonesOnly = false)
        {
            this.Index = index;
            this.BonesOnly = bonesOnly;

            this.Weapon = new Weapon();
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
            PlayerPawn = Memory.Read<int>(ControllerBase + CBasePlayerController.m_hPawn);
            var listEntrySecond = Memory.Read<IntPtr>(EntityList + 0x8 * ((PlayerPawn & 0x7FFF) >> 9) + 16);
            return listEntrySecond == IntPtr.Zero
                ? IntPtr.Zero
                : Memory.Read<IntPtr>(listEntrySecond + 120 * (PlayerPawn & 0x1FF));
        }

        public override bool Update()
        {
            if (!base.Update()) return false;
            if (Program.LocalPlayer.AddressBase == AddressBase)
                return true;

            UpdateBones();

            Dormant = Memory.Read<bool>(AddressBase + CGameSceneNode.m_bDormant);

            if (BonesOnly)
                return true;

            Nickname = Memory.ReadString(ControllerBase + CBasePlayerController.m_iszPlayerName);
            WeaponPtr = Memory.Read<IntPtr>(AddressBase + C_CSPlayerPawnBase.m_pClippingWeapon);
            Weapon.Update(WeaponPtr);

            IntPtr entitySpottedStatePtr = Memory.Read<IntPtr>(AddressBase + C_CSPlayerPawnBase.m_entitySpottedState);
            SpottedState = Memory.Read<EntitySpottedState_t>(entitySpottedStatePtr);

            IsScoped = Memory.Read<bool>(AddressBase + C_CSPlayerPawnBase.m_bIsScoped) && Weapon.IsSniperRifle;
            IsDefusing = Memory.Read<bool>(AddressBase + C_CSPlayerPawnBase.m_bIsDefusing);

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
                Bones[i] = new(bone.bone, bonePos, bone.id);
                if (i == 0)
                    HeadPos = bonePos;
            }
        }

        public bool CheckVisable()
        {
            //int v1 = (int)SpottedState.GetSpottedByMask() & (int)(1 << (int)Program.LocalPlayer.AddressBase);
            //int v2 = (int)Program.LocalPlayer.SpottedState.GetSpottedByMask() & (int)(1 << PlayerPawn);
            return SpottedState.m_bSpotted;
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && !Dormant;
        }

        public Weapon Weapon { get; private set; }
        private IntPtr WeaponPtr { get; set; }
        public Vector3 HeadPos { get; private set; } = Vector3.Zero;
        public List<(Bone bone, Vector3 pos, int id)> Bones
        {
            get; set;
        } = new List<(Bone bone, Vector3 pos, int id)>()
            {
                (Bone.head, Vector3.Zero, 6),           // 0
                (Bone.neck_0, Vector3.Zero, 5),         // 1
                (Bone.spine_1, Vector3.Zero, 4),        // 2
                (Bone.spine_2, Vector3.Zero, 2),        // 3
                (Bone.pelvis, Vector3.Zero, 0),         // 4
                (Bone.arm_upper_L, Vector3.Zero, 8),    // 5
                (Bone.arm_lower_L, Vector3.Zero, 9),    // 6
                (Bone.hand_L, Vector3.Zero, 10),        // 7
                (Bone.arm_upper_R, Vector3.Zero, 13),   // 8
                (Bone.arm_lower_R, Vector3.Zero, 14),   // 9
                (Bone.hand_R, Vector3.Zero, 15),        // 10
                (Bone.leg_upper_L, Vector3.Zero, 22),   // 11
                (Bone.leg_lower_L, Vector3.Zero, 23),   // 12
                (Bone.ankle_L, Vector3.Zero, 24),       // 13
                (Bone.leg_upper_R, Vector3.Zero, 25),   // 14
                (Bone.leg_lower_R, Vector3.Zero, 26),   // 15
                (Bone.ankle_R, Vector3.Zero, 27)        // 16
            };
        public EntitySpottedState_t SpottedState { get; private set; }
        public int PlayerPawn {  get; set; }
        public int Index { get; private set; }
        public string Nickname { get; private set; }
        public bool IsSpotted { get; private set; }
        public bool IsDefusing { get; private set; }
        public bool IsScoped { get; private set; }
        private bool Dormant { get; set; }
        private bool BonesOnly { get; set; }
    }
}
