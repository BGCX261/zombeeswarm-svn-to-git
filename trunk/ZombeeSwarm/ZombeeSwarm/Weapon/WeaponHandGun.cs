using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class WeaponHandgun : Weapon
    {
        protected override void init( Ammunition ammo )
        {
            if( ammo == null )
                _ammo = new Ammunition9mm();
            _coolDown = BASE_COOLDOWN * 0.8f;
        }

        public override string ToString()
        {
            return "Handgun";
        }
    }
}
