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

    class Wall : IGameObject
    {
        //Static
        public static Texture2D staticTexture;

        //main
        public bool alive { get; set; }
        public Vector2 position { get; set; }
        public Rectangle rectangle { get; set; }


        //Draw
        private Texture2D texture;

        public Wall(Vector2 position)
        {
            this.alive = true;          
            this.position = position * Configuration.side;
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, Configuration.side, Configuration.side);
            this.texture = staticTexture;
        }

        public void Update(GameTime time, List<IGameObject>[,] mainArray)
        {

        }

        public void Draw(SpriteBatch sprite, GameTime time )
        {
            sprite.Draw(texture, position + Configuration.margin, null, Color.White, 0, Configuration.origin, Configuration.scale, SpriteEffects.None, 0.55f);
        }
    }
}
