using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGeneral
{
    class Stages
    {
        public void StartStage()
        {
            Army[,] A = new Army[2, 2];
            A[0, 0] = new Army(500, 1, 1, 0, 0);
            A[0, 1] = new Army(500, 0, 1, 1, 0);
            A[1, 0] = new Army(5000, 0, 0, 0, 1);
            A[1, 1] = new Army(5000, 1, 0, 1, 1);
            game = new Phases(A, 2);
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
            if (game.ArmyCount[0] == 0)
                if (game.ArmyCount[1] == 0)
                    return 2;
                else return 1;
            else if (game.ArmyCount[1] == 0)
                return 0;
            else return 3;
        }

        Phases game;
    }
}
