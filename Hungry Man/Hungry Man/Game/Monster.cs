using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;

namespace Hungry_Man
{

    class Monster : IGameObject
    {
        //static
        public static Texture2D textureNormal;
        public static Texture2D textureEating;

        //main
        public bool alive { get; set; }
        public Vector2 position { get; set; }
        public Rectangle rectangle { get; set; }

        //Update
        public float velocity;
        private Directions direction;
        private Vector2 move;
        private Vector2 nowPosition;
        private Rectangle nowRectangle;
        private List<Point> listPositions;
        private Rectangle eatRec;
        private Point mainPosition { get; set; }
        private static Random random = new Random();

        //Draw
        private Animation animationNormal;
        private Animation animationEating;
        private AnimationPlayer animationPlayer;
        private Color color;
        private float rotaion;
        private SpriteEffects effect;

        //Event
        public delegate void MyEventHandler(object sender, MoveEventArgs e);
        public event MyEventHandler DeleteObject;

        public Monster(Vector2 position, int health)
        {
            this.velocity = Configuration.velocity / 4;
            this.alive = true;
            this.position = position * Configuration.side;
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Configuration.side, Configuration.side);
            this.nowRectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Configuration.side, Configuration.side);
            this.mainPosition = new Point((int)(position.X), (int)(position.Y));
            this.listPositions = new List<Point>();
            this.animationNormal = new Animation(textureNormal, 0.1f, true);
            this.animationEating = new Animation(textureEating, 0.1f, false);
            animationPlayer = new AnimationPlayer(animationNormal);
            color = Color.White;
        }

        public void Update(GameTime time, List<IGameObject>[,] mainArray)
        {
            Point a = new Point((int)((position.X + Configuration.side / 2) / Configuration.side), (int)((position.Y + Configuration.side / 2) / Configuration.side));

            if(mainPosition != a)
            {
                MoveEvent(this, mainPosition, a);
                mainPosition = a;
            }
            else
            {
                mainPosition = a;
                GetInput();
                FindLikelyArray(mainArray);
                TreatCollision(mainArray);

                if (direction != Directions.None)
                {
                    position += move;
                    rectangle = new Rectangle((int)this.position.X, (int)this.position.Y,
                    Configuration.side, Configuration.side);
                }

                animationPlayer.Update(time);
            }
        }

        public void Draw(SpriteBatch sprite, GameTime time)
        {
            sprite.Draw(animationPlayer.Animation.Texture, position + Configuration.margin,
                animationPlayer.PartAnimarion, color, rotaion, Configuration.origin,
                Configuration.scale, effect, 0.53f);
        }

        #region "Update"

        private void GetInput()
        {
            int x;

            x = random.Next(1, 100);

            if (x == 1)
            {
                rotaion = MathHelper.Pi / 2;
                effect = SpriteEffects.None;
                direction = Directions.Up;
                move = new Vector2(0, -velocity);
            }
            else if (x == 2)
            {
                rotaion = -(MathHelper.Pi / 2);
                effect = SpriteEffects.None;
                direction = Directions.Down;
                move = new Vector2(0, +velocity);
            }
            else if (x == 3)
            {
                rotaion = 0;
                effect = SpriteEffects.FlipHorizontally;
                direction = Directions.Right;
                move = new Vector2(+velocity, 0);
            }
            else if (x == 4)
            {
                rotaion = 0;
                effect = SpriteEffects.None;
                direction = Directions.Left;
                move = new Vector2(-velocity, 0);
            }

            if (direction != Directions.None)
            {
                nowPosition = this.position + move;
                nowRectangle = new Rectangle((int)nowPosition.X, (int)nowPosition.Y,
                    Configuration.side, Configuration.side);
            }
        }

        private void FindLikelyArray(List<IGameObject>[,] mainArray)
        {
            listPositions.Clear();

            listPositions.Add(mainPosition);
            listPositions.Add(new Point(mainPosition.X - 1, mainPosition.Y - 1));
            listPositions.Add(new Point(mainPosition.X - 1, mainPosition.Y));
            listPositions.Add(new Point(mainPosition.X - 1, mainPosition.Y + 1));
            listPositions.Add(new Point(mainPosition.X, mainPosition.Y + 1));
            listPositions.Add(new Point(mainPosition.X, mainPosition.Y - 1));
            listPositions.Add(new Point(mainPosition.X + 1, mainPosition.Y + 1));
            listPositions.Add(new Point(mainPosition.X + 1, mainPosition.Y));
            listPositions.Add(new Point(mainPosition.X + 1, mainPosition.Y - 1));

            for (int i = 0; i < listPositions.Count; i++)
            {
                if (listPositions[i].X < 0 || listPositions[i].X >= mainArray.GetLength(0))
                {
                    listPositions.Remove(listPositions[i]);
                    i--;
                }
                else if (listPositions[i].Y < 0 || listPositions[i].Y >= mainArray.GetLength(1))
                {
                    listPositions.Remove(listPositions[i]);
                    i--;
                }

            }
        }

        private void TreatCollision(List<IGameObject>[,] mainArray)
        {
            bool a = false;

            foreach (Point p in listPositions)
            {
                foreach (IGameObject i in mainArray[p.X, p.Y])
                {
                    if (typeof(Wall) == i.GetType())
                    {
                        if (i.rectangle.Intersects(nowRectangle))
                        {
                            
                            if (direction == Directions.Up)
                            {
                                move = new Vector2(0, i.rectangle.Bottom - rectangle.Top);
                                a = true;
                                break;
                            }
                            else if (direction == Directions.Down)
                            {
                                move = new Vector2(0, i.rectangle.Top - rectangle.Bottom);
                                a = true;
                                break;
                            }
                            else if (direction == Directions.Right)
                            {
                                move = new Vector2(i.rectangle.Left - rectangle.Right, 0);
                                a = true;
                                break;
                            }
                            else if (direction == Directions.Left)
                            {
                                move = new Vector2(i.rectangle.Right - rectangle.Left, 0);
                                a = true;
                                break;
                            }
                        }
                    }
                }
                if (a)
                {
                    a = false;
                    break;
                }
            }
        }

        #endregion

        #region "Event"
        protected void MoveEvent(object sender, Point place, Point newPlace)
        {
            MoveEventArgs e = new MoveEventArgs(place, newPlace);
            if (DeleteObject != null)
                DeleteObject(sender, e);
        }
        #endregion
    }


}
