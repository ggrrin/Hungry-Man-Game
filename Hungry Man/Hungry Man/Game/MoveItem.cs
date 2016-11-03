using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hungry_Man
{
    struct MoveItem
    {
        public IGameObject item;
        public Point place;
        public Point newPlace;

        public MoveItem(IGameObject item, Point place, Point newPlace)
        {
            this.item = item;
            this.place = place;
            this.newPlace = newPlace;
        }
    }
}
