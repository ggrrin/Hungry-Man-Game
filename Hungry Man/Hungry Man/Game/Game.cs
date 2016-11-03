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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Hungry_Man.Menu;

namespace Hungry_Man
{
    class Game
    {
        //Map
        private int level;
        private Hungry_Man_Map_Editor.Map data;        
        private List<IGameObject>[,] mainArray;

        //Player
        private Player player;
        private int health;
        private int score;
        private int scoreToEnd;
        private List<MoveItem> deleteList;
        
        //SubMenus
        private GameSubMenu subMenu;
        private LevelComplete levelComplete;
        private InformationPanel informationPanel;
        private LevelUncomplete levelUncomplete;

        //Update
        private bool isStoped;      
        private KeyboardState keyboard;
        private KeyboardState previousKeyboard;

        //Events
        public delegate void StarGameEventHandler(object sender, ExiEventArgs e);        
        public event EventHandler GotoMenu;
        public event StarGameEventHandler PlayNextMap;
        public event StarGameEventHandler RestartLevel;

        public event EventHandler PlayerDie;
        public event EventHandler LevelComplete;

        //Sounds
        private SoundEffect playerDieSound;
        private SoundEffect levelCompleteSound;


        public Game(int level, GraphicsDevice graphic)
        {
            this.level = level;
            this.health = 3;
            this.score = 0;
            this.isStoped = false;
            this.deleteList = new List<MoveItem>();
            //subMenu
            this.subMenu = new GameSubMenu();
            this.subMenu.GoToMainMenu += new EventHandler(subMenu_GoToMainMenu);
            this.subMenu.PlayAgain += new EventHandler(levelUncomplete_playAgain);
            //levelComplete
            this.levelComplete = new LevelComplete();
            this.levelComplete.PlayNext += new EventHandler(levelComplete_playNext);
            this.levelComplete.exit += new EventHandler(levelComplete_exit);
            //levelUncomplete
            this.levelUncomplete = new LevelUncomplete();
            this.levelUncomplete.PlayAgain += new EventHandler(levelUncomplete_playAgain);
            this.levelUncomplete.exit += new EventHandler(levelComplete_exit);
            //InformationPanel
            informationPanel = new InformationPanel();

            playerDieSound = Configuration.content.Load<SoundEffect>("Sounds\\Games\\die");
            levelCompleteSound = Configuration.content.Load<SoundEffect>("Sounds\\Games\\nextLevel");

            LevelComplete += new EventHandler(Game_LevelComplete);
            PlayerDie += new EventHandler(Game_PlayerDie);

            LoadMap();           
        }

        private void LoadMap()
        {
            using (Stream path = new FileStream(string.Format("Content\\Maps\\{0}.hmm", level), FileMode.Open))
            {
                IFormatter formatter = new BinaryFormatter();
                data = (Hungry_Man_Map_Editor.Map)formatter.Deserialize(path);
            }

            mainArray = new List<IGameObject>[data.mainArray.GetLength(0), data.mainArray.GetLength(1)];

            for (int y = 0; y < data.mainArray.GetLength(1); y++)
                for (int x = 0; x < data.mainArray.GetLength(0); x++)
                {
                    mainArray[x, y] = new List<IGameObject>();
                }

            for (int y = 0; y < data.mainArray.GetLength(1); y++)
                for (int x = 0; x < data.mainArray.GetLength(0); x++)
                {
                    foreach (Hungry_Man_Map_Editor.Objects editorObject in data.mainArray[x, y])
                    {
                        switch (editorObject)
                        {
                            case Hungry_Man_Map_Editor.Objects.Wall:
                                mainArray[x, y].Add(new Wall(new Vector2(x, y)));
                                break;

                            case Hungry_Man_Map_Editor.Objects.Player:
                                player = new Player(new Vector2(x, y));
                                player.DeleteObject += new Player.MyEventHandler(p_DeleteObject);
                                player.OnEat += new EventHandler(player_OnEat);
                                player.OnHurt += new EventHandler(player_OnHurt);
                                mainArray[x, y].Add(player);
                                break;

                            case Hungry_Man_Map_Editor.Objects.Monster:
                                Monster monster = new Monster(new Vector2(x, y), 1);
                                monster.DeleteObject += new Monster.MyEventHandler(p_DeleteObject);
                                mainArray[x, y].Add(monster);
                                break;

                            case Hungry_Man_Map_Editor.Objects.Eat:
                                Eat eat = new Eat(new Vector2(x, y));
                                eat.DeleteObject += new Eat.MyEventHandler(p_DeleteObject);
                                mainArray[x, y].Add(eat);
                                scoreToEnd++;
                                break;
                        }
                    }
                }
        }

        public void Update(GameTime time)
        {
            keyboard = Keyboard.GetState();

            if (health == 0)
            {
                player.alive = false;
                if (PlayerDie != null)
                    PlayerDie(this, new EventArgs());
                health = -1;
            }

            if (!player.alive)
            {   
                isStoped = true;
                levelUncomplete.Visible = true;
            }

            if (scoreToEnd == score)
            {
                if (LevelComplete != null)
                    LevelComplete(this, new EventArgs());

                Configuration.useProfile.CompleteLevel(level);
                levelComplete.Visible = true;
                isStoped = true;
                score = 0;
            }
            else if (keyboard.IsKeyDown(Keys.Escape) && previousKeyboard.IsKeyUp(Keys.Escape))
            {
                if (subMenu.Visible)
                {
                    subMenu.Visible = false;
                    isStoped = false;
                }
                else
                {
                    subMenu.Visible = true;
                    isStoped = true;
                }
            }

            subMenu.Update(time, new Vector2(0, 0));            
            levelComplete.Update(time, new Vector2(0, 0));
            levelUncomplete.Update(time, new Vector2(0, 0));

            informationPanel.health.Text = string.Format("Zdraví {0}", health);
            informationPanel.eat.Text = string.Format("Snědeno {0} z {1}", score, scoreToEnd);
            informationPanel.Update(time, new Vector2(0, 0));

            if (!isStoped)
            {
                for (int y = player.mainPosition.Y - Configuration.brickOnHeight / 2 < 0 ? 0 : player.mainPosition.Y - Configuration.brickOnHeight / 2;
                    y < (player.mainPosition.Y + Configuration.brickOnHeight / 2 > mainArray.GetLength(1) ? mainArray.GetLength(1) : player.mainPosition.Y + Configuration.brickOnHeight / 2); y++)

                    for (int x = player.mainPosition.X - Configuration.bricksOnWidth / 2 < 0 ? 0 : player.mainPosition.X - Configuration.bricksOnWidth / 2;
                        x < (player.mainPosition.X + Configuration.bricksOnWidth / 2 > mainArray.GetLength(0) ? mainArray.GetLength(0) : player.mainPosition.X + Configuration.bricksOnWidth / 2 ); x++)
                    {
                        foreach (IGameObject a in mainArray[x, y])
                        {
                            a.Update(time, mainArray);
                        }

                        if (deleteList.Count != 0)
                        {
                            foreach (MoveItem b in deleteList)
                            {
                                if (b.newPlace.X != -1 && b.newPlace.X != -1)
                                    mainArray[b.newPlace.X, b.newPlace.Y].Add(b.item);

                                mainArray[b.place.X, b.place.Y].Remove(b.item);
                            }
                            deleteList.Clear();
                        }
                    }
            }

            previousKeyboard = keyboard;
        }

        public void Draw(SpriteBatch sprite, GameTime time)
        {
            for (int y = player.mainPosition.Y - Configuration.brickOnHeight / 2 < 0 ? 0 : player.mainPosition.Y - Configuration.brickOnHeight / 2;
                y < (player.mainPosition.Y + Configuration.brickOnHeight / 2 > mainArray.GetLength(1) ? mainArray.GetLength(1) : player.mainPosition.Y + Configuration.brickOnHeight / 2); y++)

                for (int x = player.mainPosition.X - Configuration.bricksOnWidth / 2 < 0 ? 0 : player.mainPosition.X - Configuration.bricksOnWidth / 2;
                    x < (player.mainPosition.X + Configuration.bricksOnWidth / 2 > mainArray.GetLength(0) ? mainArray.GetLength(0) : player.mainPosition.X + Configuration.bricksOnWidth / 2); x++)               
                    foreach (IGameObject a in mainArray[x, y])                    
                        a.Draw(sprite, time);

            subMenu.Draw(sprite, time, new Vector2(0, 0));
            levelComplete.Draw(sprite, time, new Vector2(0, 0));
            levelUncomplete.Draw(sprite, time, new Vector2(0, 0));
            informationPanel.Draw(sprite, time, new Vector2(0, 0));
        }        

        #region "Events"

        private void p_DeleteObject(object sender, MoveEventArgs e)
        {
            deleteList.Add(new MoveItem((IGameObject)sender, e.Place, e.NewPlace));
        }

        private void subMenu_GoToMainMenu(object sender, EventArgs e)
        {
            isStoped = true;
            if (GotoMenu != null)
                GotoMenu(this, new EventArgs());
        }

        private void levelComplete_playNext(object sender, EventArgs e)
        {
            if (PlayNextMap != null)
            {

                ExiEventArgs eventArgs = new ExiEventArgs();
                eventArgs.LevelPath = Configuration.useProfile.LastLevel;
                PlayNextMap(this, eventArgs);
            }
        }

        private void levelComplete_exit(object sender, EventArgs e)
        {
            if (this.GotoMenu != null)
                GotoMenu(this, new EventArgs());
        }

        private void levelUncomplete_playAgain(object sender, EventArgs e)
        {
            if (RestartLevel != null)
            {
                ExiEventArgs eventArgs = new ExiEventArgs();
                eventArgs.LevelPath = level;
                RestartLevel(this, eventArgs);
            }
        }

        private void player_OnEat(object sender, EventArgs e)
        {
            score++;
        }

        private void player_OnHurt(object sender, EventArgs e)
        {
            health--;
        }

        private void Game_LevelComplete(object sender, EventArgs e)
        {
            levelCompleteSound.Play();
        }

        private void Game_PlayerDie(object sender, EventArgs e)
        {
            playerDieSound.Play();
        }

        #endregion
    }
}
