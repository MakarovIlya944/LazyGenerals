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

                case "85%":
                    Random rand = new Random();
                    for (int i = 0; i < sizeX; i++)
                        for (int j = 0; j < sizeY; j++)
                        {
                            double res = rand.NextDouble();
                            if (res <= 0.15)
                                tile[i, j] = types.water;
                            else tile[i, j] = types.field;
                        }
                    for (int i = 0; i < 2; i++)
                        for (int j = 0; j < 2; j++)
                        {
                            tile[i, j] = types.field;
                            tile[sizeX - i - 1, sizeY - j - 1] = types.field;
                        }
                    //костылики
                    tile[1, 0] = types.field;
                    tile[2, 0] = types.field;
                    tile[3, 0] = types.field;
                    tile[4, 0] = types.field;
                    tile[5, 0] = types.field;

                    tile[1, sizeY - 1] = types.field;
                    tile[2, sizeY - 1] = types.field;
                    tile[3, sizeY - 1] = types.field;
                    tile[4, sizeY - 1] = types.field;
                    tile[5, sizeY - 1] = types.field;
                    break;
            }
        }
        //=======================Необходимые параметры=======================//
        private const int sizeX = 10;
        private const int sizeY = 10;
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