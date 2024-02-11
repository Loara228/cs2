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

        }

        protected override nint ReadAddressBase()
        {
            return Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerPawn);
        }

        protected override nint ReadControllerBase()
        {
            return Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwLocalPlayerController);
        }
        public override bool Update()
        {
            if (!base.Update()) return false;


            MatrixViewProjection = Matrix.Transpose(Memory.Read<Matrix>(Memory.ClientPtr + ClientOffsets.dwViewMatrix));
            MatrixViewport = Matrix.GetMatrixViewport(new System.Drawing.Size(1920, 1080));
            MatrixViewProjectionViewport = MatrixViewProjection * MatrixViewport;

            IntPtr entitySpottedStatePtr = Memory.Read<IntPtr>(AddressBase + C_CSPlayerPawnBase.m_entitySpottedState);
            SpottedState = Memory.Read<EntitySpottedState_t>(entitySpottedStatePtr);


            return true;
        }

        private Matrix MatrixViewProjection { get; set; }
        public Matrix MatrixViewport { get; private set; }
        public Matrix MatrixViewProjectionViewport { get; private set; }

        public EntitySpottedState_t SpottedState { get; private set; }
    }
}
