using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hungry_Man.Menu
{
    class Fence : IFenceObject
    {
        //Graphic
        private Color backColor;
        private Texture2D backTexture;
        private Image backgroundImage;
        private string text;
        private Vector2 location;
        private Vector2 size;
        private bool visible;
        private float layerDepth;

        //
        private List<IFenceObject> controls;
        private Fence innerFence;

        private GraphicsDevice graphic;

        //events
        public event EventHandler OnUpdate;

        #region "Properties"

        public Texture2D BackImage
        {
            get { return backgroundImage.Texture; }
            set { backgroundImage.Texture = value; }
        
        }

        public SizeMode BackImageSizemode
        {
            get { return backgroundImage.SizeMode; }
            set { backgroundImage.SizeMode = value; }

        }

        public float LayerDepth
        {
            get { return layerDepth; }
            set
            {
                layerDepth = value;
                AktualizeLayers();
                if (backgroundImage != null)
                {
                    backgroundImage.LayerDepth = value + 0.001f;
                }
            }
        }

        public Fence InnerFence
        {
            get { return innerFence; }
            set { innerFence = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            { 
                backColor = value;
                backTexture = new Texture2D(graphic, 1, 1, true, SurfaceFormat.Color);
                Color[] c = new Color[1];
                c[0] = backColor;
                backTexture.SetData<Color>(c);
            }
        }

        public string Text
        {
            get { return text; }
            set{ text = value; }
        }

        public Vector2 Location
        {
            get { return location; }
            set
            {
                location = value;
                location = new Vector2(location.X , location.Y );
            }
        }

        public Vector2 Size
        {
            get { return size; }
            set
            {
                size = new Vector2(value.X , value.Y );
                backgroundImage.Width = (int)value.X;
                backgroundImage.Height = (int)value.Y;
            }
        }

        public List<IFenceObject> Controls
        {
            get { return controls; }
            set { controls = value; }
        }

        #endregion

        public Fence()
        {
            this.backgroundImage = new Image();            
            this.graphic = Configuration.graphicDivice;
            this.controls = new List<IFenceObject>();
            this.Location = new Vector2(0, 0);
            this.Size = new Vector2(800, 600);
            this.Visible = true;
            this.LayerDepth = 0.60f;
        }


        public void Update(GameTime time, Vector2 margin)
        {
            if (visible)
            {
                if (OnUpdate != null)
                    OnUpdate(this, new EventArgs());

                if (innerFence != null)
                {
                    innerFence.Update(time, margin + location);
                }
                else
                {
                    foreach (IFenceObject a in controls)
                    {
                        a.Update(time, margin + location);
                    }
                }
            }
        }

        public void Draw(SpriteBatch sprite, GameTime time, Vector2 margin)
        {
            if (visible)
            {
                if (backTexture != null)
                    sprite.Draw(backTexture, new Rectangle((int)((location.X + margin.X) * Configuration.menuScale), (int)((location.Y + margin.Y) * Configuration.menuScale), (int)(size.X * Configuration.menuScale), (int)(size.Y * Configuration.menuScale)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
                
                if (backgroundImage != null)
                    backgroundImage.Draw(sprite, time, margin + location);

                foreach (IFenceObject a in controls)
                {
                    a.Draw(sprite, time, margin + location);
                }

                if (innerFence != null)
                {
                    innerFence.Draw(sprite, time, margin + location);
                }
            }            
            
        }

        private void CloseInnerFence()
        {
            this.innerFence = null;
        }

        private void AktualizeLayers()
        {
            foreach (IFenceObject i in controls)
            {
                i.LayerDepth = layerDepth;
            }
        }

    }



}
