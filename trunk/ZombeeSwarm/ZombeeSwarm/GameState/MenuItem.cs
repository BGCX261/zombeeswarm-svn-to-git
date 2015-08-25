using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZombeeSwarm.GameState
{
    /// <summary>
    /// A class describing a menu item.
    /// </summary>
    class MenuItem
    {
        /// <summary>
        /// The text of the menu item.
        /// </summary>
        public string Text;
        /// <summary>
        /// True if the item is selected.
        /// </summary>
        public bool IsSelected = false;
        /// <summary>
        /// The next state.
        /// </summary>
        public string NextState;
        /// <summary>
        /// The position.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="nextState"></param>
        public MenuItem( string text, string nextState )
        {
            Text = text;
            NextState = nextState;
        }

        /// <summary>
        /// Checks wether or not the point is on this.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Collision( Vector2 point )
        {
            Rectangle myRectangle = new Rectangle(
                (int) Position.X,
                (int) Position.Y,
                300,
                30 );
            Point myPoint = new Point( (int) point.X, (int) point.Y );

            return myRectangle.Contains( myPoint );
        }
    }
}
