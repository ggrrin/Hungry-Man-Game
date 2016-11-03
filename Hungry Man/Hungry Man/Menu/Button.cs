using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Hungry_Man.Menu
{
    class Button : IFenceObject
    {
        private UseKeyboard useKeyboard;

        //mouse
        private MouseState mouse;
        private MouseState previousMouse;
        private Rectangle mouseRec;

        //keyboard
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;

        //Graphic
        private Texture2D backTexture;
        private Texture2D backTextureHover;
        private Color tinge;
        private Color tingeHover;
        private Vector2 location;
        private Vector2 size;        
        private Label label;
        private int index;
        private bool visible;

        //Events
        public event EventHandler OnClick;
        public event EventHandler MouseEnter;

        //using
        private Texture2D usingTexture;
        private Color usingTinge;
        private float layerDepth;
        private bool isSelect;

        //sound
        private SoundEffect mouseEnterSound;
        private SoundEffect onClickSound;


        #region "Properties"

        public SoundEffect OnClickSound
        {
            get { return onClickSound; }
            set { onClickSound = value; }
        }

        

        public SoundEffect MouseEnterSound
        {
            get { return mouseEnterSound; }
            set { mouseEnterSound = value; }
        }

        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value + 0.01f; label.LayerDepth = this.layerDepth + 0.01f; }
        }

        public UseKeyboard UseKeyboard
        { 
            get { return useKeyboard; } set { useKeyboard = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; label.Visible = value; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        public Texture2D BackTexture
        {
            get { return backTexture; }
            set { backTexture = value; }
        }

        public Texture2D BackTextureHover
        {
            get { return backTextureHover; }
            set { backTextureHover = value; }
        }

        public Color Tinge
        {
            get { return tinge; }
            set { tinge = value; }
        }

        public Color TingeHover
        {
            get { return tingeHover; }
            set { tingeHover = value; }
        }

        public string Text
        {
            get { return label.Text; }
            set
            {
                label.Text = value;
            }
        }

        public float TextSize
        {
            get
            {
                return label.TextSize;
            }
            set
            {
                label.TextSize = value;
            }
        }

        public SpriteFont Font
        {
            get { return label.Font; }
            set { label.Font = value; }
        }

        public Color TextColor
        {
            get { return label.Color; }
            set { label.Color = value; }
        }

        public Vector2 Location
        {
            get { return location; }
            set { location = value; location = new Vector2(location.X, location.Y); }
        }

        public Vector2 Size
        {
            get { return size; }
            set
            {
                size = new Vector2(value.X , value.Y);
                label.Size = value;
            }
        }

        #endregion

        public Button()
        {
            label = new Label();
            BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            MouseEnterSound = Configuration.content.Load<SoundEffect>("Sounds\\Menus\\button1");
            OnClickSound = Configuration.content.Load<SoundEffect>("Sounds\\Menus\\button2");
            usingTexture = backTexture; 
            tinge = Color.White;
            tingeHover = Color.White;
            Size = new Vector2(150, 50);
            Location = new Vector2(0, 0);            
            label.Text = "Button";
            label.IsCenter = true;
            label.Size = size;
            index = -1;
            Visible = true;
            previousMouse = Mouse.GetState();
            previousKeyboard = Keyboard.GetState();
            layerDepth = 0.61f;

            MouseEnter += new EventHandler(Button_MouseEnter);
            OnClick += new EventHandler(Button_OnClick);

        }

        public void Update(GameTime time, Vector2 margin)
        {
            if (visible)
            {
                //mouse
                mouse = Mouse.GetState();
                mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

                //keyboard
                keyboard = Keyboard.GetState();

                bool use = false;

                if (useKeyboard != null)
                {
                    if (index == useKeyboard.SelectIndex)
                        use = true;
                }

                bool isMouseRec = new Rectangle((int)((location.X + margin.X) * Configuration.menuScale), (int)((location.Y + margin.Y) * Configuration.menuScale), (int)(size.X * Configuration.menuScale), (int)(size.Y * Configuration.menuScale)).Intersects(mouseRec);

                if (isMouseRec || use)
                {
                    if (useKeyboard != null)
                        useKeyboard.SelectIndex = index;

                    if (!isSelect)
                    {
                        isSelect = true;
                        if (MouseEnter != null)
                            MouseEnter(this, new EventArgs());
                        
                    }

                    usingTexture = backTextureHover;
                    usingTinge = tingeHover;

                    if ((mouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released && isMouseRec) || (keyboard.IsKeyDown(Keys.Enter) && previousKeyboard.IsKeyUp(Keys.Enter)))
                        if (OnClick != null)
                            OnClick(this, new EventArgs());
                }
                else
                {
                    usingTexture = backTexture;
                    usingTinge = tinge;
                    isSelect = false;
                }

                label.Update(time, new Vector2(location.X + margin.X, location.Y + margin.Y));

                previousMouse = mouse;
                previousKeyboard = keyboard;
            }
        }

        public void Draw(SpriteBatch sprite, GameTime time, Vector2 margin)
        {
            if (visible)
            {

                sprite.Draw(usingTexture, new Rectangle((int)((location.X + margin.X) * Configuration.menuScale), (int)((location.Y + margin.Y) * Configuration.menuScale), (int)(size.X * Configuration.menuScale), (int)(size.Y * Configuration.menuScale)), null, usingTinge, 0, Vector2.Zero, SpriteEffects.None, layerDepth);
                label.Draw(sprite, time, new Vector2(margin.X + location.X, margin.Y + location.Y));
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            mouseEnterSound.Play();
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            onClickSound.Play();
        }
    }
}
