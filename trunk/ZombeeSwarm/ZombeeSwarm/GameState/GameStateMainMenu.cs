using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombeeSwarm.GameState
{
    class GameStateMainMenu : IGameState
    {
        private List<MenuItem> _menuItems = new List<MenuItem>();
        private string _nextState = "";

        private SpriteFont _font;
        private bool _mouseHasBeenDown = true;

        private bool _isDone;

        public GameStateMainMenu()
        {
            _menuItems.Add( new MenuItem( "Play Menu", "PlayMenu" ) );
            _menuItems.Add( new MenuItem( "Level Editor", "LevelEditor" ) );
            _menuItems.Add( new MenuItem( "Exit", "" ) );

            for( int i = 0 ; i < _menuItems.Count ; ++i )
                _menuItems.ElementAt( i ).Position = new Vector2( 100, 200 + 30 * i );

        }

        public void Init( Game game )
        {
            if( _font == null )
                _font = game.Content.Load<SpriteFont>( "fonts/MenuItem" );
            _mouseHasBeenDown = true;
            _isDone = false;
        }

        public void Exit()
        {
            
        }

        public void Update( Microsoft.Xna.Framework.GameTime gameTime )
        {
            _nextState = null;

            foreach ( MenuItem item in _menuItems )
                item.IsSelected = false;
            
            MouseState mouseState = Mouse.GetState();
            foreach( MenuItem item in _menuItems )
                if( item.Collision( new Vector2( mouseState.X, mouseState.Y ) ) )
                {
                    item.IsSelected = true;
                    _nextState = item.NextState;
                    break;
                }

            if( mouseState.LeftButton == ButtonState.Released )
                _mouseHasBeenDown = false;
            else if( mouseState.LeftButton == ButtonState.Pressed && _nextState != null && !_mouseHasBeenDown )
                _isDone = true;
        }

        public void Draw( Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch )
        {
            foreach( MenuItem item in _menuItems )
                spriteBatch.DrawString( _font, item.Text, item.Position, item.IsSelected ? Color.GreenYellow : Color.Red );
        }

        public bool IsDone()
        {
            return _isDone;
        }

        public string GetNextStateId()
        {
            return _nextState;
        }
    }
}
