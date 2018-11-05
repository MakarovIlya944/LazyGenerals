using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneral
{
    class Tile
    {
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
        public enum types { water, field };
        public types type = types.field;  
    }

    class Battlefield
    {
        public Battlefield(string option)
        {
            switch (option)
            {
                case "100%":
                    for (int i = 0; i < size_x; i++)
                        for (int j = 0; j < size_y; j++)
                            tile[i, j] = new Tile(1);
                    break;

                case "70%":
                    Random rand = new Random();
                    for (int i = 0; i < size_x; i++)
                        for (int j = 0; j < size_y; j++)
                        {
                            int res = rand.Next(1, 11);
                            if (res < 30)
                                tile[i, j] = new Tile(0);
                            else tile[i, j] = new Tile(1);
                        }
                    break;
            }
        }
        public const int size_x = 10;
        public const int size_y = 10;
        Tile[,] tile = new Tile[size_x, size_y];
    }
}
