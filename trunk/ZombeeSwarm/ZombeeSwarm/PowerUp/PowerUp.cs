using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ZombeeSwarm
{
    /// <summary>
    /// Abstract base class for the PowerUps.
    /// </summary>
    abstract class PowerUp : MapObject
    {
        protected float _effectDuration = 0.0f;
        protected float _onMapDuration = 14.0f;
        protected float _updateTimer = 0.0f;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PowerUp()
            : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position"></param>
        public PowerUp( Vector2 position )
            : base( position ) { }

        /// <summary>
        /// This is called at the beginning of the power ups duration.
        /// </summary>
        /// <param name="player"></param>
        public virtual void Apply( EntityPlayer player )
        {
            _updateTimer = 0f;
        }

        /// <summary>
        /// This is called at the end of the power ups duration.
        /// </summary>
        /// <param name="player"></param>
        public abstract void Remove( EntityPlayer player );

        public void Update( GameTime gameTime )
        {
            _updateTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            IsAlive = _updateTimer < _onMapDuration;
        }

        /// <summary>
        /// Counts down the power up depending on the duration.
        /// At the end of the duration, the Remove-method is called.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="player"></param>
        public void UpdateEffect( GameTime gameTime, EntityPlayer player, List<PowerUp> powerUps )
        {
            _updateTimer += gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            if( _updateTimer >= _effectDuration )
            {
                Remove( player );
                powerUps.Remove( this );
            }
        }
    }
}
