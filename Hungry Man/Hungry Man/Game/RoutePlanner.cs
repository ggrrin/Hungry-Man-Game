using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Hungry_Man
{
    class RoutePlanner
    {
        public bool alive;

        float firstMove;
        float secondMove;

        Directions firstDirection;
        Directions secondDirection;

        public RoutePlanner(Vector2 wholeMovement, Directions firstDirection, Directions secondDirection)
        {
            this.alive = true;
            this.firstDirection = firstDirection;
            this.secondDirection = secondDirection;

            if (firstDirection == Directions.Right || firstDirection == Directions.Left)
            {
                this.firstMove = wholeMovement.X;
                this.secondMove = wholeMovement.Y;
            }
            else
            {
                this.firstMove = wholeMovement.Y;
                this.secondMove = wholeMovement.X;
            }            

            if (firstMove < 0 )
                firstMove *= -1;

            if (secondMove < 0)
                secondMove *= -1;
        }

        public Directions NextDirection()
        {
            if (firstMove != 0)
                return firstDirection;
            else
                return secondDirection;
        }

        public float NextMovement(float velocity)
        {
            if (firstMove != 0)
            {
                if (firstMove - velocity >= 0)
                {
                    firstMove -= velocity;
                    return velocity;
                }
                else
                {
                    var between = firstMove;
                    firstMove = 0;
                    return between;
                }
            }
            else
            {
                if (secondMove - velocity >= 0)
                {
                    secondMove -= velocity;
                    return velocity;
                }
                else
                {
                    alive = false;
                    return secondMove;                    
                }
            }            
        }
    }
}
