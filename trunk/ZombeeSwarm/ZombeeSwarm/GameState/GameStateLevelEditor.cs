using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ZombeeSwarm.GameState
{
    class GameStateLevelEditor : IGameState
    {
        private Game _game;

        private List<Swarm> _swarms = new List<Swarm>();
        private int _currentSwarm = 0;
        private EntityEnemy _selectedEnemy;
        private int _lastWheelValue;

        private bool _previousKeyHasBeenDown = false;
        private bool _deleteKeyHasBeenDown = false;
        private bool _nextKeyHasBeenDown = false;
        private bool _saveKeyHasBeenDown = false;

        private const Keys PREVIOUS_KEY = Keys.Q;
        private const Keys DELETE_KEY = Keys.W;
        private const Keys NEXT_KEY = Keys.E;
        private const Keys SAVE_KEY = Keys.S;
        private const Keys MENU_KEY = Keys.Escape;

        private SpriteFont _font;

        private bool _isDone = false;
        private String _nextState;

        public GameStateLevelEditor()
        {

        }

        public void Init( Game game )
        {
            _game = game;

            _isDone = false;

            _selectedEnemy = new EntityEnemyZombee();

            _swarms.Clear();
            _swarms.Add( new Swarm() );
            _currentSwarm = 0;

            if( _font == null )
                _font = game.Content.Load<SpriteFont>( "fonts/MenuItem" );

            _lastWheelValue = Mouse.GetState().ScrollWheelValue;
        }

        public void Exit()
        {
            _swarms.Clear();
        }

        public void Update( GameTime gameTime )
        {
            MouseState mouseState = Mouse.GetState();

            _selectedEnemy.Position = new Vector2( mouseState.X, mouseState.Y );

            // Make sure that the mouse is inside the window!
            if( _game.Window.ClientBounds.Contains( mouseState.X + _game.Window.ClientBounds.X, mouseState.Y + _game.Window.ClientBounds.Y ) )
            {
                // The user wishes to place an enemy at the position of the mouse
                if( mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed )
                    _swarms.ElementAt( _currentSwarm ).Add( _selectedEnemy.Clone( _selectedEnemy.Position ) );
                // The user wishes to remove an enemy at the position of the mouse
                else if( mouseState.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed )
                    _swarms.ElementAt( _currentSwarm ).Remove( new Vector2( mouseState.X, mouseState.Y ) );
                // The player wants to change the selected enemy
                else if( mouseState.ScrollWheelValue < _lastWheelValue )
                {
                    _lastWheelValue = mouseState.ScrollWheelValue;
                    _selectedEnemy = new EntityEnemyZombee();
                }
                else if( mouseState.ScrollWheelValue > _lastWheelValue )
                {
                    _lastWheelValue = mouseState.ScrollWheelValue;
                    _selectedEnemy = new EntityEnemyBeeHive();
                }
            }

            KeyboardState keyboardState = Keyboard.GetState();

            // Update the previous state of the keys
            if( !keyboardState.IsKeyDown( PREVIOUS_KEY ) )
                _previousKeyHasBeenDown = false;
            if( !keyboardState.IsKeyDown( DELETE_KEY ) )
                _deleteKeyHasBeenDown = false;
            if( !keyboardState.IsKeyDown( NEXT_KEY ) )
                _nextKeyHasBeenDown = false;
            if( !keyboardState.IsKeyDown( SAVE_KEY ) )
                _saveKeyHasBeenDown = false;

            // Handle the keyboard input accordingly

            // Previous wave
            if( keyboardState.IsKeyDown( PREVIOUS_KEY ) && !_previousKeyHasBeenDown )
            {
                _previousKeyHasBeenDown = true;

                if( _currentSwarm != 0 )
                    --_currentSwarm;
            }
            // Delete the current wave
            else if( keyboardState.IsKeyDown( DELETE_KEY ) && !_deleteKeyHasBeenDown )
            {
                _deleteKeyHasBeenDown = true;

                _swarms.RemoveAt( _currentSwarm );

                if( _currentSwarm != 0 )
                    --_currentSwarm;

                if( _swarms.Count == 0 )
                    _swarms.Add( new Swarm() );
            }
            // Next wave
            else if( keyboardState.IsKeyDown( NEXT_KEY ) && !_nextKeyHasBeenDown )
            {
                _nextKeyHasBeenDown = true;

                if( ++_currentSwarm == _swarms.Count )
                    _swarms.Add( new Swarm() );
            }
            // Save the current map
            else if( keyboardState.IsKeyDown( SAVE_KEY ) && !_saveKeyHasBeenDown )
            {
                _saveKeyHasBeenDown = true;

                Map map = new Map();
                string fileName = "editor.map";

                map.Swarms = _swarms;

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using( XmlWriter writer = XmlWriter.Create( "Content/maps/" + fileName, settings ) )
                    IntermediateSerializer.Serialize( writer, map, null );
            }
            // Go back to the menu
            else if( keyboardState.IsKeyDown( MENU_KEY ) )
            {
                _isDone = true;
                _nextState = "MainMenu";
            }
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            bool mouseOverEnemy = false;

            spriteBatch.GraphicsDevice.Clear( Color.Green );

            foreach( EntityEnemy enemy in _swarms.ElementAt( _currentSwarm ).Enemies )
            {
                enemy.Draw( spriteBatch );
                if( enemy.Collision( _selectedEnemy ) )
                    mouseOverEnemy = true;
            }

            _selectedEnemy.DrawTransparent( spriteBatch, mouseOverEnemy ? Color.Red : Color.GreenYellow );

            spriteBatch.DrawString( _font, "Wave#: " + (_currentSwarm + 1), new Vector2( 10, 10 ), Color.White );
            spriteBatch.DrawString( _font, "Waves#: " + _swarms.Count, new Vector2( 10, 50 ), Color.White );
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
