using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hungry_Man
{
    class MoveEventArgs
    {
        private Point place;
        private Point newPlace;

        #region "Constructors"

        public MoveEventArgs()
        {

        }

        public MoveEventArgs(Point place)
        {
            this.place = place;
        }

        public MoveEventArgs(Point place, Point newPlace)
        {
            this.place = place;
            this.newPlace = newPlace;
        }

        #endregion

        #region "Properties"

        public Point Place  
        {
            get { return place; }  
        }

        public Point NewPlace
        {
            get { return newPlace; }
        }

        #endregion
    }
}
