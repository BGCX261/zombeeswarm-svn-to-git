using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    /// <summary>
    /// An abstract class describing an entity.
    /// </summary>
    abstract class Entity : MapObject
    {
        /// <summary>
        /// The base movement speed
        /// </summary>
        public const float BASE_SPEED = 0.95f;
        /// <summary>
        /// The base player movement speed
        /// </summary>
        public const float PLAYER_SPEED = BASE_SPEED;
        /// <summary>
        /// The base enemy movement speed
        /// </summary>
        public const float ENEMY_SPEED = PLAYER_SPEED * 0.6f;

        /// <summary>
        /// The base health value.
        /// </summary>
        public const float BASE_HEALTH = 100.0f;
        /// <summary>
        /// The base player health value.
        /// </summary>
        public const float PLAYER_HEALTH = BASE_HEALTH;
        /// <summary>
        /// The base enemy health value.
        /// </summary>
        public const float ENEMY_HEALTH = BASE_HEALTH * 0.5f;

        protected float _maxHealth;
        protected float _currentHealth;
        protected float _speed;
        protected Weapon _weapon;
        protected bool _canBeCollidedWith = true;

        protected Texture2D _healthBarTexture = SpriteManager.CreateSprite( new Vector2(), "entity-healthbar" ).Texture;

        public Entity()
            : base()
        {
        }

        /// <summary>
        /// Creates an entity at the given position.
        /// </summary>
        /// <param name="position"></param>
        public Entity( Vector2 position )
            : base( position )
        {
        }

        /// <summary>
        /// Collision method that is used during game play, to make it somewhat
        /// more realistic.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public bool PlayingCollision( EntityEnemy enemy )
        {
            if( enemy == this || !enemy._canBeCollidedWith || !enemy.IsAlive || !IsAlive )
                return false;

            Vector2 distance = Position - enemy.Position;
            return distance.Length() < (Size.X + Size.Y) / 16;
        }

        /// <summary>
        /// Checks wether or not the point is within this.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Collision( Vector2 position )
        {
            Rectangle myRectangle = new Rectangle( (int) (Position.X - Size.X / 2),
                (int) (Position.Y - Size.Y / 2),
                (int) Size.X,
                (int) Size.Y );
            Point point = new Point( (int) position.X, (int) position.Y );

            return myRectangle.Contains( point );
        }

        /// <summary>
        /// Checks wether or not this has collided with the
        /// provided entity.
        /// </summary>
        /// <param name="mapObject"></param>
        /// <returns>true if there is a collision</returns>
        public bool Collision( MapObject mapObject )
        {
            // One cannot collide with one self!
            if( this == mapObject || !IsAlive || !mapObject.IsAlive )
                return false;

            Rectangle myRectangle = new Rectangle( (int) (Position.X - Size.X / 2),
                (int) (Position.Y - Size.Y / 2),
                (int) Size.X,
                (int) Size.Y );
            Rectangle otherRectangle = new Rectangle( (int) (mapObject.Position.X - mapObject.Size.X / 2),
                (int) (mapObject.Position.Y - mapObject.Size.Y / 2),
                (int) mapObject.Size.X,
                (int) mapObject.Size.Y );

            return myRectangle.Intersects( otherRectangle );
        }

        /// <summary>
        /// Deals damage to the entity according to the bullet
        /// that has hit it.
        /// </summary>
        /// <param name="bullet"></param>
        public void DealDamage( Ammunition bullet )
        {
            if( IsAlive )
            {
                Game1._soundBank.PlayCue( "hurt_enemy0" );
                _currentHealth -= bullet.damage;
                IsAlive = _currentHealth > 0;

                // The entity was killed, replace the sprite to that of a corpse
                if( !IsAlive )
                {
                    Game1._soundBank.PlayCue( "killed_enemy0" );
                    _sprite = SpriteManager.CreateSprite( Position, "enemy-dead" );
                }
            }
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="model"></param>
        /// <param name="game"></param>
        public abstract void Update( GameTime gameTime, Model model, Game1 game );
    }
}
