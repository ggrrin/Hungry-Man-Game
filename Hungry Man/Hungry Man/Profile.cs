using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Hungry_Man
{
    [Serializable]
    class Profile
    {
        private bool[] levels;
        private int lastLevel;

        public int LastLevel
        {
            get { return lastLevel; }
        }

        public Profile()
        {
            levels = new bool[System.IO.Directory.GetFiles("Content\\Maps").Length];
            lastLevel = 0;
        }
        
        public void CompleteLevel(int level)
        {
            levels[level] = true;

            if (level + 1 < levels.Length)
                lastLevel = level + 1;
        }        
    }
}
