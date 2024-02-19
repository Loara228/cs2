using cs2.Game.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs2.Game.Objects
{
    internal class Projectile : Weapon
    {
        public Projectile(IntPtr ptr, string name)
        {
            WeaponPtr = ptr;
            this.Name = name;
        }

        public override char ToIcon()
        {
            if (Name.StartsWith("flash"))
                return '\uE02b';
            else if (Name.StartsWith("flash"))
                return '\uE02c';
            else if (Name.StartsWith("smoke"))
                return '\uE02d';
            else if (Name.StartsWith("mol"))
                return '\uE02e';
            else if (Name.StartsWith("he"))
                return '\uE02c';
            return ' ';
        }
    }
}
