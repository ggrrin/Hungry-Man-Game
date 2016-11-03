﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hungry_Man.Menu
{
    class LevelComplete : Fence
    {
        #region "declaration"

        private Label label;
        private Button nextLevel;
        private Button mainMenu;
        private UseKeyboard keyboardBox;

        public event EventHandler exit;
        public event EventHandler PlayNext;

        #endregion

        public LevelComplete()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //declaration
            label = new Label();

            nextLevel = new Button();
            mainMenu = new Button();

            keyboardBox = new UseKeyboard();

            //Properties

            //keyboardBox
            Controls.Add(keyboardBox);

            //label
            Controls.Add(label);
            label.Location = new Vector2(32, 25);
            label.Size = new Vector2(436, 55);
            label.Text = "Level hotov";
            label.IsCenter = true;
            label.TextSize = 55;

            //nextLevel
            keyboardBox.Add(nextLevel);
            nextLevel.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            nextLevel.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            nextLevel.Location = new Vector2(32, 80);
            nextLevel.Size = new Vector2(436, 98);
            nextLevel.TextSize = 45;
            nextLevel.Index = 0;
            nextLevel.Text = "Další level";
            nextLevel.OnClick += new EventHandler(nextLevel_OnClick);

            //mainMenu
            keyboardBox.Add(mainMenu);
            mainMenu.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            mainMenu.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            mainMenu.Location = new Vector2(32, 190);
            mainMenu.Size = new Vector2(436, 98);
            mainMenu.TextSize = 45;
            mainMenu.Index = 1;
            mainMenu.Text = "Hlavní menu";
            mainMenu.OnClick += new EventHandler(mainMenu_OnClick);

            //fence
            this.Visible = false;
            this.Location = new Vector2(715, 200);
            this.Size = new Vector2(500, 700);
            this.BackImageSizemode = SizeMode.extend;
            this.BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenubackground");

        }

        private void nextLevel_OnClick(object sender, EventArgs e)
        {
            if (PlayNext != null)
                PlayNext(this, new EventArgs());
        }

        private void mainMenu_OnClick(object sender, EventArgs e)
        {
            if (exit != null)
                exit(this, new EventArgs());
        }
    }
}
