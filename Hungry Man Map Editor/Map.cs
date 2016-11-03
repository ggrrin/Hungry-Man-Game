using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hungry_Man_Map_Editor
{
    [Serializable]
    
    public class Map
    {
        public List<Objects>[,] mainArray;

        public string path; 

        public Map(int width, int height)
        {
            mainArray = new List<Objects>[width, height];
            for (int y = 0; y < mainArray.GetLength(0); y++)
                for (int x = 0; x < mainArray.GetLength(1); x++)
                    mainArray[y, x] = new List<Objects>();
           /* mainArray[0, 0].Add(Objects.Wall);
            mainArray[1, 0].Add(Objects.Wall);
            mainArray[2, 0].Add(Objects.Wall);
            mainArray[3, 0].Add(Objects.Wall);
            mainArray[4, 0].Add(Objects.Wall);
            mainArray[5, 0].Add(Objects.Wall);
            mainArray[6, 0].Add(Objects.Wall);
            mainArray[7, 0].Add(Objects.Wall);
            mainArray[9, 0].Add(Objects.Wall);
            mainArray[8, 0].Add(Objects.Wall);
            mainArray[10, 0].Add(Objects.Wall);
            mainArray[11, 0].Add(Objects.Wall);
            mainArray[12, 0].Add(Objects.Wall);
            mainArray[13, 0].Add(Objects.Wall);
            mainArray[14, 0].Add(Objects.Wall);
            mainArray[15, 0].Add(Objects.Wall);
            mainArray[15, 1].Add(Objects.Wall);
            mainArray[0, 1].Add(Objects.Wall);
            mainArray[0, 2].Add(Objects.Wall);
            mainArray[0, 3].Add(Objects.Wall);
            mainArray[15, 15].Add(Objects.Wall);
            mainArray[14, 15].Add(Objects.Wall);
            mainArray[13, 15].Add(Objects.Wall);
            mainArray[12, 11].Add(Objects.Wall);
            mainArray[15, 12].Add(Objects.Wall);
            mainArray[15, 13].Add(Objects.Wall);
            mainArray[15, 14].Add(Objects.Wall);
            mainArray[15, 15].Add(Objects.Wall);*/
        }
    }
}
