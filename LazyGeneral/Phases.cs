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
        public Phases(Army[,] a, int startArmy)
        {
            teams = a;
            battlesCount = 0;
            armyCount = new int[2];
            armyCount[0] = teams.GetLength(0);
            armyCount[1] = armyCount[0];
            battles = new Battle[startArmy];
            for (int i = 0; i < startArmy; i++)
                battles[i] = new Battle();
            maxArmy = startArmy;
            commands = new List<MoveOrder>(maxArmy * 2);
        }

        //=======================Необходимые параметры=======================//
        private Army[,] teams;
        private int[] armyCount;

        private Battle[] battles;
        private int battlesCount;

        private int maxArmy;
        private List<MoveOrder> commands;
        private int[] curCommand = new int[2];
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

        public int MaxArmy
        {
            get { return maxArmy; }
        }
        //=======================Методы=======================//
        public void Scouting()
        {
            Console.WriteLine("Информация для первого игрока:");
            ShowInfo(0);
            Console.WriteLine("Информация для второго игрока:");
            ShowInfo(1);

            void ShowInfo(int side)
            {
                Console.WriteLine("Количество армий в живых:" + armyCount[side]);
                Console.WriteLine("Таблица армий:");
                Console.WriteLine("Номер\t Мощь\t Сражается\t X\t Y\t");
                for (int i = 0; i < maxArmy; i++)
                    Console.WriteLine(teams[i, side].Number + "\t" + teams[i, side].Power + "\t" + teams[i, side].InFight + "\t" + teams[i, side].Location[0] + "\t" + teams[i, side].Location[1]);
            }
        }

        public void Orders()
        {
            //curCommand[0] = 0;
            //curCommand[1] = 0;
            //Console.WriteLine("Фаза приказов первого игрока!");
            //OrderFormation(0);
            //Console.WriteLine("Фаза приказов второго игрока!");
            //OrderFormation(1);

            //void OrderFormation(int side)
            //{
            //    int cur;
            //    bool quit = false;
            //    int num;
            //    string read;
            //    string[] coordsSym = new string[4];
            //    int[] coordsInt = new int[4];
            //    while (!quit)
            //    {
            //        Console.WriteLine("Выберите армию либо закончите ход:");
            //        for (int i = 0; i < armyCount[side]; i++)
            //            if (teams[i, side].Power != 0 && !teams[i, side].InFight)
            //                Console.WriteLine("Номер армии:" + teams[i, side].Number + "\t Мощь армии:" + teams[i, side].Power);
            //        Console.WriteLine("Выход: 123");
            //        read = Console.ReadLine();
            //        num = int.Parse(read);
            //        switch (num)
            //        {
            //            case 123:
            //                quit = true;
            //                break;

            //            default:
            //                cur = 0;
            //                Console.WriteLine("Напишите два приказа армии:");
            //                StringToInt(0);
            //                StringToInt(2);
            //                while (commands[cur, side].armyNumber != num && cur < curCommand[side])
            //                    cur++;
            //                if (cur == curCommand[side])
            //                {
            //                    commands[curCommand[side], side].armyNumber = num;
            //                    for (int i = 0; i < 2; i++)
            //                        for (int j = 0; j < 2; j++)
            //                            commands[curCommand[side], side].step[i, j] = coordsInt[i * 2 + j];
            //                    curCommand[side]++;
            //                }
            //                else
            //                {
            //                    for (int i = cur; i < curCommand[side] - 1; i++)
            //                        commands[i, side] = commands[i + 1, side];
            //                    for (int i = 0; i < 2; i++)
            //                        for (int j = 0; j < 2; j++)
            //                            commands[curCommand[side], side].step[i, j] = coordsInt[i * 2 + j];
            //                }
            //                break;
            //        }
            //    }
            //    void StringToInt(int curt)
            //    {
            //        read = Console.ReadLine();
            //        coordsSym = read.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //        for (int i = curt; i < curt + 2; i++)
            //            coordsInt[i] = int.Parse(coordsSym[i]);
            //    }

            //}
            if (commands.Count == 0)
            {
                MoveOrder cur = new MoveOrder();

                cur.step = new int[2, 2];
                cur.currArm = teams[0, 0];
                cur.step[0, 0] = 1;
                cur.step[0, 1] = 0;
                cur.step[1, 0] = 0;
                cur.step[1, 1] = 0;
                commands.Add(cur);

                cur.step = new int[2, 2];
                cur.currArm = teams[0, 1];
                cur.step[0, 0] = 0;
                cur.step[0, 1] = 1;
                cur.step[1, 0] = 0;
                cur.step[1, 1] = 0;
                commands.Add(cur);

                cur.step = new int[2, 2];
                cur.currArm = teams[1, 0];
                cur.step[0, 0] = 0;
                cur.step[0, 1] = 1;
                cur.step[1, 0] = 0;
                cur.step[1, 1] = 0;
                commands.Add(cur);

                cur.step = new int[2, 2];
                cur.currArm = teams[1, 1];
                cur.step[0, 0] = 1;
                cur.step[0, 1] = 0;
                cur.step[1, 0] = 0;
                cur.step[1, 1] = 0;
                commands.Add(cur);
            }
        }

        public void Move()
        {
            MovePart(0);
            commands.Reverse();
            MovePart(1);
            commands.Reverse();

            void MovePart(int stage)
            {
                foreach (MoveOrder command in commands)
                    if (command.currArm.Power != 0 && !command.currArm.InFight && !FindBattle(command.currArm))
                        for (int i = 0; i < 2; i++)
                            command.currArm.Location[i] = command.step[stage, i];
            }

            bool FindBattle(Army current)
            {
                int opp_team;
                if (current.Team == 0)
                    opp_team = 1;
                else opp_team = 0;

                for (int i = 0; i < maxArmy; i++)
                    if (current.Location[0] == teams[i, opp_team].Location[0] && current.Location[1] == teams[i, opp_team].Location[1] && teams[i, opp_team].Power != 0)
                    {
                        battles[battlesCount].Init(current, teams[i, opp_team]);
                        current.InFight = true;
                        teams[i, opp_team].InFight = true;
                        battlesCount++;
                        return true;
                    }
                return false;
            }
        }

        public void Attack()
        {
            for (int i = 0; i < battlesCount; i++)
                battles[i].Fight();
        }

        public void Losses()
        {
            int c = 0;
            for (int i = 0; i < battlesCount; i++)
                if (battles[i].Forces[0].Power <= 0 || battles[i].Forces[1].Power <= 0)
                {
                    KillLoser(i, 0);
                    KillLoser(i, 1);
                    c++;
                }
            battlesCount -= c;

            void KillLoser(int duelNum, int side)
            {
                if (battles[duelNum].Forces[side].Power <= 0)
                {
                    battles[duelNum].Forces[side].Power = 0;
                    armyCount[battles[duelNum].Forces[side].Team]--;
                }
                teams[battles[duelNum].Forces[side].Number, battles[duelNum].Forces[side].Team].InFight = false;
                //battles[duelNum].Forces[side].InFight = false;
            }
        }

        //=======================Вспомогательные структуры=======================//
        public struct MoveOrder
        {
            public Army currArm;
            public int[,] step; // Строки - шаги, столбцы - оси
        }
    }
}
