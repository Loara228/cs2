using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static cs2.Offsets.OffsetsLoader;
using System.Threading.Tasks;
using System.Numerics;
using cs2.GameOverlay;
using cs2.Game.Structs;

namespace cs2.Game.Objects
{
    internal class LocalPlayer : EntityBase
    {
        public LocalPlayer()
        {
            Weapon = new Weapon();
        }

        public static void Initialize()
        {
            Current = new LocalPlayer();
        }

        public static LocalPlayer Current
        {
            get; private set;
        }

        public override nint ReadAddressBase()
        {
            return Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerPawn);
        }

        public override nint ReadControllerBase()
        {
            return Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerController);
        }
        public override bool Update()
        {
            if (!base.Update()) return false;

            MatrixViewProjection = Matrix.Transpose(Memory.Read<Matrix>(Memory.ClientPtr + ClientOffsets.dwViewMatrix));
            MatrixViewport = Matrix.GetMatrixViewport(new System.Drawing.Size(1920, 1080));
            MatrixViewProjectionViewport = MatrixViewProjection * MatrixViewport;

            Weapon.Update(Memory.Read<IntPtr>(AddressBase + C_CSPlayerPawnBase.m_pClippingWeapon));

            ViewAngles = Memory.Read<Vector3>(Memory.ClientPtr + ClientOffsets.dwViewAngles);

            IsScoped = Memory.Read<bool>(AddressBase + C_CSPlayerPawnBase.m_bIsScoped) && Weapon.IsSniperRifle;

            return true;
        }

        public Weapon Weapon { get; private set; }
        public Vector3 ViewAngles { get; private set; }
        private Matrix MatrixViewProjection { get; set; }
        public Matrix MatrixViewport { get; private set; }
        public Matrix MatrixViewProjectionViewport { get; private set; }
        public ulong SpottedMask { get; private set; }
        public bool IsScoped { get; private set; }

        public static int index = -1;
    }
}
