using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyGeneral
{
    class Phases
    {
        //=======================Конструктор=======================//
        public Phases(Army[,] a, int maxArmy)
        {
            teams = a;
            battlesCount = 0;
            armyCount[0] = teams.GetLength(0);
            armyCount[1] = armyCount[0];
            battles = new Battle[maxArmy];
        }

        //=======================Необходимые параметры=======================//
        private Army[,] teams;
        private int[] armyCount;

        private Battle[] battles;
        private int battlesCount;

        //=======================Свойства=======================//
        public Army[,] Teams
        {
            get { return teams; }
        }
        public int[] ArmyCount
        {
            get { return armyCount; }
        }

        public Battle[] Battles
        {
            get { return battles; }
        }
        public int BattlesCount
        {
            get { return battlesCount; }
        }

        //=======================Методы=======================//
        public void Scouting()
        {

        }

        public void Orders()
        {

        }

        public void Move()
        {

        }

        public void Attack()
        {
            for (int i = 0; i < battlesCount; i++)
                battles[i].Fight();
        }

        public void Losses()
        {
            for (int i = 0; i < battlesCount; i++)
                if (battles[i].Forces[0].Power <= 0 || battles[i].Forces[1].Power <= 0)
                {
                    KillLoser(i, 0);
                    KillLoser(i, 1);
                }

            void KillLoser(int duelNum, int side)
            {
                if (battles[duelNum].Forces[side].Power <= 0)
                {
                    battles[duelNum].Forces[side].Power = 0;
                    armyCount[side]--;
                }
                else
                    battles[duelNum].Forces[side].InFight = false;
            }
        }

        //=======================Вспомогательные структуры=======================//
        struct MoveOrder
        {
            int armyNumber;
            int[,] step; // Строки - шаги, столбцы - оси
        }
    }
}
