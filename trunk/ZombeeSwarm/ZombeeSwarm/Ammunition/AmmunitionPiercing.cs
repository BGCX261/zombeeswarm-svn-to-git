using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class AmmunitionPiercing : Ammunition
    {
        private List<EntityEnemy> _enemies = new List<EntityEnemy>();

        public AmmunitionPiercing()
            : base()
        {

        }

        public AmmunitionPiercing( Vector2 position, Vector2 direction )
            : base( position, direction )
        {

        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "ammunition-piercing" );
            _damage = BASE_DAMAGE * 2.5f;
            _speed = BASE_SPEED * 2.5f;
        }

        public override Ammunition Clone( Vector2 direction, Vector2 position )
        {
            return new AmmunitionPiercing( position, direction );
        }

        public override void Update( GameTime gameTime, Model model, Game1 game )
        {
            base.Update( gameTime, model, game );
            foreach( EntityEnemy enemy in model.Enemies )
                if( !_enemies.Contains( enemy ) && Collision( enemy ) )
                {
                    _enemies.Add( enemy );
                    enemy.DealDamage( this );
                }
        }
    }
}
