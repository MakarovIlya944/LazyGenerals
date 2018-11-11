using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGeneral
{
    class Tile
    {
        //=======================Конструктор=======================//
        public Tile(int option)
        {
            switch (option)
            {
                case (int)types.field:
                    type = types.field;
                    break;

                case (int)types.water:
                    type = types.field;
                    break;
            }
        }

        //=======================Необходимые параметры=======================//
        public enum types { water, field };
        private types type = types.field;

        //=======================Свойства=======================//
        public types Type
        {
            get { return type; }
        }
    }

    class Battleground
    {
        //=======================Конструктор=======================//
        public Battleground(string option)
        {
            switch (option)
            {
                case "100%":
                    for (int i = 0; i < sizeX; i++)
                        for (int j = 0; j < sizeY; j++)
                            tile[i, j] = new Tile(1);
                    break;

                case "70%":
                    Random rand = new Random();
                    for (int i = 0; i < sizeX; i++)
                        for (int j = 0; j < sizeY; j++)
                        {
                            int res = rand.Next(1, 11);
                            if (res < 30)
                                tile[i, j] = new Tile(0);
                            else tile[i, j] = new Tile(1);
                        }
                    break;
            }
        }
        //=======================Необходимые параметры=======================//
        private const int sizeX = 2;
        private const int sizeY = 2;
        public Tile[,] tile = new Tile[sizeX, sizeY];

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
