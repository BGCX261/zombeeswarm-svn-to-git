using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombeeSwarm
{
    /// <summary>
    /// Class describing a map, containing and managing swarms
    /// of enemies.
    /// </summary>
    class Map
    {
        private List<Swarm> _swarms = new List<Swarm>();
        private int _currentSwarmIndex = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Map()
        {

        }

        /// <summary>
        /// Sets the current swarm to the given index and returns the
        /// enemies in it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<EntityEnemy> GetSwarm( int index )
        {
            if( index < 0 || _swarms.Count <= index )
                throw new Exception( "Map: Invalid swarm index: " + index );

            _currentSwarmIndex = index;

            return _swarms.ElementAt( index ).Enemies;
        }

        /// <summary>
        /// Returns the first swarm of enemies and sets the current swarm to the
        /// first one.
        /// </summary>
        /// <returns></returns>
        public List<EntityEnemy> GetFirstSwarm()
        {
            _currentSwarmIndex = 0;
            return _swarms.ElementAt( _currentSwarmIndex ).Enemies;
        }

        /// <summary>
        /// Returns the previous swarm of enemies. This will also
        /// set the current swarm to the previous one.
        /// </summary>
        /// <returns></returns>
        public List<EntityEnemy> GetPreviousSwarm()
        {
            if( _currentSwarmIndex == 0 )
                throw new Exception( "Map: Already at the first swarm!" );

            return _swarms.ElementAt( --_currentSwarmIndex ).Enemies;
        }

        public List<EntityEnemy> GetCurrentSwarm()
        {
            return _swarms.ElementAt( _currentSwarmIndex ).Enemies;
        }

        /// <summary>
        /// Returns the next swarm of enemies. This will also
        /// set the current swarm to the next one.
        /// </summary>
        /// <returns></returns>
        public List<EntityEnemy> GetNextSwarm()
        {
            if( IsOnLastSwarm() )
                throw new Exception( "Map: Already at the last swarm!" );

            return _swarms.ElementAt( ++_currentSwarmIndex ).Enemies;
        }

        /// <summary>
        /// Checks wether or not the current swarm is the last one.
        /// </summary>
        /// <returns>true if on the last swarm</returns>
        public bool IsOnLastSwarm()
        {
            return _currentSwarmIndex + 1 == _swarms.Count;
        }

        /// <summary>
        /// The list of swarms.
        /// </summary>
        public List<Swarm> Swarms
        {
            get{ return _swarms; }
            set{ _swarms = value; }
        }
    }
}
