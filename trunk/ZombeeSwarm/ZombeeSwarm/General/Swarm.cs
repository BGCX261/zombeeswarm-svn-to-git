using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace ZombeeSwarm
{
    /// <summary>
    /// Class describing a swarm of enemies.
    /// </summary>
    class Swarm
    {
        private List<EntityEnemy> _enemies;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Swarm()
        {
            _enemies = new List<EntityEnemy>();
        }

        /// <summary>
        /// The list of enemies in the swarms.
        /// </summary>
        public List<EntityEnemy> Enemies
        {
            get{ return _enemies; }
            set{ _enemies = value; }
        }

        /// <summary>
        /// Adds the enemy to the list if it doesn't collide
        /// with another one in it.
        /// </summary>
        /// <param name="enemy"></param>
        public void Add( EntityEnemy enemy )
        {
            foreach( EntityEnemy e in _enemies )
                if( e.Collision( enemy ) )
                    return;
            _enemies.Add( enemy );
        }

        /// <summary>
        /// Removes the enemy at the given position.
        /// </summary>
        /// <param name="position"></param>
        public void Remove( Vector2 position )
        {
            for( int i = _enemies.Count - 1 ; i >= 0 ; --i )
                if( _enemies.ElementAt( i ).Collision( position ) )
                {
                    _enemies.RemoveAt( i );
                    return;
                }
        }
    }
}
