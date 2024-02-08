using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static cs2.Offsets.OffsetsLoader;
using System.Threading.Tasks;
using System.Numerics;
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
            return Memory.Read<IntPtr>(Memory.ClientPtr + Offsets.OffsetsLoader.Client.dwLocalPlayerPawn);
        }

        protected override nint ReadControllerBase()
        {
            return Memory.Read<IntPtr>(Memory.ClientPtr + Offsets.OffsetsLoader.Client.dwLocalPlayerController);
        }
        public override bool Update()
        {
            if (!base.Update()) return false;


            MatrixViewProjection = Matrix.Transpose(Memory.Read<Matrix>(Memory.ClientPtr + Client.dwViewMatrix));
            MatrixViewport = Matrix.GetMatrixViewport(new System.Drawing.Size(1920, 1080));
            MatrixViewProjectionViewport = MatrixViewProjection * MatrixViewport;

            //ViewOffset = Memory.Read<Vector3>(AddressBase + C_BaseEntity.m_vecViewOffset);
            //EyePosition = Origin + ViewOffset;
            //ViewAngles = gameProcess.ModuleClient.Read<Vector3>(Offsets.dwViewAngles);
            //AimPunchAngle = gameProcess.Process.Read<Vector3>(AddressBase + Offsets.m_AimPunchAngle);
            //TargetedEntityIndex = gameProcess.Process.Read<int>(AddressBase + Offsets.m_iIDEntIndex);
            //FFlags = gameProcess.Process.Read<int>(AddressBase + Offsets.m_fFlags);


            //EyeDirection =
            //    GraphicsMath.GetVectorFromEulerAngles(ViewAngles.X.DegreeToRadian(), ViewAngles.Y.DegreeToRadian());
            //AimDirection = AimDirection = GraphicsMath.GetVectorFromEulerAngles
            //(
            //    (ViewAngles.X + AimPunchAngle.X * Offsets.WeaponRecoilScale).DegreeToRadian(),
            //    (ViewAngles.Y + AimPunchAngle.Y * Offsets.WeaponRecoilScale).DegreeToRadian()
            //);

            return true;
        }

        private Matrix MatrixViewProjection { get; set; }
        public Matrix MatrixViewport { get; private set; }
        public Matrix MatrixViewProjectionViewport { get; private set; }
        private Vector3 ViewOffset { get; set; }
        public Vector3 EyePosition { get; private set; }
        private Vector3 ViewAngles { get; set; }
        public Vector3 AimPunchAngle { get; private set; }
        public Vector3 AimDirection { get; private set; }
        public int TargetedEntityIndex { get; private set; }
    }
}
