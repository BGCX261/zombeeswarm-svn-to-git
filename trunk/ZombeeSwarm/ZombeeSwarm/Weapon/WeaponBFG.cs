using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombeeSwarm
{
    class WeaponBFG : Weapon
    {
        protected override void init( Ammunition ammo )
        {
            _coolDown = BASE_COOLDOWN;
            _ammo = new AmmunitionPiercing();
        }

        public override string ToString()
        {
            return "BFG";
        }
    }
}
