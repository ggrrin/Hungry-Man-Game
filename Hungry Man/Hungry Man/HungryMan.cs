using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Hungry_Man.Menu;

namespace Hungry_Man
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class HungryMan : Microsoft.Xna.Framework.Game
    {
        private Song song;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Game game;
        MainMenu menu;

        public HungryMan()
        {
            IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = false;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Exiting += new EventHandler<EventArgs>(HungryMan_Exiting);

            Configuration.Initialize(graphics, Services);         
            InitializeMainMenu();


            song = Configuration.content.Load<Song>("Sounds\\Menus\\Music");
            try
            {
                MediaPlayer.Play(song);
            }

            finally
            {
                Console.WriteLine("Error in playing music.");
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Player
            Player.textureNormal = Content.Load<Texture2D>("Textures\\Game\\HungryMan");
            Player.textureEating = Content.Load<Texture2D>("Textures\\Game\\HungryManEat");

            //Monster
            Monster.textureNormal = Configuration.content.Load<Texture2D>("Textures\\Game\\Monster");
            //Monster.staticTextureEating = Configuration.content.Load<Texture2D>("Textures\\Wall");

            //Eat
            Eat.textureNormal = Content.Load<Texture2D>("Textures\\Game\\Eat");
            Eat.textureEnding = Content.Load<Texture2D>("Textures\\Game\\EatDisappear");

            //Wall
            Wall.staticTexture = Content.Load<Texture2D>("Textures\\Game\\Wall");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (game != null)
                game.Update(gameTime);
            if (menu != null)
                menu.Update(gameTime, new Vector2(0, 0));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(30, 30, 30));

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            if(game != null)
                game.Draw(spriteBatch, gameTime);

            if (menu != null)
                menu.Draw(spriteBatch, gameTime, new Vector2(0, 0));

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region "Events"

        private void menu_OnExit(object sender, EventArgs e)
        {
            this.Exit();
        }

        private void menu_OnStartGame(object sender, ExiEventArgs e)
        {
            InitializeGame(e);
            menu = null;
        }

        private void game_GotoMenu(object sender, EventArgs e)
        {
            InitializeMainMenu();
            game = null;
        }

        private void InitializeMainMenu()
        {
            menu = new MainMenu();
            menu.OnExit += new EventHandler(menu_OnExit);
            menu.OnStartGame += new MainMenu.StarGameEventHandler(menu_OnStartGame); 
        }

        private void InitializeGame( ExiEventArgs e)
        {         
            game = new Game(e.LevelPath, graphics.GraphicsDevice);
            game.GotoMenu += new EventHandler(game_GotoMenu);
            game.PlayNextMap += new Game.StarGameEventHandler(game_PlayNextMap);
            game.RestartLevel += new Game.StarGameEventHandler(game_RestartLevel);
        }

        private void HungryMan_Exiting(object sender, EventArgs e)
        {
            Configuration.SaveProfile();
        }

        private void game_PlayNextMap(object sender, ExiEventArgs e)
        {
            InitializeGame(e);
        }

        private void game_RestartLevel(object sender, ExiEventArgs e)
        {
            InitializeGame(e);
        }

        #endregion

    }
}
