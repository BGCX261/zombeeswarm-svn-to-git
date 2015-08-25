using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class PowerUpWeaponMachineGun : PowerUp
    {
        public PowerUpWeaponMachineGun()
            : base()
        {

        }

        public PowerUpWeaponMachineGun( Vector2 position )
            : base( position )
        {

        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "powerup-machinegun" );
        }

        public override void Apply( EntityPlayer player )
        {
            base.Apply( player );
            player.AddWeapon( new WeaponMachineGun() );
        }

        public override void Remove( EntityPlayer player )
        {
        }
    }
}
