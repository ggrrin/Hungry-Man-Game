using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Hungry_Man_Map_Editor
{
    class UseEventArgs
    {
        public readonly Point location;
        public readonly int x;
        public readonly int y;
        public Point f;

        public UseEventArgs(Point location, Point f)
        {
            this.location = location;
            this.x = location.X;
            this.y = location.Y;
            this.f = f;
        }

        public UseEventArgs(Point location)
        {
            this.location = location;
            this.x = location.X;
            this.y = location.Y;
        }
        
    }
}
