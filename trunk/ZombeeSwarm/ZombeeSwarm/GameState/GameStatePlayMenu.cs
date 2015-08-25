using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombeeSwarm.GameState
{
    class GameStatePlayMenu : IGameState
    {
        public static string MapToPlay;

        private List<PlayMenuItem> _menuItems = new List<PlayMenuItem>();
        private MenuItem _backButton = new MenuItem( "Back", "MainMenu" );

        private SpriteFont _font;
        private bool _mouseHasBeenDown = true;

        private bool _isDone = false;
        private string _nextState;

        class PlayMenuItem : MenuItem
        {
            public String Map;

            public PlayMenuItem( String title, String map, String nextState )
                : base( title, nextState )
            {
                Map = map;
            }
        }

        public GameStatePlayMenu()
        {
            _menuItems.Add( new PlayMenuItem( "Editor Map", "editor.map", "Play" ) );

            _menuItems.Add( new PlayMenuItem( "Level 1 - Very Easy", "c1.map", "Play" ) );
            _menuItems.Add( new PlayMenuItem( "Level 2 - Easy", "c2.map", "Play" ) );
            _menuItems.Add( new PlayMenuItem( "Level 3 - Normal", "c3.map", "Play" ) );
            _menuItems.Add( new PlayMenuItem( "Level 4 - Hard", "c4.map", "Play" ) );
            _menuItems.Add( new PlayMenuItem( "Level 5 - You won't survive", "c5.map", "Play" ) );

            for( int i = 0 ; i < _menuItems.Count ; ++i )
                _menuItems.ElementAt( i ).Position = new Vector2( 100, 200 + i * 30 );

            _backButton.Position = new Vector2( 100, 200 + _menuItems.Count * 30 );
        }

        public void Init( Microsoft.Xna.Framework.Game game )
        {
            if( _font == null )
                _font = game.Content.Load<SpriteFont>( "fonts/MenuItem" );
            _mouseHasBeenDown = true;
            _isDone = false;
        }

        public void Exit()
        {

        }

        public void Update( GameTime gameTime )
        {
            _nextState = null;

            foreach( PlayMenuItem item in _menuItems )
                item.IsSelected = false;
            _backButton.IsSelected = false;

            MouseState mouseState = Mouse.GetState();
            if( _backButton.Collision( new Vector2( mouseState.X, mouseState.Y ) ) )
            {
                _backButton.IsSelected = true;
                _nextState = _backButton.NextState;
            }
            else
                foreach( PlayMenuItem item in _menuItems )
                    if( item.Collision( new Vector2( mouseState.X, mouseState.Y ) ) )
                    {
                        item.IsSelected = true;
                        _nextState = item.NextState;
                        MapToPlay = item.Map;
                        break;
                    }


            if( mouseState.LeftButton == ButtonState.Released )
                _mouseHasBeenDown = false;
            else if( mouseState.LeftButton == ButtonState.Pressed && _nextState != null && !_mouseHasBeenDown )
                _isDone = true;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            foreach( MenuItem item in _menuItems )
                spriteBatch.DrawString( _font, item.Text, item.Position, item.IsSelected ? Color.GreenYellow : Color.Red );

            spriteBatch.DrawString( _font, _backButton.Text, _backButton.Position, _backButton.IsSelected ? Color.GreenYellow : Color.Red );
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
