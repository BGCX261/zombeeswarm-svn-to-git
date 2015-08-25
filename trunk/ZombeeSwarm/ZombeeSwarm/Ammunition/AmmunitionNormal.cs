using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class AmmunitionNormal : Ammunition
    {
        public AmmunitionNormal()
            : base()
        {
        }

        public AmmunitionNormal( Vector2 position, Vector2 direction )
            : base( position, direction )
        {
        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "ammunition-normal" );
            _speed = BASE_SPEED;
            _damage = BASE_DAMAGE;
        }

        public override Ammunition Clone( Vector2 direction, Vector2 position )
        {
            return new AmmunitionNormal( position, direction );
        }

        public override void Update( GameTime gameTime, Model model, Game1 game )
        {
            base.Update( gameTime, model, game );

            foreach( EntityEnemy enemy in model.Enemies )
                if( !model.DisplayingMessage && Collision( enemy ) )
                {
                    IsAlive = false;
                    model.Bullets.Remove( this );
                    enemy.DealDamage( this );
                    return;
                }
        }
    }
}
