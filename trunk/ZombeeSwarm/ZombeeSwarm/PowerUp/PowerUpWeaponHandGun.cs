using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class PowerUpWeaponHandgun : PowerUp
    {
        public PowerUpWeaponHandgun()
            : base()
        {

        }

        public PowerUpWeaponHandgun( Vector2 position )
            : base( position )
        {

        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "powerup-gun" );
        }

        public override void Apply( EntityPlayer player )
        {
            base.Apply( player );
            player.AddWeapon( new WeaponHandgun() );
        }

        public override void Remove( EntityPlayer player )
        {
        }
    }
}
