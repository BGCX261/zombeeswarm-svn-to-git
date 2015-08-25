using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm
{
    class Sprite
    {
        private Vector2 _position;
        private Texture2D _texture;
        private string _textureName;

        public Sprite( Vector2 position )
        {
            _position = position;
            _texture = null;
        }

        public float X
        {
            get{ return _position.X; }
            set{ _position.X = value; }
        }

        public float Y
        {
            get{ return _position.Y; }
            set{ _position.Y = value; }
        }

        public Vector2 Position
        {
            get{ return _position; }
            set{ _position = value; }
        }

        public string TextureName
        {
            get{ return _textureName; }
            set{ _textureName = value; }
        }

        public Texture2D Texture
        {
            get{ return _texture; }
            set{ _texture = value; }
        }
    }
}
