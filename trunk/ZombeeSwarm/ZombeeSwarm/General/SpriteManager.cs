using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    /// <summary>
    /// Class that handles all of the sprite creation.
    /// </summary>
    class SpriteManager
    {
        private static String _contentPath;
        private static ContentManager _contentManager;
        private static Dictionary<String, Texture2D> _textures = new Dictionary<string,Texture2D>();

        /// <summary>
        /// The path to the content.
        /// </summary>
        public static string ContentPath
        {
            get{ return _contentPath; }
            set{ _contentPath = value; }
        }

        /// <summary>
        /// The service provider.
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            set{ _contentManager = new ContentManager( value, "Content" ); }
        }

        /// <summary>
        /// Creates and returns a sprite with the given texture and position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public static Sprite CreateSprite( Vector2 position, String textureName )
        {
            Texture2D texture = null;

            if( !_textures.TryGetValue( textureName, out texture ) )
            {
                texture = _contentManager.Load<Texture2D>( _contentPath + textureName );
                _textures.Add( textureName, texture );
            }

            Sprite sprite = new Sprite( position );
            sprite.Texture = texture;
            sprite.TextureName = textureName;

            return sprite;
        }
    }
}
