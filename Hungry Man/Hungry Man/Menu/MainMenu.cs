using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Hungry_Man.Menu
{
    class MainMenu : Fence
    {

        #region "Declaration"

        private Label text;

        private Button resumeButton;
        private Button newGameButton;
        private Button settingButton;
        private Button exitButton;

        private UseKeyboard keyboardBox;

        #endregion

        //events
        public event EventHandler OnExit;

        public delegate void StarGameEventHandler(object sender, ExiEventArgs e);
        public event StarGameEventHandler OnStartGame;
        

        public MainMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Created new instancs
            text = new Label();

            resumeButton = new Button();
            newGameButton = new Button();
            settingButton = new Button();
            exitButton = new Button();
            keyboardBox = new UseKeyboard();

            //text
            text.Text = "Hungry Man";
            text.Color = Color.Black;
            text.IsMultiLine = true;
            text.Location = new Vector2(950, 700);
            text.Size = new Vector2(900, 600);
            text.TextSize = 200;

            //resumeButton
            keyboardBox.Add(resumeButton);
            resumeButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            resumeButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton1");
            resumeButton.Location = new Vector2(1300, 50);
            resumeButton.Size = new Vector2(475, 122);
            resumeButton.TextSize = 55;
            resumeButton.Index = 0;
            resumeButton.Text = "Pokračovat";
            resumeButton.OnClick += new EventHandler(resumeButton_OnClick);

            //newGameButton
            keyboardBox.Add(newGameButton);
            newGameButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            newGameButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton1");
            newGameButton.Location = new Vector2(1300, 175);
            newGameButton.Size = new Vector2(475, 122);
            newGameButton.TextSize = 55;
            newGameButton.Index = 1;
            newGameButton.Text = "Nová Hra";
            newGameButton.OnClick += new EventHandler(newGameButton_OnClick);

            //settingButton
            keyboardBox.Add(settingButton);
            settingButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            settingButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton1");
            settingButton.Location = new Vector2(1300, 300);
            settingButton.Size = new Vector2(475, 122);
            settingButton.TextSize = 55;
            settingButton.Index = 2;
            settingButton.Text = "Nastavení";
            settingButton.OnClick += new EventHandler(settingButton_OnClick);

            //exitButton
            keyboardBox.Add(exitButton);
            exitButton.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton0");
            exitButton.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainButton1");
            exitButton.Location = new Vector2(1300, 425);
            exitButton.Size = new Vector2(475, 122);
            exitButton.TextSize = 55;
            exitButton.Index = 3;
            exitButton.Text = "Ukončit";
            exitButton.OnClick += new EventHandler(exitButton_OnClick);

            //keyboardBox
            Controls.Add(keyboardBox);
            Controls.Add(text);

            //Mainmenu 
            Size = new Vector2(1921, 1536);
            Location = new Vector2(0, 0);
            BackColor = new Color(166, 218, 232);
            BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\MainMenuBackground");
            this.BackImageSizemode = SizeMode.extedToWidth;
        }

        private void SetStartGameEventArgs(int level)
        {
            if (OnStartGame != null)
            {
                ExiEventArgs e = new ExiEventArgs();
                e.LevelPath = level;
                OnStartGame(this, e);
            }
        }

        #region "Events"

        private void exitButton_OnClick(object sender, EventArgs e)
        {
            if (OnExit != null)
                OnExit(this, new EventArgs());
        }

        private void newGameButton_OnClick(object sender, EventArgs e)
        {
            NewGameAsk newGameAsk = new NewGameAsk();
            newGameAsk.Exit += new NewGameAsk.ExitEventHandler(newGameAsk_Exit);
            InnerFence = newGameAsk;       
        }   

        private void resumeButton_OnClick(object sender, EventArgs e)
        {
            SetStartGameEventArgs(Configuration.useProfile.LastLevel);
        }

        private void settingButton_OnClick(object sender, EventArgs e)
        {
            SettingMenu settingMenu = new SettingMenu();
            settingMenu.Exit += new EventHandler(settingMenu_Exit);
            this.InnerFence = settingMenu;
            
            
        }

        private void settingMenu_Exit(object sender, EventArgs e)
        {
            InnerFence = null;
        }

        private void newGameAsk_Exit(object sender, ExitEventArgs e)
        {
            InnerFence = null;

            if (e.IsContinue)
            {
                Configuration.useProfile = new Profile();
                Configuration.SaveProfile();
                SetStartGameEventArgs(Configuration.useProfile.LastLevel);
            }

        }

        #endregion
    }
}
