using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    abstract class MapObject
    {
        public bool IsAlive = true;

        protected float _rotation = 0.0f;
        protected Sprite _sprite;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MapObject()
        {
            init( new Vector2( 0, 0 ) );
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position"></param>
        public MapObject( Vector2 position )
        {
            init( position );
        }

        /// <summary>
        /// Initializing method.
        /// </summary>
        /// <param name="position"></param>
        protected abstract void init( Vector2 position );

        /// <summary>
        /// The position.
        /// </summary>
        public Vector2 Position
        {
            get{ return _sprite.Position; }
            set{ _sprite.Position = value; }
        }

        /// <summary>
        /// A vector containing the width (X) and height (Y).
        /// </summary>
        public Vector2 Size
        {
            get{ return new Vector2( (float) _sprite.Texture.Width,
                                     (float) _sprite.Texture.Height ); }
        }

        /// <summary>
        /// Draws the MapObject.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw( SpriteBatch spriteBatch )
        {
            spriteBatch.Draw( _sprite.Texture,
                _sprite.Position,
                null,
                Color.White,
                _rotation,
                new Vector2( _sprite.Texture.Width / 2, _sprite.Texture.Height / 2 ),
                1,
                SpriteEffects.None,
                0 );
        }

        /// <summary>
        /// Draws the MapObject transparent in the color of ones choosing.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="color"></param>
        public virtual void DrawTransparent( SpriteBatch spriteBatch, Color color )
        {
            spriteBatch.Draw( _sprite.Texture,
                _sprite.Position,
                null,
                new Color( color, 0.5f ),
                _rotation,
                new Vector2( _sprite.Texture.Width / 2, _sprite.Texture.Height / 2 ),
                1,
                SpriteEffects.None,
                0 );
        }
    }
}
