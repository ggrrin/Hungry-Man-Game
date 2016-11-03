using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Hungry_Man
{
    static class Configuration
    {
        public static GraphicConnfiguration settingG;
        public static float velocity;
        public static Profile useProfile;
        //it is percent size brick from monitor
        private const float percent = 0.037f;
        //it is real size of graphic brick
        private const float heightImage = 80.0f;
        //it is height of monitor
        public static int height;
        //number of brick 
        public static int bricksOnWidth;
        //number of brick
        public static int brickOnHeight;
        //it is width of monitor
        public static int width;
        //it is size of side of brick
        public static int side;
        //it is percent size of image
        public static float scale;
        public static float menuScale;
        //it is center of image
        public static Vector2 origin;
        //it is position map in monitor
        public static Vector2 margin;
        //for get data
        public static ContentManager content;
        public static GraphicsDevice graphicDivice;
        public static GraphicsDeviceManager graphic;

        public static void Initialize(GraphicsDeviceManager graphics, IServiceProvider Service)
        {
            graphic = graphics;
            graphicDivice = graphics.GraphicsDevice;

            if (Directory.GetFiles("Content\\Profiles").Length == 0)
            {
                useProfile = new Profile();
                SaveProfile();
            }
            else
            {
                using (Stream lStream = new FileStream("Content\\Profiles\\profile.hmp", FileMode.Open))
                {
                    IFormatter lFormatter = new BinaryFormatter();
                    useProfile = (Profile)lFormatter.Deserialize(lStream);
                } 
            }

            if (Directory.GetFiles("Content\\Configuration").Length == 0)
            {
                settingG = new GraphicConnfiguration();
                settingG.SaveResolution();
            }
            else
            {
                using (Stream lStream = new FileStream("Content\\Configuration\\GraphicConnfiguration.hmc", FileMode.Open))
                {
                    IFormatter lFormatter = new BinaryFormatter();
                    settingG = (GraphicConnfiguration)lFormatter.Deserialize(lStream);
                }
            }

            graphic.PreferredBackBufferWidth = settingG.GetResolution().Width;
            graphic.PreferredBackBufferHeight = settingG.GetResolution().Height;
            graphic.IsFullScreen = settingG.IsFullScreen;

            content = new ContentManager(Service, "Content");
            height = graphic.PreferredBackBufferHeight;
            width = graphic.PreferredBackBufferWidth;
            side = (int)(width * percent);
            scale = (float)(side / heightImage);
            menuScale = (float)width / 1920f;
            origin =  new Vector2(heightImage / 2, heightImage / 2);
            bricksOnWidth = width / side + 3;
            brickOnHeight = height / side + 3;
            velocity = width / 256;

            graphic.ApplyChanges();
        }

        public static void Initialize()
        {
            graphic.PreferredBackBufferWidth = settingG.GetResolution().Width;
            graphic.PreferredBackBufferHeight = settingG.GetResolution().Height;
            graphic.IsFullScreen = settingG.IsFullScreen;

            height = graphic.PreferredBackBufferHeight;
            width = graphic.PreferredBackBufferWidth;
            side = (int)(width * percent);
            scale = (float)(side / heightImage);
            menuScale = (float)width / 1920f;
            origin = new Vector2(heightImage / 2, heightImage / 2);
            bricksOnWidth = width / side + 3;
            brickOnHeight = height / side + 3;
            velocity = width / 256;

            graphic.ApplyChanges();
        }

        public static void SaveProfile()
        {
            using (Stream lStream = new FileStream("Content\\Profiles\\profile.hmp", FileMode.Create))
            {
                IFormatter lFormatter = new BinaryFormatter();
                lFormatter.Serialize(lStream, useProfile);
            }        
        }
        

    }

    
}
 