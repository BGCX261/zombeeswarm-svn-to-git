using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombeeSwarm
{
    class WeaponMachineGun : Weapon
    {
        protected override void init( Ammunition ammo )
        {
            if( ammo == null )
                _ammo = new Ammunition556mm();
            _coolDown = BASE_COOLDOWN * 0.2f;
        }

        public override string ToString()
        {
            return "Machine Gun";
        }
    }
}
