using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Hungry_Man
{
    [Serializable]
    public class GraphicConnfiguration
    {
        private Resolution[] resolutios;
        private uint selectResolution;

        public Resolution[] Resolution
        {
            get { return resolutios; }
        }

        public uint SelectResolution
        {
            get { return selectResolution; }
        }

        public bool IsFullScreen
        { get; set; }

        public GraphicConnfiguration()
        {
            IsFullScreen = false;
            resolutios = new Resolution[18];
            resolutios[0] = new Resolution(640, 480);
            resolutios[1] = new Resolution(720, 480);
            resolutios[2] = new Resolution(720, 576);
            resolutios[3] = new Resolution(800, 600);
            resolutios[4] = new Resolution(844, 480);
            resolutios[5] = new Resolution(1024, 768);
            resolutios[6] = new Resolution(1152, 720);
            resolutios[7] = new Resolution(1152, 864);
            resolutios[8] = new Resolution(1280, 720);
            resolutios[9] = new Resolution(1280, 768);
            resolutios[10] = new Resolution(1280, 800);
            resolutios[11] = new Resolution(1280, 960);
            resolutios[12] = new Resolution(1280, 1024);
            resolutios[13] = new Resolution(1360, 768);
            resolutios[14] = new Resolution(1360, 1024);
            resolutios[15] = new Resolution(1600, 900);
            resolutios[16] = new Resolution(1680, 1050);
            resolutios[17] = new Resolution(1920, 1080);

            selectResolution = 0;
        }

        public Resolution GetResolution()
        {
            return resolutios[selectResolution];
        }

        public void SetResolution(uint index)
        {
            if (index < resolutios.Length)
                selectResolution = index;
            else
                throw new Exception("You set index, which doesn't implement resolution. ");
        }

        public void NextResolution()
        {
            if (selectResolution < resolutios.Length - 1)
            {
                selectResolution++;
            }
        }

        public void PreviousResolution()
        {
            if (selectResolution > 0)
            {
                selectResolution--;
            }
        }

        public void SaveResolution()
        {
            using (Stream lStream = new FileStream("Content\\Configuration\\GraphicConnfiguration.hmc", FileMode.Create))
            {
                IFormatter lFormatter = new BinaryFormatter();
                lFormatter.Serialize(lStream, this);
            }  
        }

    }

    [Serializable]
    public struct Resolution
    {
        private int width;
        private int height;

        public int Width
        { get { return width; } set { width = value; } }
        public int Height
        { get { return height; } set { height = value; } }

        public Resolution(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return string.Format("{0} x {1}", width, height);
        }
    }
}
