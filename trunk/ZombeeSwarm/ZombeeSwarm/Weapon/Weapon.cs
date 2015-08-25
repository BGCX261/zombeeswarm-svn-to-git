using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    /// <summary>
    /// Abstract base class for weapons.
    /// </summary>
    abstract class Weapon
    {
        /// <summary>
        /// The base speed of the cooldown.
        /// </summary>
        public const float BASE_COOLDOWN = 1.0f;

        protected Ammunition _ammo;
        private float _updateTimer;
        private bool _canBeFired = true;
        
        protected float _coolDown = BASE_COOLDOWN;

        /// <summary>
        /// Creates a new weapon with the default ammunition.
        /// </summary>
        /// <param name="spriteManager"></param>
        public Weapon()
        {
            init( null );
        }

        /// <summary>
        /// Creates a new weapon with the given ammunition.
        /// </summary>
        /// <param name="spriteManager"></param>
        /// <param name="ammo"></param>
        public Weapon( Ammunition ammo )
        {
            _ammo = ammo;
            init( ammo );
        }

        /// <summary>
        /// Initializes the weapon.
        /// </summary>
        /// <param name="ammo"></param>
        protected abstract void init( Ammunition ammo );

        /// <summary>
        /// Fires the weapon.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="direction"></param>
        /// <param name="position"></param>
        public void Fire( Model model, Vector2 direction, Vector2 position )
        {
            if( !_canBeFired )
                return;

            _canBeFired = false;
            model.Add( _ammo.Clone( direction, position ) );
            Game1._soundBank.PlayCue( "weapon1" );
        }

        /// <summary>
        /// Updates the weapon. Handles the cooldown of the weapon.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update( GameTime gameTime )
        {
            if( _canBeFired )
                return;

            _updateTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if( _updateTimer > _coolDown )
            {
                _updateTimer = 0.0f;
                _canBeFired = true;
            }
        }

        public abstract override string ToString();
    }
}
