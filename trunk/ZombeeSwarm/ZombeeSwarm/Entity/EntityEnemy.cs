using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    /// <summary>
    /// Base class for all of the enemies.
    /// </summary>
    abstract class EntityEnemy : Entity
    {
        /// <summary>
        /// Base cooldown for the meele attacks.
        /// </summary>
        public const float BASE_MELEE_COOLDOWN = 1.0f;

        protected float _meleeCooldown = BASE_MELEE_COOLDOWN;
        protected bool _canMeleeAttack = true;

        private float _meleeUpdateTimer = 0f;

        public EntityEnemy()
            : base()
        {
            _currentHealth = ENEMY_HEALTH;
            _maxHealth = _currentHealth;
            _speed = ENEMY_SPEED;
        }

        public EntityEnemy( Vector2 position )
            : base( position )
        {
            _currentHealth = ENEMY_HEALTH;
            _maxHealth = _currentHealth;
            _speed = ENEMY_SPEED;
        }

        protected void UpdateMeleeTimer( GameTime gameTime )
        {
            if( _canMeleeAttack )
                return;

            _meleeUpdateTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if( _meleeUpdateTimer > _meleeCooldown )
            {
                _meleeUpdateTimer = 0f;
                _canMeleeAttack = true;
            }
        }

        /// <summary>
        /// Creates a clone of the object.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract EntityEnemy Clone( Vector2 position );

        public override void Draw( SpriteBatch spriteBatch )
        {
            base.Draw( spriteBatch );

            spriteBatch.Draw( _healthBarTexture,
                new Rectangle( (int) (Position.X - (Size.X / 2)),
                    (int) (Position.Y - Size.Y / 2) - 2,
                    (int) (Size.X * _currentHealth / _maxHealth),
                    5 ),
                    null,
                    Color.White );
        }
    }
}
