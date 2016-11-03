using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hungry_Man.Menu
{
    class LevelUncomplete : Fence
    {
        #region "declaration"

        private Label label;
        private Button resetLevelButton;
        private Button mainMenuButton;
        private UseKeyboard keyboardBox;

        public event EventHandler exit;
        public event EventHandler PlayAgain;

        #endregion

        public LevelUncomplete()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //declaration
            label = new Label();

            resetLevelButton = new Button();
            mainMenuButton = new Button();

            keyboardBox = new UseKeyboard();

            //Properties

            //keyboardBox
            Controls.Add(keyboardBox);

            //title
            label.Text = "Jsi mrtev";
            label.IsMultiLine = false;
            label.IsCenter = true;
            label.Location = new Vector2(32, 25);
            label.Size = new Vector2(436, 55);
            label.TextSize = 55;
            Controls.Add(label);

            //resetLevel
            keyboardBox.Add(resetLevelButton);
            resetLevelButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            resetLevelButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            resetLevelButton.Location = new Vector2(32, 80);
            resetLevelButton.Size = new Vector2(436, 98);
            resetLevelButton.TextSize = 45;
            resetLevelButton.Index = 0;
            resetLevelButton.Text = "Restartovat level";
            resetLevelButton.OnClick += new EventHandler(resetLevel_OnClick);


            //mainMenuButton
            keyboardBox.Add(mainMenuButton);
            mainMenuButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            mainMenuButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            mainMenuButton.Location = new Vector2(32, 190);
            mainMenuButton.Size = new Vector2(436, 98);
            mainMenuButton.TextSize = 45;
            mainMenuButton.Index = 1;
            mainMenuButton.Text = "Hlavní menu";
            mainMenuButton.OnClick += new EventHandler(mainMenuButton_OnClick);

            //GameSubMenu
            this.Visible = false;
            this.Location = new Vector2(715, 200);
            this.Size = new Vector2(500, 700);
            this.BackImageSizemode = SizeMode.extend;
            this.BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenubackground");

        }

        private void resetLevel_OnClick(object sender, EventArgs e)
        {
            if (PlayAgain != null)
                PlayAgain(this, new EventArgs());
        }

        private void mainMenuButton_OnClick(object sender, EventArgs e)
        {
            if (exit != null)
                exit(this, new EventArgs());
        }
    }
}
