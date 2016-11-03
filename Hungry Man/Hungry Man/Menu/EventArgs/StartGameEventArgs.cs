using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hungry_Man.Menu
{
    class ExiEventArgs
    {
        private int levelPath;

        #region "Properties"

        public int LevelPath
        {
            get { return levelPath; }
            set { levelPath = value; }
        }

        #endregion

        public ExiEventArgs()
        {

        }
    }
}
