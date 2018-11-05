using System;

namespace LazyGeneral
{
    class Program
    {
        static void Main(string[] args)
        {
            Battlefield BF = new Battlefield("100%");
            Army[] T1 = new Army[2];
            T1[0] = new Army(1, 1500);
            T1[1] = new Army(1, 5000);
            Army[] T2 = new Army[2];
            T2[0] = new Army(2, 1500);
            T2[1] = new Army(2, 500);
            Stages A = new Stages(T1, T2);
            while (A.EndOfTheGame() == 0)
            {
                A.Move();
                A.Attack();
                A.Death();
            }
            int res = A.EndOfTheGame();
            switch (res)
            {
                case 1:
                    Console.WriteLine("First team won");
                    break;
                case 2:
                    Console.WriteLine("Second team won");
                    break;
            }
            Console.ReadLine();
        }
    }
}
