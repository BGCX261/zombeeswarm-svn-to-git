using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class EntityEnemyBeeHive : EntityEnemy
    {
        private float _coolDown = 5.0f;
        private float _updateTimer = 0f;

        public EntityEnemyBeeHive()
            : base()
        {
        }

        public EntityEnemyBeeHive( Vector2 position )
            : base( position )
        {
        }

        public override EntityEnemy Clone( Vector2 position )
        {
            return new EntityEnemyBeeHive( position );
        }

        protected override void init( Vector2 position )
        {
            _canBeCollidedWith = false;
            _sprite = SpriteManager.CreateSprite( position, "enemy-beehive" );
        }

        public override void Update( GameTime gameTime, Model model, Game1 game )
        {
            if( model.DisplayingMessage )
                return;

            _updateTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if( _updateTimer >= _coolDown )
            {
                model.Add( new EntityEnemyZombee( new Vector2( Position.X, Position.Y ) ) );
                _updateTimer = 0f;
            }
        }
    }
}
