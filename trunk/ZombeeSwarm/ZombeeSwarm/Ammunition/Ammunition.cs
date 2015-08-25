using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    abstract class Ammunition : MapObject
    {
        public const float BASE_SPEED = Entity.BASE_SPEED * 5.0f;
        public const float BASE_DAMAGE = Entity.ENEMY_HEALTH / 10;

        protected float _damage = BASE_DAMAGE;
        protected float _speed = BASE_SPEED;
        protected Vector2 _direction;

        public Ammunition()
            : base()
        {
        }

        public Ammunition( Vector2 position, Vector2 direction )
            : base( position )
        {
            _direction = direction;
        }

        public float damage
        {
            get{ return _damage; }
        }

        /// <summary>
        /// Checks wether or not there is a collision with an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected bool Collision( Entity entity )
        {
            if( !entity.IsAlive )
                return false;

            Rectangle bulletRectangle = new Rectangle( (int) _sprite.X, (int) _sprite.Y, (int) Size.X, (int) Size.Y );
            Rectangle entityRectangle = new Rectangle( (int) (entity.Position.X - entity.Size.X/2),
                (int) (entity.Position.Y - entity.Size.Y/2),
                (int) entity.Size.X,
                (int) entity.Size.Y );

            return bulletRectangle.Intersects( entityRectangle );
        }

        /// <summary>
        /// Creates a clone of the ammunition.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract Ammunition Clone( Vector2 direction, Vector2 position );

        /// <summary>
        /// Updates the ammunition.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="model"></param>
        public virtual void Update( GameTime gameTime, Model model, Game1 game )
        {
            _sprite.X += _direction.X * _speed;
            _sprite.Y += _direction.Y * _speed;

            int width = game.Window.ClientBounds.Width;
            int height = game.Window.ClientBounds.Height;

            IsAlive = !(Position.X < 0 || Position.Y < 0 || Position.X > width || Position.Y > height);
        }
    }
}
