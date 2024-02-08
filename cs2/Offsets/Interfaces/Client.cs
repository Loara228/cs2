using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Offsets.Interfaces
{
    internal class Client : InterfaceBase
    {
        public Client() : base("client_dll ")
        {
        }

        public nint dwEntityList = 0x0;
        public nint dwForceAttack = 0x0;
        public nint dwForceAttack2 = 0x0;
        public nint dwForceBackward = 0x0;
        public nint dwForceCrouch = 0x0;
        public nint dwForceForward = 0x0;
        public nint dwForceJump = 0x0;
        public nint dwForceLeft = 0x0;
        public nint dwForceRight = 0x0;
        public nint dwGameEntitySystem = 0x0;
        public nint dwGameEntitySystem_getHighestEntityIndex = 0x0;
        public nint dwGameRules = 0x0;
        public nint dwGlobalVars = 0x0;
        public nint dwGlowManager = 0x0;
        public nint dwInterfaceLinkList = 0x0;
        public nint dwLocalPlayerController = 0x0;
        public nint dwLocalPlayerPawn = 0x0;
        public nint dwPlantedC4 = 0x0;
        public nint dwPrediction = 0x0;
        public nint dwSensitivity = 0x0;
        public nint dwSensitivity_sensitivity = 0x0;
        public nint dwViewAngles = 0x0;
        public nint dwViewMatrix = 0x0;
        public nint dwViewRender = 0x0;
    }
}
