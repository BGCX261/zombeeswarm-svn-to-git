using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombeeSwarm
{
    /// <summary>
    /// Model, containing the current data for a map and handles the
    /// game play.
    /// </summary>
    class Model
    {
        private EntityPlayer _player;
        private List<EntityEnemy> _enemies = new List<EntityEnemy>();
        private List<EntityEnemy> _bodies = new List<EntityEnemy>();
        private List<Ammunition> _bullets = new List<Ammunition>();
        private List<PowerUp> _powerUps = new List<PowerUp>();

        private Game1 _game;
        private Map _map;

        public bool DisplayingMessage = false;
        private float _messageTimer = 0.0f;
        private float _messageDuration = 3.0f;
        private string _message;

        private SpriteFont _font;

        private bool _isDone = false;

        private PowerUpFactory _powerUpFactory = new PowerUpFactory();
        private Random random = new Random();

        /// <summary>
        /// Creates a new Model.
        /// </summary>
        /// <param name="game"></param>
        public Model( Game1 game )
        {
            _game = game;
            _font = _game.Content.Load<SpriteFont>( "fonts/MenuItem" );
        }

        /// <summary>
        /// Starts the game play.
        /// </summary>
        public void Start()
        {
            _enemies = _map.GetFirstSwarm();
            displayMessage();
        }

        public bool IsDone()
        {
            return _isDone;
        }

        /// <summary>
        /// Loads the contents of the mapfile to the model.
        /// </summary>
        /// <param name="fileName"></param>
        public void Load( String fileName )
        {
            _bodies.Clear();
            _powerUps.Clear();

            float x = _game.Window.ClientBounds.Width / 2;
            float y = _game.Window.ClientBounds.Height / 2;
            _player = new EntityPlayer( new Vector2( x, y ) );

            XmlReaderSettings settings = new XmlReaderSettings();

            using( XmlReader reader = XmlReader.Create( "Content/maps/" + fileName, settings ) )
                _map = IntermediateSerializer.Deserialize<Map>( reader, null );
        }

        /// <summary>
        /// The player.
        /// </summary>
        public EntityPlayer Player
        {
            get{ return _player; }
        }

        /// <summary>
        /// The list of enemies on the map.
        /// </summary>
        public List<EntityEnemy> Enemies
        {
            get{ return _enemies; }
        }

        /// <summary>
        /// The list of bullets that are still traveling through the map.
        /// </summary>
        public List<Ammunition> Bullets
        {
            get{ return _bullets; }
        }

        /// <summary>
        /// The list of power ups that are on the map.
        /// </summary>
        public List<PowerUp> PowerUps
        {
            get { return _powerUps; }
        }

        /// <summary>
        /// Removes the PowerUp from the model.
        /// </summary>
        /// <param name="powerUp"></param>
        public void Remove( PowerUp powerUp )
        {
            _powerUps.Remove( powerUp );
        }

        /// <summary>
        /// Adds the bullet to the model.
        /// </summary>
        /// <param name="bullet"></param>
        public void Add( Ammunition bullet )
        {
            _bullets.Add( bullet );
        }

        /// <summary>
        /// Removes the bullet from the model.
        /// </summary>
        /// <param name="bullet"></param>
        public void Remove( Ammunition bullet )
        {
            _bullets.Remove( bullet );
        }

        /// <summary>
        /// Removes the enemy from the model.
        /// </summary>
        /// <param name="enemy"></param>
        public void Add( EntityEnemy enemy )
        {
            _enemies.Add( enemy );
        }

        /// <summary>
        /// Kills the enemy. If the swarm is done, the next one will begin.
        /// </summary>
        /// <param name="enemy"></param>
        public void Kill( EntityEnemy enemy )
        {
            _enemies.Remove( enemy );
            _bodies.Add( enemy );

            if( random.NextDouble() < 0.20 )
                _powerUps.Add( _powerUpFactory.Create( enemy.Position ) );

            if( isSwarmDone() )
            {
                if( !_map.IsOnLastSwarm() )
                    startNextSwarm();
                else
                    displayMessage();
            }
        }

        /// <summary>
        /// Displays a message to the player.
        /// </summary>
        private void displayMessage()
        {
            DisplayingMessage = true;
            if( !_player.IsAlive )
                _message = "The battle has been lost, and humanity is gone...";
            else if( isSwarmDone() && _map.IsOnLastSwarm() )
                _message = "The enemies are gone...for now...";
            else
                _message = "Get ready for the " + (_map.IsOnLastSwarm() ? "last" : "next") + " swarm!";
        }

        /// <summary>
        /// Checks if the current swarm is done.
        /// </summary>
        /// <returns>true if the swarm is done</returns>
        private bool isSwarmDone()
        {
            foreach( EntityEnemy enemy in _enemies )
                if( enemy.IsAlive )
                    return false;

            return true;
        }

        /// <summary>
        /// Starts the next swarm.
        /// </summary>
        private void startNextSwarm()
        {
            _enemies = _map.GetNextSwarm();
            displayMessage();
        }

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update( GameTime gameTime )
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if( keyboardState.IsKeyDown( Keys.Escape ) )
            {
                _isDone = true;
                return;
            }

            // If a message is displayed, update the timer
            if( DisplayingMessage )
            {
                _messageTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;
                // We are done showing the message
                if( _messageTimer >= _messageDuration )
                {
                    DisplayingMessage = false;
                    _messageTimer = 0.0f;
                    
                    _isDone = (isSwarmDone() && _map.IsOnLastSwarm()) || !_player.IsAlive;
                }
            }
            
            // Updates the player
            if( _player.IsAlive )
                _player.Update( gameTime, this, _game );

            // Updates the enemies
            foreach( EntityEnemy enemy in _enemies.ToArray() )
            {
                if( enemy.IsAlive )
                    enemy.Update( gameTime, this, _game );
                else
                    Kill( enemy );
            }

            // Updates the power ups
            foreach( PowerUp powerUp in _powerUps.ToArray() )
            {
                if( powerUp.IsAlive )
                    powerUp.Update( gameTime );
                else
                    _powerUps.Remove( powerUp );
            }

            // If the player was killed, show a message
            if( !_player.IsAlive )
                displayMessage();

            // Update the ammunition
            foreach( Ammunition bullet in _bullets.ToArray() )
            {
                if( bullet.IsAlive )
                    bullet.Update( gameTime, this, _game );
                else
                    _bullets.Remove( bullet );
            }
        }

        /// <summary>
        /// Draws the model.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw( SpriteBatch spriteBatch )
        {
            spriteBatch.GraphicsDevice.Clear( Color.Green );

            // Draw the bodies
            foreach( EntityEnemy body in _bodies )
                body.Draw( spriteBatch );

            // Draw the power ups
            foreach( PowerUp powerUp in _powerUps )
                powerUp.Draw( spriteBatch );

            // Draw the enemies
            foreach( EntityEnemy enemy in _enemies )
            {
                if( !DisplayingMessage || !_player.IsAlive )
                    enemy.Draw( spriteBatch );
                else
                    enemy.DrawTransparent( spriteBatch, Color.White );
            }

            // Draw the player
            _player.Draw( spriteBatch );

            // Draw the bullets
            foreach( Ammunition bullet in _bullets.ToArray() )
                bullet.Draw( spriteBatch );

            // The current health
            spriteBatch.DrawString( _font,
                "Health: " + _player.Health,
                new Vector2( 10, 10 ),
                Color.White );

            // The current weapon
            spriteBatch.DrawString( _font,
                "Weapon: " + _player.CurrentWeapon.ToString(),
                new Vector2( 10, 40 ),
                Color.White );

            // If there is a message to display, display it
            if( DisplayingMessage )
            {
                int countDown = (int) (_messageDuration - _messageTimer);
                Vector2 messageSize = _font.MeasureString( _message + " " + countDown );
                float x = _game.Window.ClientBounds.Width / 2 - messageSize.X / 2 ;
                float y = _game.Window.ClientBounds.Height - messageSize.Y;

                spriteBatch.DrawString( _font, _message + " " + countDown, new Vector2( x, y ), Color.White );
            }
        }
    }
}
