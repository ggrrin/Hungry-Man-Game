using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Hungry_Man.Menu
{
    class UseKeyboard : IFenceObject
    {
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;

        private MouseState mouse;
        private MouseState previousMouse;

        private List<Button> controls;

        private int selectIndex;
        private float layerDepth;

        #region "Properties"

        public float LayerDepth
        {
            get { return layerDepth; }
            set 
            {
                layerDepth = value;
                AktualizeLayers();

            }
        }

        public int SelectIndex { get { return selectIndex; } set { selectIndex = value; } }

        #endregion

        public UseKeyboard()
        {
            controls = new List<Button>();
        }

        public void Update(GameTime time, Vector2 margin)
        {
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (((keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.Right)) && (previousKeyboard.IsKeyUp(Keys.Down) && previousKeyboard.IsKeyUp(Keys.Right))) || (mouse.ScrollWheelValue == previousMouse.ScrollWheelValue - 120))
            {
                if (selectIndex + 1 < controls.Count)
                    selectIndex++;
            }
            else if (((keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Left)) && (previousKeyboard.IsKeyUp(Keys.Up) && previousKeyboard.IsKeyUp(Keys.Left))) || (mouse.ScrollWheelValue == previousMouse.ScrollWheelValue + 120))
            {
                if (selectIndex - 1 >= 0 )
                    selectIndex--;
            }

            foreach (IFenceObject a in controls)
            {
                a.Update(time, margin);
            }

            previousKeyboard = keyboard;
            previousMouse = mouse;
        }

        public void Draw(SpriteBatch sprite, GameTime time, Vector2 margin)
        {
            foreach (IFenceObject a in controls)
            {
                a.Draw(sprite, time, margin);
            }
        }

        public void Add(Button button)
        {
            controls.Add(button);
            button.UseKeyboard = this;
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
