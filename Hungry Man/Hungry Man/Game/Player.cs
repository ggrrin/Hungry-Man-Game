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

    class Player : IGameObject
    {
        //Static
        public static Texture2D textureNormal;
        public static Texture2D textureEating;
       
        //Life
        public bool alive { get; set; }
        private bool isHurt;
        private TimeSpan timeLeft;
        
        //
        //Update
        //
        //Move
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;
        private float velocity;
        private Directions direction;
        private Directions previousDirection;        
        private Vector2 move;
        public Vector2 position { get; set; }
        private Vector2 countPosition;
        public Rectangle rectangle { get; set; }
        private Rectangle countRectangle;          
        private List<Point> listPositions;
        private RoutePlanner routePlaner;
        public Point mainPosition { get; set; }
        //Eat
        private Rectangle eatRectangle;      
        
        //
        //Draw
        //
        private Animation animationNormal;
        private Animation animationEating;
        private AnimationPlayer animationPlayer;
        public Color color;
        private float rotaion;
        private SpriteEffects effect;

        //
        //Events
        //
        public delegate void MyEventHandler(object sender, MoveEventArgs e);
        public event MyEventHandler DeleteObject;
        public event EventHandler OnEat;
        public event EventHandler OnHurt;

        //
        //Sounds
        //
        private SoundEffect eatSound;
        private SoundEffect hurtSound;

        public Player(Vector2 position)
        {
            this.alive = true;
            this.isHurt = false;
            this.velocity = Configuration.velocity;            
            this.position = position * Configuration.side;
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Configuration.side, Configuration.side);
            this.mainPosition = new Point((int)(position.X), (int)(position.Y)); 
            this.listPositions = new List<Point>();
            this.animationNormal = new Animation(textureNormal, 0.1f, true);
            this.animationEating = new Animation(textureEating, 0.02f, false);
            animationPlayer = new AnimationPlayer(animationNormal);
            color = Color.White;
            Configuration.margin = new Vector2(Configuration.width / 2, Configuration.height / 2) - this.position;

            eatSound = Configuration.content.Load<SoundEffect>("Sounds\\Games\\eating");
            hurtSound = Configuration.content.Load<SoundEffect>("Sounds\\Games\\lessLife");

            OnHurt += new EventHandler(Player_OnHurt);
            OnEat += new EventHandler(Player_OnEat);
        }

        public void Update(GameTime time, List<IGameObject>[,] mainArray)
        {
            Point coutMainPosition = new Point((int)((position.X + Configuration.side / 2) / Configuration.side), (int)((position.Y + Configuration.side / 2) / Configuration.side));

            if (mainPosition != coutMainPosition)
            {
                MoveEvent(this, mainPosition, coutMainPosition);
                mainPosition = coutMainPosition;
            }
            else
            {
                if (isHurt)
                {
                    timeLeft -= time.ElapsedGameTime;

                    if (timeLeft.TotalMilliseconds < 0)
                        isHurt = false;
                }

                GetInput();
                FindLikelyArray(mainArray);
                TreatCollision(mainArray);

                if (direction != Directions.None)
                {
                    previousDirection = direction;
                    Configuration.margin -= move;
                    position += move;
                    rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Configuration.side, Configuration.side);
                }

                if (!animationPlayer.IsPlaying)
                    animationPlayer = new AnimationPlayer(animationNormal);
                animationPlayer.Update(time);
            }

            previousKeyboard = keyboard;
        }      

        public void Draw(SpriteBatch sprite, GameTime time)
        {
            sprite.Draw(animationPlayer.Animation.Texture, position + Configuration.margin,
                animationPlayer.PartAnimarion, color, rotaion, Configuration.origin,
                Configuration.scale, effect, 0.54f);                
        }

        #region "Update"

        private void GetInput()
        {
            keyboard = Keyboard.GetState();
            float usingVelocity = velocity;

            

            if (routePlaner != null)
                if (routePlaner.alive)
                {
                    direction = routePlaner.NextDirection();
                    usingVelocity = routePlaner.NextMovement(velocity);
                }
                else
                    routePlaner = null;

            if (keyboard.IsKeyDown(Keys.Up) && previousKeyboard.IsKeyUp(Keys.Up))
            {
                direction = Directions.Up;
                routePlaner = null;
            }
            else if (keyboard.IsKeyDown(Keys.Down) && previousKeyboard.IsKeyUp(Keys.Down))
            {
                direction = Directions.Down;
                routePlaner = null;
            }
            else if (keyboard.IsKeyDown(Keys.Right) && previousKeyboard.IsKeyUp(Keys.Right))
            {
                direction = Directions.Right;
                routePlaner = null;
            }
            else if (keyboard.IsKeyDown(Keys.Left) && previousKeyboard.IsKeyUp(Keys.Left))
            {
                direction = Directions.Left;
                routePlaner = null;
            }
               
            

            if (direction == Directions.Up)
            {
                this.eatRectangle = new Rectangle((int)(this.position.X + (Configuration.side * 25f / 80f)), (int)this.position.Y, (int)(Configuration.side * 3f / 8f), (int)(Configuration.side * 3f / 8f));
                rotaion = MathHelper.PiOver2;
                effect = SpriteEffects.None;
                move = new Vector2(0, -usingVelocity);
            }
            else if (direction == Directions.Down)
            {
                this.eatRectangle = new Rectangle((int)(this.position.X + (Configuration.side * 25f / 80f)), (int)(this.position.Y + (Configuration.side * 65f / 80f)), (int)(Configuration.side * 3f / 8f), (int)(Configuration.side * 3f / 8f));
                rotaion = -(MathHelper.PiOver2);
                effect = SpriteEffects.None;
                move = new Vector2(0, +usingVelocity);
            }
            else if (direction == Directions.Right)
            {
                this.eatRectangle = new Rectangle((int)(this.position.X + (Configuration.side * 65f / 80f)), (int)(this.position.Y + (Configuration.side * 25f / 80f)), (int)(Configuration.side * 3f / 8f), (int)(Configuration.side * 3f / 8f));

                rotaion = 0;
                effect = SpriteEffects.FlipHorizontally;
                move = new Vector2(+usingVelocity, 0);
            }
            else if (direction == Directions.Left)
            {
                this.eatRectangle = new Rectangle((int)this.position.X, (int)(this.position.Y + (Configuration.side * 25f / 80f)), (int)(Configuration.side * 3f / 8f), (int)(Configuration.side * 3f / 8f));
                rotaion = 0;
                effect = SpriteEffects.None;
                move = new Vector2(-usingVelocity, 0);
            }
            else if (direction == Directions.None)
            {
                rotaion = 0;
                effect = SpriteEffects.None;
                move = Vector2.Zero;
            }

            countPosition = this.position + move;
            countRectangle = new Rectangle((int)countPosition.X, (int)countPosition.Y, Configuration.side, Configuration.side);
            
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
            foreach (Point p in listPositions)
            {
                foreach (IGameObject i in mainArray[p.X, p.Y])
                {
                    if (typeof(Wall) == i.GetType())
                    {
                        if (i.rectangle.Intersects(countRectangle))
                        {
                            if (direction == Directions.Up)
                            {
                                move = new Vector2(0, i.rectangle.Bottom - rectangle.Top);

                                if (move == Vector2.Zero)
                                    if (previousDirection == Directions.Left)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) ])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2(position.X - (i.rectangle.X - Configuration.side), position.Y - i.rectangle.Bottom - 1), previousDirection, direction);

                                    }
                                    else if (previousDirection == Directions.Right)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side)])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2(i.rectangle.Right - position.X,position.Y - i.rectangle.Bottom -1  ), previousDirection, direction);

                                    }


                                break;
                            }
                            else if (direction == Directions.Down)
                            {
                                move = new Vector2(0, i.rectangle.Top - rectangle.Bottom);

                                if (move == Vector2.Zero)
                                    if (previousDirection == Directions.Right)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1 , (int)((i.position.Y + Configuration.side / 2) / Configuration.side)])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }
                                                                                        
                                        }

                                        if(isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }                                               
                                            }

                                        if (isContinue)
                                           routePlaner = new RoutePlanner(new Vector2(i.rectangle.Right - position.X, (i.rectangle.X - 1) - position.Y), previousDirection, direction);

                                    }
                                    else if (previousDirection == Directions.Left)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side)])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2(position.X - i.rectangle.X + Configuration.side, (i.rectangle.X - 1) - position.Y), previousDirection, direction);

                                    }

                                break;
                            }
                            else if (direction == Directions.Right)
                            {
                                move = new Vector2(i.rectangle.Left - rectangle.Right, 0);

                                if (move == Vector2.Zero)
                                    if (previousDirection == Directions.Up)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side), (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2((i.rectangle.X + 1) - position.X,position.Y - (i.rectangle.Y - Configuration.side)), previousDirection, direction);

                                    }
                                    else if (previousDirection == Directions.Down)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) , (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) - 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2((i.rectangle.X + 1) - position.X, i.rectangle.Bottom - position.Y), previousDirection, direction);
                                        
                                    }

                                break;
                            }
                            else if (direction == Directions.Left)
                            {
                                move = new Vector2(i.rectangle.Right - rectangle.Left, 0);

                                if (move == Vector2.Zero)
                                    if (previousDirection == Directions.Up)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) , (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) - 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2(position.X - i.rectangle.X - 1,position.Y - i.rectangle.Y + Configuration.side), previousDirection, direction);

                                    }
                                    else if (previousDirection == Directions.Down)
                                    {
                                        bool isContinue = true;
                                        foreach (IGameObject first in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side), (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                        {
                                            if (first.GetType() == typeof(Wall))
                                            {
                                                isContinue = false;
                                                break;
                                            }

                                        }

                                        if (isContinue)
                                            foreach (IGameObject second in mainArray[(int)((i.position.X + Configuration.side / 2) / Configuration.side) + 1, (int)((i.position.Y + Configuration.side / 2) / Configuration.side) + 1])
                                            {
                                                if (second.GetType() == typeof(Wall))
                                                {
                                                    isContinue = false;
                                                    break;
                                                }
                                            }

                                        if (isContinue)
                                            routePlaner = new RoutePlanner(new Vector2(i.rectangle.Right - 1, i.rectangle.Bottom - position.Y), previousDirection, direction);

                                    }

                                break;
                            }
                        }
                    }

                    if (typeof(Monster) == i.GetType())
                    {
                        if (!isHurt)
                            {
                                if (i.rectangle.Intersects(countRectangle))
                                {
                                    OnHurt(this, new EventArgs());  
                                    isHurt = true;
                                    timeLeft = TimeSpan.FromMilliseconds(4000);
                                    break;
                                }
                            }
                    }
                    if (typeof(Eat) == i.GetType())
                    {
                        if (i.alive)
                        {
                            if (i.rectangle.Intersects(eatRectangle))
                            {
                                i.alive = false;
                                OnEat(this, new EventArgs());
                                animationPlayer = new AnimationPlayer(animationEating);
                                break;
                            }
                        }
                    }

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

        private void Player_OnEat(object sender, EventArgs e)
        {
            eatSound.Play();
        }

        private void Player_OnHurt(object sender, EventArgs e)
        {
            hurtSound.Play();
        }

        #endregion

    }

    enum Directions
    {None, Up, Down, Left, Right};

}
