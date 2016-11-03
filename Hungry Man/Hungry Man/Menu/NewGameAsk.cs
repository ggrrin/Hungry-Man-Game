using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hungry_Man.Menu
{
    class NewGameAsk : Fence 
    {
        private Label text;
        private Button yes;
        private Button no;
        private UseKeyboard keyboardBox;

        public delegate void ExitEventHandler(object sender, ExitEventArgs e);
        public event ExitEventHandler Exit;

        public NewGameAsk()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            //Create new instance
            text = new Label();
            yes = new Button();
            no = new Button();

            keyboardBox = new UseKeyboard();

            //kyeboardBox
            Controls.Add(keyboardBox);

            //text
            Controls.Add(text);
            text.Text = "Opravdu chcete začít novou hru? Předchozí hra bude ztracena!";
            text.IsMultiLine = true;
            text.IsMultiLine = false;
            text.IsCenter = true;
            text.Location = new Vector2(10, 90);
            text.Size = new Vector2(880, 40);
            text.TextSize = 35;

            //yes
            keyboardBox.Add(yes);
            yes.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            yes.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            yes.Location = new Vector2(150, 150);
            yes.Size = new Vector2(250, 70);
            yes.TextSize = 35;
            yes.Index = 0;
            yes.Text = "Ano";
            yes.OnClick += new EventHandler(yes_OnClick);

            //no
            keyboardBox.Add(no);
            no.BackTexture = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton0");
            no.BackTextureHover = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMenuButton1");
            no.Location = new Vector2(500, 150);
            no.Size = new Vector2(250, 70);
            no.TextSize = 35;
            no.Index = 1;
            no.Text = "Ne";
            no.OnClick += new EventHandler(no_OnClick);

            //this Fenece
            this.Size = new Vector2(900 ,300);
            this.Location = new Vector2(510, 390);
            this.BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\SubMainMenu");
            this.BackImageSizemode = SizeMode.extend;
            LayerDepth = 0.65f;            
        }

        private void PrepareExitEventArgs(bool isContinue)
        {
            ExitEventArgs e = new ExitEventArgs();
            e.IsContinue = isContinue;
            if (Exit != null)
            {
                Exit(this, e);
            }
        }

        #region "Events"
        
        private void no_OnClick(object sender, EventArgs e)
        {
            PrepareExitEventArgs(false);
        }

        private void yes_OnClick(object sender, EventArgs e)
        {
            PrepareExitEventArgs(true);
        }

        #endregion
    }

}
