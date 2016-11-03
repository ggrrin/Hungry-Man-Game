using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Hungry_Man.Menu
{
    class GameSubMenu : Fence
    {
        #region "Declaration"

        private Label title;
        private Button resumeButton;
        private Button saveGameButton;
        private Button mainMenuButton;
        private Button resetLevelButton;


        private UseKeyboard keyboardBox;

        #endregion

        public event EventHandler Close;
        public event EventHandler GoToMainMenu;
        public event EventHandler PlayAgain;

        

        public GameSubMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Created new instancs
            title = new Label();

            resumeButton = new Button();
            saveGameButton = new Button();
            mainMenuButton = new Button();
            resetLevelButton = new Button();

            keyboardBox = new UseKeyboard();

            //useKeyboard
            Controls.Add(keyboardBox);

            //title
            title.Text = "Herní menu";
            title.IsMultiLine = false;
            title.IsCenter = true;
            title.Location = new Vector2(32, 25);
            title.Size = new Vector2(436, 55);
            title.TextSize = 55;
            Controls.Add(title);

            //resumebutton
            keyboardBox.Add(resumeButton);
            resumeButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            resumeButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            resumeButton.Location = new Vector2(32, 80);
            resumeButton.Size = new Vector2(436, 98);
            resumeButton.TextSize = 45;
            resumeButton.Index = 0;
            resumeButton.Text = "Pokračovat";
            resumeButton.OnClick += new EventHandler(resumeButton_OnClick);

            //resetLevel
            keyboardBox.Add(resetLevelButton);
            resetLevelButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            resetLevelButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            resetLevelButton.Location = new Vector2(32, 190);
            resetLevelButton.Size = new Vector2(436, 98);
            resetLevelButton.TextSize = 45;
            resetLevelButton.Index = 1;
            resetLevelButton.Text = "Restartovat level";
            resetLevelButton.OnClick += new EventHandler(resetLevel_OnClick);

            //mainMenuButton
            keyboardBox.Add(mainMenuButton);
            mainMenuButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            mainMenuButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            mainMenuButton.Location = new Vector2(32, 300);
            mainMenuButton.Size = new Vector2(436, 98);
            mainMenuButton.TextSize = 45;
            mainMenuButton.Index = 2;
            mainMenuButton.Text = "Hlavní menu";
            mainMenuButton.OnClick += new EventHandler(mainMenuButton_OnClick);

            //GameSubMenu
            this.Visible = false;
            this.Location = new Vector2(715, 200);
            this.Size = new Vector2(500, 700);
            this.BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenubackground");
            this.BackImageSizemode = SizeMode.extend;
            this.Close += new EventHandler(GameSubMenu_Close);
        }

        private void GameSubMenu_Close(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void resumeButton_OnClick(object sender, EventArgs e)
        {
            if (Close != null)
                Close(this, new EventArgs());                
        }

        private void mainMenuButton_OnClick(object sender, EventArgs e)
        {
            if (GoToMainMenu != null)
                GoToMainMenu(this, new EventArgs());
            
        }

        private void resetLevel_OnClick(object sender, EventArgs e)
        {
            if (PlayAgain != null)
                PlayAgain(this, new EventArgs());
        }
    }
}
