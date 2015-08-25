using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class PowerUpSpeed : PowerUp
    {
        private float _speedBonus = 1.2f;

        public PowerUpSpeed()
            : base()
        {
        }

        public PowerUpSpeed( Vector2 position )
            : base( position )
        {
        }

        protected override void init( Vector2 position )
        {
            _effectDuration = 4.0f;
            _sprite = SpriteManager.CreateSprite( position, "powerup-speed" );
        }

        public override void Apply( EntityPlayer player )
        {
            base.Apply( player );
            player.MultiplySpeed( _speedBonus );
        }

        public override void Remove( EntityPlayer player )
        {
            player.DemultiplySpeed( _speedBonus );
        }
    }
}
