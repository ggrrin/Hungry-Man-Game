using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hungry_Man.Menu
{
    class InformationPanel : Fence
    {
        #region "declaration"

        public Label health;
        public Label eat;
        
        #endregion

        public InformationPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //declaration
            health = new Label();
            eat = new Label();
            //Properties

            //health
            Controls.Add(health);
            health.Location = new Vector2(30, 0);
            health.Size = new Vector2(350, 100);
            health.Text = "Health ";
            health.IsCenter = true;
            health.TextSize = 30;

            //eat
            Controls.Add(eat);
            eat.Location = new Vector2(1450, 0);
            eat.Size = new Vector2(400, 100);
            eat.Text = "z";
            eat.IsCenter = true;
            eat.TextSize = 30;

            //InformationPanel
            this.Size = new Vector2(1921, 100);
            this.Location = new Vector2(0, 0);
            this.BackImage = Configuration.content.Load<Texture2D>("Textures\\Menu\\InformationPanel");

        }
    }
}
