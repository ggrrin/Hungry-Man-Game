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
    class Eat : IGameObject
    {
        //Static
        public static Texture2D textureNormal;
        public static Texture2D textureEnding;

        //main
        public bool alive { get; set; }
        public Vector2 position { get; set; }
        public Rectangle rectangle { get; set; }
        public Point mainPosition { get; set; }        
        
        //Draw
        private Animation animationNormal;
        private Animation animationEnding;
        private AnimationPlayer animationPlayer;

        //Event
        public delegate void MyEventHandler(object sender, MoveEventArgs e);
        public event MyEventHandler DeleteObject;

        public Eat(Vector2 position)
        {
            this.alive = true;
            this.position = position * Configuration.side;
            this.rectangle = new Rectangle((int)(this.position.X + (Configuration.side * 25f / 80f)), (int)(this.position.Y + (Configuration.side * 25f / 80f)), (int)(Configuration.side * 3f / 8f), (int)(Configuration.side * 3f / 8f));
            this.mainPosition = new Point((int)((position.X + Configuration.side / 2) / Configuration.side), (int)((position.Y + Configuration.side / 2) / Configuration.side));          
            this.animationNormal = new Animation(textureNormal, 3.0f, true);
            this.animationEnding = new Animation(textureEnding, 0.026f, false);
            animationPlayer = new AnimationPlayer(animationNormal);
        }

        public void Update(GameTime time, List<IGameObject>[,] mainArray)
        {
            if (!alive && animationPlayer.Animation != animationEnding)
                    animationPlayer = new AnimationPlayer(animationEnding);            

            if (!animationPlayer.IsPlaying && !alive)
            {
                DeleteEvent(this, mainPosition, new Point(-1, -1));
            }

            animationPlayer.Update(time);
        }

        public void Draw(SpriteBatch sprite, GameTime time)
        {            
                sprite.Draw(animationPlayer.Animation.Texture, position + Configuration.margin, animationPlayer.PartAnimarion,
                    Color.White, 0, Configuration.origin, Configuration.scale, SpriteEffects.None, 0.52f);
        }
    
        #region "Event"

        protected void DeleteEvent(object sender, Point place, Point newPlace)  
        {
            MoveEventArgs e = new MoveEventArgs(place, newPlace); 
            if (DeleteObject != null)
                DeleteObject(sender, e);  
        } 

        #endregion
    }
}
