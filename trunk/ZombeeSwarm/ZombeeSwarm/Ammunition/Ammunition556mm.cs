using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class Ammunition556mm : Ammunition
    {
        public Ammunition556mm()
            : base()
        {

        }

        public Ammunition556mm( Vector2 position, Vector2 direction )
            : base( position, direction )
        {

        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "ammunition-normal" );
            _damage = BASE_DAMAGE * 4.0f;
        }

        public override Ammunition Clone( Vector2 direction, Vector2 position )
        {
            return new Ammunition556mm( position, direction );
        }

        public override void Update( Microsoft.Xna.Framework.GameTime gameTime, Model model, Game1 game )
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
