using System;
using System.Collections.Generic;
using System.Text;

namespace LazyGeneral
{
    class Stages
    {
        public Stages(Army[] A1, Army[] A2)
        {
            team1 = A1;
            team2 = A2;
            army1_count = team1.Length;
            army2_count = team2.Length;
        }

        public void Attack()
        {
            for (int i = 0; i < duel_count; i++)
                d[i].Fight();
        }

        public void Move() // это пиздец, дядь
        {
            if (duel_count == 0)
            {
                d[0] = new Duel(team1[0], team2[0]);
                d[1] = new Duel(team1[1], team2[1]);
                for (int i = 0; i < 2; i++)
                {
                    team1[i].fighting = true;
                    team2[i].fighting = true;
                }
                duel_count = 2;
            }
        }

        public void Orders()
        {
            int num;
            cur_order = 0;
            string read;
            Console.WriteLine("First player's turn!");
            for (int i = 0; i < army1_count; i++)
            {
                Console.WriteLine("Choose army:");
                for (int j = 0; j < army1_count; j++)
                    Console.WriteLine(team1[i].number);
                read = Console.ReadLine();
                num = int.Parse(read);
                Order(1, num);
            }

        }

        public void Death()
        {
            for (int i = 0; i < duel_count; i++)
                if (d[i].Force1.Power <= 0 || d[i].Force2.Power <= 0)
                {
                    if (d[i].Force1.Power <= 0)
                    { d[i].Force1.Power = 0; army1_count--; }
                    else
                        d[i].Force1.fighting = false;
                    if (d[i].Force2.Power <= 0)
                    { d[i].Force2.Power = 0; army2_count--; }
                    else
                        d[i].Force2.fighting = false;
                    for (int j = i; j < duel_count - 1; i++)
                        d[j] = d[j + 1];
                    duel_count--;
                    d[duel_count] = null;
                }
        }

        public int EndOfTheGame()
        {
            if (army2_count == 0)
                return 1;
            else if (army1_count == 0)
                return 2;
            else return 0;
        }

        MoveOrders[] ord = new MoveOrders[10];
        private Army[] team1;
        private Army[] team2;
        private Duel[] d = new Duel[5];
        private int duel_count = 0;
        private int army1_count;
        private int army2_count;
        private int cur_order;

        private void Order(int team, int army_num)
        {

            ord[cur_order].StepX = { };
        }


        class MoveOrders
        {
            int army_number;
            private int[] stepX = new int[3];
            private int[] stepY = new int[3];

            public int[] StepX
            {
                get { return stepX; }
                set { stepX[0] = value[0]; stepX[1] = value[1]; stepX[2] = value[2];}
            }

            public int[] StepY
            {
                get { return stepY; }
                set { stepY[0] = value[0]; stepY[1] = value[1]; stepY[2] = value[2]; }
            }
        }
    }
}
