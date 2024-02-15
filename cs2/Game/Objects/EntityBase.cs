using cs2.Game.Structs;
using cs2.Offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static cs2.Offsets.OffsetsLoader;

namespace cs2.Game.Objects
{
    internal abstract class EntityBase
    {

        public virtual bool IsAlive()
        {
            return ControllerBase != IntPtr.Zero &&
                   AddressBase != IntPtr.Zero &&
                   LifeState &&
                   Health > 0 &&
                   Team is not Team.UNKNOWN;
        }

        public abstract IntPtr ReadControllerBase();
        public abstract IntPtr ReadAddressBase();

        public virtual bool Update()
        {
            EntityList = Memory.Read<IntPtr>(Memory.ClientPtr + ClientOffsets.dwEntityList);
            ControllerBase = ReadControllerBase();
            AddressBase = ReadAddressBase();
            if (ControllerBase == IntPtr.Zero || AddressBase == IntPtr.Zero) return false;

            LifeState = Memory.Read<bool>(AddressBase + C_BaseEntity.m_lifeState);
            Health = Memory.Read<int>(AddressBase + C_BaseEntity.m_iHealth);
            Team = (Team)Memory.Read<int>(AddressBase + C_BaseEntity.m_iTeamNum);
            Origin = Memory.Read<Vector3>(AddressBase + C_BasePlayerPawn.m_vOldOrigin);

            return true;
        }

        /// <summary>
        /// PlayerPawn
        /// </summary>
        public IntPtr AddressBase { get; set; }

        protected IntPtr EntityList { get; set; }
        public IntPtr ControllerBase { get; set; }

        private bool LifeState { get; set; }
        public int Health { get; set; }
        public Team Team { get; private set; }
        public Vector3 Origin { get; set; }
    }
}
