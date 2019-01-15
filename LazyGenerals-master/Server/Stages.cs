using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyServer;

namespace LazyGeneral
{
    class Stages
    {
        public void StartStage()
        {
            Battleground field = new Battleground("100%");
            int armyCount = 5;
            // Отправляю поле бля и макс мощь
            int[,] poi = new int[field.SizeX, field.SizeY];
            for (int i = 0; i < field.SizeX; i++)
                for (int j = 0; j < field.SizeY; j++)
                    poi[i, j] = (int)field.tile[i, j];
            Server.SendInfo(field.SizeX, field.SizeY, 15000, field.tile);

            // Обрати внимание!
            Army[,] A = new Army[5, 2];
            int side = new int();
            double[] powerInfo;
            int[,] locationInfo;
            // Получаю команду, силы армий, их номера, их локации
            for (int j = 0; j < 2; j++)
            {
                (side, powerInfo, locationInfo) = Server.GetInfoStart();
                for (int i = 0; i < armyCount; i++)
                    A[i, side] = new Army(powerInfo[i], locationInfo[i, 0], locationInfo[i, 1], side, i); 
            }
            game = new Phases(A, armyCount, poi);
        }

        public void CyclicStage()
        {
            game.Scouting();
            game.Orders();
            game.Move();
            game.Attack();
            game.Losses();
        }

        public int EndStage()
        {
            // Отправляю инфу об окончании игры
            if (game.ArmyCount[0] == 0)
                if (game.ArmyCount[1] == 0)
                {
                    Server.SendInfo(2);
                    return 2;
                }
                else
                {
                    Server.SendInfo(1);
                    return 1;
                }
            else if (game.ArmyCount[1] == 0)
            {
                Server.SendInfo(0);
                return 0;
            }
            else
            {
                Server.SendInfo(3);
                return 3;
            }
        }

        Phases game;
    }
}