using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class PowerUpWeaponBFG : PowerUp
    {
        public PowerUpWeaponBFG()
            : base()
        {

        }

        public PowerUpWeaponBFG( Vector2 position )
            : base( position )
        {

        }

        public override void Apply( EntityPlayer player )
        {
            base.Apply( player );

            player.AddWeapon( new WeaponBFG() );
        }

        public override void Remove( EntityPlayer player )
        {
        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "powerup-bfg" );
        }
    }
}
