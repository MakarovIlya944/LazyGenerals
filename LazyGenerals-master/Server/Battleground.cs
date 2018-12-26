using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGeneral
{
    public class Battleground
    {
        //=======================Конструктор=======================//
        public Battleground(string option)
        {
            switch (option)
            {
                case "100%":
                    for (int i = 0; i < sizeX; i++)
                        for (int j = 0; j < sizeY; j++)
                            tile[i, j] = types.field;
                    break;

                case "70%":
                    Random rand = new Random();
                    for (int i = 0; i < sizeX; i++)
                        for (int j = 0; j < sizeY; j++)
                        {
                            double res = rand.NextDouble();
                            if (res <= 0.3)
                                tile[i, j] = types.water;
                            else tile[i, j] = types.field;
                        }
                    break;
            }
        }
        //=======================Необходимые параметры=======================//
        private const int sizeX = 2;
        private const int sizeY = 2;
        public enum types { water, field };
        public types[,] tile = new types[sizeX, sizeY];

        //=======================Свойства=======================//
        public int SizeX
        {
            get { return sizeX; }
        }
        public int SizeY
        {
            get { return sizeY; }
        }

    }
}