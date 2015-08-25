using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZombeeSwarm.GameState
{
    class GameStatePlay : IGameState
    {
        private Model _model;

        public void Init( Microsoft.Xna.Framework.Game game )
        {
            _model = new Model( (Game1) game );
            _model.Load( GameStatePlayMenu.MapToPlay );
            _model.Start();
        }

        public void Exit()
        {

        }

        public void Update( Microsoft.Xna.Framework.GameTime gameTime )
        {
            _model.Update( gameTime );
        }

        public void Draw( Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch )
        {
            _model.Draw( spriteBatch );
        }

        public bool IsDone()
        {
            return _model.IsDone();
        }

        public string GetNextStateId()
        {
            return "PlayMenu";
        }
    }
}
