using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    /// <summary>
    /// A class that utilises the factory-pattern in order
    /// to create a randomly chosen PowerUp.
    /// </summary>
    class PowerUpFactory
    {
        /// <summary>
        /// Enum of the available PowerUps.
        /// </summary>
        private enum PowerUps
        {
            HEALTH,
            TEMPORARY_SPEED_BOOST,
            WEAPON_HANDGUN,
            WEAPON_MACHINE_GUN,
            WEAPON_BFG,
            LAST
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public PowerUpFactory()
        {

        }

        /// <summary>
        /// Returns a new power up at the given position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public PowerUp Create( Vector2 position )
        {
            Random r = new Random();

            int number = r.Next( (int) PowerUps.LAST );

            switch( number )
            {
                case (int) PowerUps.HEALTH:
                    return new PowerUpHealth( position );
                case (int) PowerUps.TEMPORARY_SPEED_BOOST:
                    return new PowerUpSpeed( position );
                case (int) PowerUps.WEAPON_HANDGUN:
                    // This is more of a place holder, as the player already have this gun...
                    // return new PowerUpWeaponGun( position );
                case (int) PowerUps.WEAPON_MACHINE_GUN:
                    return new PowerUpWeaponMachineGun( position );
                case (int) PowerUps.WEAPON_BFG:
                    return new PowerUpWeaponBFG( position );
                default:
                    throw new Exception( "PowerUpFactory: Invalid random number was given: " + number );
            }
        }
    }
}
