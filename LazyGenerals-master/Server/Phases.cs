using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyServer;

namespace LazyGeneral
{
    class Phases
    {
        //=======================Конструктор=======================//
        public Phases(Army[,] a, int startArmy, int[,] newPoints)
        {
            points = newPoints; // не уверен, что работает
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
        private int[,] points;

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

            double[,] powerInfo = new double[maxArmy, 2];
            int[,] numberInfo = new int[maxArmy, 2];
            int[,,] locationInfo = new int[maxArmy, 2, 2];

            // Обрати внимание!
            Sending();

            void Sending()
            {
                for (int i = 0; i < maxArmy; i++)
                    for (int side = 0; side < 2; side++)
                    {
                        powerInfo[i, side] = teams[i, side].Power;
                        numberInfo[i, side] = teams[i, side].Number;
                        for (int j = 0; j < 2; j++)
                            locationInfo[i, j, side] = teams[i, side].Location[j];
                    }

                //server.SendXY(side, powerInfo, numberInfo, locationInfo);
                Server.SendInfo(powerInfo, numberInfo, locationInfo);
            }
        }

        public void Checking()
        {
            int armyNum;
            int sideNum;
            int[] newLoc;
            int[] temp;
            while (!Server.quit1 || !Server.quit2)
            {
                (armyNum, sideNum, newLoc) = Server.GetInfoMoveCheck(); // получаем первую часть хода
                switch (armyNum)
                {
                    case -1:
                        if (sideNum == 0)
                            Server.quit1 = true;
                        else Server.quit2 = true;
                        Server.SendInfo(true, sideNum);
                        break;

                    case -2:
                        Server.SendInfo(true, sideNum);
                        break;

                    default:
                        switch (Check(teams[armyNum, sideNum].Location, newLoc))
                        {
                            case true:
                                //server.SendIsCorrect(true);
                                Server.SendInfo(true, sideNum);
                                temp = newLoc; // запоминаем первую часть хода
                                (armyNum, sideNum, newLoc) = Server.GetInfoMoveCheck(); // получаем вторую часть хода
                                switch (armyNum)
                                {
                                    case -1:
                                        Server.SendInfo(true, sideNum);
                                        if (sideNum == 1)
                                            Server.quit1 = true;
                                        else Server.quit2 = true;
                                        break;

                                    case -2:
                                        Server.SendInfo(true, sideNum);
                                        break;

                                    default:
                                        switch (Check(temp, newLoc))
                                        {
                                            case true:
                                                Server.SendInfo(true, sideNum);
                                                break;

                                            case false:
                                                Server.SendInfo(false, sideNum);
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case false:
                                Server.SendInfo(false, sideNum);
                                break;
                        }
                        break;
                }
            }

            bool Check(int[] curLoc, int[] nextLoc) // Проверяю, движется ли армия только на одну клетку и что эта клетка не вода
            {
                if (Math.Abs(curLoc[0] + curLoc[1] - (nextLoc[0] + nextLoc[1])) == 1 && points[nextLoc[0], nextLoc[1]] == 1)
                    return true; // можно
                return false; // нельзя
            }
        }

        public void Orders() // алерт, много говнокода
        {
            int sideNum;

            Task.Run(() => Checking());
            Task.Run(() => Checking());
            while (!Server.quit1 || !Server.quit2) ;
            Server.quit1 = false;
            Server.quit2 = false;
            Server.SendInfo(true, 0);
            Server.SendInfo(true, 1);
            int[,] armys = new int[maxArmy * 2, 3]; // armynum1, x1, y1    armynum1, x2, y2
            // Дважды получаю список приказов, от каждого клиента по одному
            for (int k = 0; k < 2; k++)
            {
                (sideNum, armys) = Server.GetInfoOrders();
                for (int i = 0; i < armys.GetLength(0); i += 2)
                    AddArmy(sideNum, armys[i, 0], armys[i, 1], armys[i, 2], armys[i + 1, 1], armys[i + 1, 2]);
            }

            void AddArmy(int side, int curArmy, int newLoc1x, int newLoc1y, int newLoc2x, int newLoc2y) // добавляем приказ в очередь
            {
                MoveOrder cur = new MoveOrder();
                cur.step = new int[2, 2];
                cur.currArm = teams[curArmy, side];
                cur.step[0, 0] = newLoc1x;
                cur.step[0, 1] = newLoc1y;
                cur.step[1, 0] = newLoc2x;
                cur.step[1, 1] = newLoc2y;
                commands.Add(cur);
            }
        }

        public void Move()
        {
            MovePart(0);
            commands.Reverse();
            MovePart(1);
            commands.Clear();

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