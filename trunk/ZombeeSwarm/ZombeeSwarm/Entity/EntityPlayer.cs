using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace ZombeeSwarm
{
    /// <summary>
    /// Class representing the player.
    /// </summary>
    class EntityPlayer : Entity
    {
        private List<PowerUp> _powerUps = new List<PowerUp>();
        private List<Weapon> _weapons = new List<Weapon>();

        private const Keys NORTH_KEY = Keys.W;
        private const Keys SOUTH_KEY = Keys.S;
        private const Keys WEST_KEY = Keys.A;
        private const Keys EAST_KEY = Keys.D;

        private int _currentWeaponIndex = 0;
        private int _currentScrollValue = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityPlayer()
            : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position"></param>
        public EntityPlayer( Vector2 position )
            : base( position )
        {
        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "player" );
            //_weapon = new WeaponMachineGun();
            _weapon = new WeaponHandgun();
            _currentHealth = PLAYER_HEALTH;
            _maxHealth = _currentHealth;
            _speed = PLAYER_SPEED;

            _weapons.Add( _weapon );
        }

        public void MultiplySpeed( float multiplier )
        {
            _speed *= multiplier;
        }

        public void DemultiplySpeed( float demultiplier )
        {
            _speed /= demultiplier;
        }

        public float Health
        {
            get{ return _currentHealth; }
        }

        public Weapon CurrentWeapon
        {
            get{ return _weapon; }
        }

        public void AddHealth( float h )
        {
            _currentHealth += h;

            if( _currentHealth > _maxHealth )
                _currentHealth = _maxHealth;
        }

        public void AddWeapon( Weapon weapon )
        {
            foreach( Weapon w in _weapons )
                if( w.GetType() == weapon.GetType() )
                {
                    _weapon = w;
                    return;
                }

            _weapon = weapon;
            _weapons.Add( weapon );
        }

        public void DealDamage( float damage )
        {
            _currentHealth -= damage;
            Game1._soundBank.PlayCue( "hurt_player0" );
            IsAlive = _currentHealth > 0f;
        }

        public override void Update( GameTime gameTime, Model model, Game1 game )
        {
            _weapon.Update( gameTime );

            MouseState mouseState = Mouse.GetState();

            // Make sure that the mouse is inside the window!
            if( game.Window.ClientBounds.Contains( mouseState.X + game.Window.ClientBounds.X, mouseState.Y + game.Window.ClientBounds.Y ) )
            {
                _rotation = (float) Math.Atan2( mouseState.Y - _sprite.Y, mouseState.X - _sprite.X );

                // Check if the player wishes to shoot
                if ( mouseState.LeftButton == ButtonState.Pressed && !model.DisplayingMessage )
                {
                    float xSpeed = (float) Math.Cos( _rotation );
                    float ySpeed = (float) Math.Sin( _rotation );

                    Vector2 direction = new Vector2( xSpeed, ySpeed );
                    Vector2 position = new Vector2( Position.X + xSpeed * Size.X / 2,
                        Position.Y + ySpeed * Size.Y / 2 );
                    _weapon.Fire( model, direction, position );
                }
            }

            // The player wishes to change the current weapon
            if( mouseState.ScrollWheelValue > _currentScrollValue )
            {
                _currentScrollValue = mouseState.ScrollWheelValue;

                ++_currentWeaponIndex;

                if( _currentWeaponIndex == _weapons.Count )
                    _currentWeaponIndex = 0;
                _weapon = _weapons.ElementAt( _currentWeaponIndex );
            }
            else if( mouseState.ScrollWheelValue < _currentScrollValue )
            {
                _currentScrollValue = mouseState.ScrollWheelValue;

                --_currentWeaponIndex;

                if( _currentWeaponIndex < 0 )
                    _currentWeaponIndex = _weapons.Count - 1;
                _weapon = _weapons.ElementAt( _currentWeaponIndex );
            }

            // Check for keyboard input
            KeyboardState keyState = Keyboard.GetState();

            // Left
            if ( keyState.IsKeyDown( WEST_KEY ) && 0 < Position.X )
                _sprite.X -= _speed;
            // Right
            if( keyState.IsKeyDown( EAST_KEY ) && Position.X < game.Window.ClientBounds.Width )
                _sprite.X += _speed;
            // Up
            if( keyState.IsKeyDown( NORTH_KEY ) && 0 < Position.Y )
                _sprite.Y -= _speed;
            // Down
            if( keyState.IsKeyDown( SOUTH_KEY ) && Position.Y < game.Window.ClientBounds.Height )
                _sprite.Y += _speed;

            // Check if the player has collided with a power up
            foreach( PowerUp powerUp in new List<PowerUp>( model.PowerUps ) )
            {
                // The power up has been collided with, pick it up
                if( Collision( powerUp ) )
                {
                    Game1._soundBank.PlayCue( "powerup_pickup0" );
                    powerUp.Apply( this );
                    _powerUps.Add( powerUp );
                    model.Remove( powerUp );
                }
            }

            // Update the power ups that the player currently have
            foreach( PowerUp powerUp in new List<PowerUp>( _powerUps ) )
                powerUp.UpdateEffect( gameTime, this, _powerUps );
        }
    }
}
