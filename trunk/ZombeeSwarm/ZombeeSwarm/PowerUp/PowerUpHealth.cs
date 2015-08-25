using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    /// <summary>
    /// Recovers health to the player.
    /// </summary>
    class PowerUpHealth : PowerUp
    {
        public PowerUpHealth()
            : base()
        {
        }

        public PowerUpHealth( Vector2 position )
            : base( position )
        {
        }

        protected override void init( Vector2 position )
        {
            _sprite = SpriteManager.CreateSprite( position, "powerup-health" );
        }

        public override void Apply( EntityPlayer player )
        {
            base.Apply( player );
            player.AddHealth( 25.0f );
        }

        public override void Remove( EntityPlayer player )
        {

        }
    }
}
