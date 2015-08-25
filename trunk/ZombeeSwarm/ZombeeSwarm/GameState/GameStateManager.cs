using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombeeSwarm.GameState
{
    interface IGameState
    {
        void Init( Game game );
        void Exit();
        void Update( GameTime gameTime );
        void Draw( SpriteBatch spriteBatch );
        bool IsDone();
        string GetNextStateId();
    }

    class GameStateManager
    {
        private Dictionary<string, IGameState> _gameStates;
        private IGameState _currentGameState;
        private Game _game;

        public GameStateManager( Game game )
        {
            _game = game;

            _gameStates = new Dictionary<string,IGameState>();
            _currentGameState = null;
        }

        public void RegisterGameState( string gameStateId, IGameState gameState )
        {
            _gameStates.Add( gameStateId, gameState );
        }

        public void SetActiveGameState( string gameStateId )
        {
            if( !_gameStates.TryGetValue( gameStateId, out _currentGameState ) )
                throw new Exception( "Could not find GameState!" );

            _currentGameState.Init( _game );
        }

        public bool Update( GameTime gameTime )
        {
            if( _currentGameState == null )
                return false;

            _currentGameState.Update( gameTime );
            if( _currentGameState.IsDone() )
                doChangeState();

            return true;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            if( _currentGameState != null )
                _currentGameState.Draw( spriteBatch );
        }

        private void doChangeState()
        {
            _currentGameState.Exit();

            if( _currentGameState.GetNextStateId().Length == 0 )
            {
                _currentGameState = null;
                return;
            }

            IGameState gameState = null;
            if( !_gameStates.TryGetValue( _currentGameState.GetNextStateId(), out gameState ) )
                throw new Exception( "Error! Could not find GameState: " + _currentGameState.GetNextStateId() );

            _currentGameState = gameState;
            _currentGameState.Init( _game );
        }
    }
}
