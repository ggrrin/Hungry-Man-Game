using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Hungry_Man.Menu;

namespace Hungry_Man.Menu
{
    class Image : IFenceObject
    {
        private Texture2D texture;
        private int width;
        private int height;
        private SizeMode sizeMode; 
        private Rectangle rectangle;
        private Rectangle cutRectangle;
        private Vector2 margin;
        private Color tinge;
        private float rotation;
        private Vector2 origin;
        private SpriteEffects spriteEffects;
        private float layerDepth;

        #region "Properties"

        public Texture2D Texture
        {
            get {return texture;}
            set { texture = value; SetRectangles(); }
        }

        public int Width
        {
            get { return width; }
            set { width = (int)((float)value); SetRectangles(); }
        }

        public int Height
        {
            get { return height; }
            set { height = (int)((float)value); SetRectangles(); }
        }

        public Vector2 Margin
        {
            get { return margin; }
            set { margin = value; }
        }

        public SizeMode SizeMode
        {
            get { return sizeMode; }
            set { sizeMode = value; SetRectangles(); }
        }

        public Color Tinge
        {
            get {return tinge;}
            set { tinge = value; }
        }

        public float Rotation
        {
            get {return rotation;}
            set {rotation = value; }
        }

        public Vector2 Origin
        {
            get {return origin;}
            set { origin = value; }
        }

        public SpriteEffects SpriteEffects
        {
            get {return spriteEffects;}
            set { spriteEffects = value; }
        }

        public float LayerDepth
        {
            get {return layerDepth;}
            set { layerDepth = value; }
        }

        #endregion

        public Image()
        {
            this.texture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMainMenu");
            width = texture.Width;
            height = texture.Height;
            sizeMode = SizeMode.noExtend;
            SetRectangles();
            margin = Vector2.Zero;
            rotation = 0f;
            tinge = Color.White;
            origin = Vector2.Zero;
            spriteEffects = SpriteEffects.None;
            layerDepth = 0.605f;
        }

        public void Update(GameTime time, Vector2 margin)
        {

        }

        public void Draw(SpriteBatch sprite, GameTime time, Vector2 margin)
        {
            sprite.Draw(texture, new Rectangle((int)((rectangle.X + margin.X + this.margin.X) * Configuration.menuScale), (int)((rectangle.Y + margin.Y + this.margin.Y) * Configuration.menuScale), (int)(rectangle.Width * Configuration.menuScale), (int)(rectangle.Height * Configuration.menuScale)), cutRectangle, tinge, rotation, origin, spriteEffects, layerDepth);
        }

        private void SetRectangles()
        {
            if (sizeMode == SizeMode.extedToHeight)
            {
                rectangle = new Rectangle(0, 0, (int)(texture.Width * ((float)height / (float)texture.Height)) > this.width ? this.width : (int)(texture.Width * ((float)height / (float)texture.Height)), height);
                cutRectangle = new Rectangle(0, 0, (int)(texture.Width * ((float)width / (float)(texture.Width * ((float)height / (float)texture.Height)))) > texture.Width ? texture.Width : (int)(texture.Width * ((float)width / (float)(texture.Width * ((float)height / (float)texture.Height)))), texture.Height);
            }
            else if (sizeMode == SizeMode.extedToWidth)
            {
                rectangle = new Rectangle(0, 0, width, (int)(texture.Height * ((float)width / (float)texture.Width)) > this.height ? this.height : (int)(texture.Height * ((float)width / (float)texture.Width)));
                cutRectangle = new Rectangle(0, 0, texture.Width, (int)(texture.Height * ((float)height / (float)(texture.Height * ((float)width / (float)texture.Width)))) > texture.Height ? texture.Height : (int)(texture.Height * ((float)height / (float)(texture.Height * ((float)width / (float)texture.Width)))));
            }
            else if (sizeMode == SizeMode.extend)
            {
                rectangle = new Rectangle(0, 0, width, height);
                cutRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            }
            else if (sizeMode == SizeMode.noExtend)
            {
                rectangle = new Rectangle(0, 0, texture.Width > width ? width : texture.Width, texture.Height > height ? height : texture.Height);
                cutRectangle = rectangle;
            }

        }
    }

    enum SizeMode { noExtend, extend, extedToHeight, extedToWidth }
}
