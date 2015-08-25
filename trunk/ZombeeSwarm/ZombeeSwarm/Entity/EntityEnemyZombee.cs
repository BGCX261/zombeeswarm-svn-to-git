using System;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    class EntityEnemyZombee : EntityEnemy
    {
        public EntityEnemyZombee()
            : base()
        {
        }

        public EntityEnemyZombee( Vector2 position )
            : base( position )
        {
        }

        public override EntityEnemy Clone( Vector2 position )
        {
            return new EntityEnemyZombee( position );
        }

        protected override void init( Vector2 position )
        {
            _meleeCooldown = BASE_MELEE_COOLDOWN * 2.0f;
            _sprite = SpriteManager.CreateSprite( position, "enemy-zombee" );
        }

        public override void Update( GameTime gameTime, Model model, Game1 game )
        {
            UpdateMeleeTimer( gameTime );

            EntityPlayer player = model.Player;

            _rotation = (float) Math.Atan2( player.Position.Y - Position.Y, player.Position.X - Position.X );

            if( model.DisplayingMessage )
                return;

            if( Collision( player ) )
            {
                if( _canMeleeAttack )
                {
                    _canMeleeAttack = false;
                    player.DealDamage( (int) Entity.PLAYER_HEALTH / 20 );
                }

                return;
            }

            float xDifference = _speed * (float) Math.Cos( _rotation );
            float yDifference = _speed * (float) Math.Sin( _rotation );

            _sprite.X += xDifference;
            _sprite.Y += yDifference;

            // If there is a collision after the movement, undo the movement
            foreach( EntityEnemy enemy in model.Enemies )
                if( enemy.IsAlive && PlayingCollision( enemy ) )
                {
                    _sprite.X -= xDifference;
                    _sprite.Y -= yDifference;
                    return;
                }
        }
    }
}
