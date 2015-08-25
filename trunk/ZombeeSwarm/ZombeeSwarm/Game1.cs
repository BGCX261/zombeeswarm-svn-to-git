using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ZombeeSwarm.GameState;

namespace ZombeeSwarm
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static AudioEngine _audioEngine;
        public static WaveBank _waveBank;
        public static SoundBank _soundBank;

        private GameStateManager _gameStateManager;
        public string PathToMap;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Make sure that the mouse is visible
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteManager.ContentPath = "textures/";
            SpriteManager.ServiceProvider = Services;

            _audioEngine = new AudioEngine( "Content/audio/ZombeeSwarmSound.xgs" );
            _soundBank = new SoundBank( _audioEngine, "Content/audio/soundbank.xsb" );
            _waveBank = new WaveBank( _audioEngine, "Content/audio/wavebank.xwb" );

            _gameStateManager = new GameStateManager( this );

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _gameStateManager.RegisterGameState( "MainMenu", new GameStateMainMenu() );
            _gameStateManager.RegisterGameState( "PlayMenu", new GameStatePlayMenu() );
            _gameStateManager.RegisterGameState( "LevelEditor", new GameStateLevelEditor() );
            _gameStateManager.RegisterGameState( "Play", new GameStatePlay() );

            _gameStateManager.SetActiveGameState( "MainMenu" );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //System.GC.Collect();

            _audioEngine.Update();

            if( IsActive && !_gameStateManager.Update( gameTime ) )
                Exit();

            base.Update( gameTime );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear( Color.Black );

            spriteBatch.Begin( SpriteBlendMode.AlphaBlend );
            _gameStateManager.Draw( spriteBatch );
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
