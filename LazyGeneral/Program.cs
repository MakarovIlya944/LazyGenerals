using System;

namespace LazyGeneral
{
    class Program
    {
        static void Main(string[] args)
        {
            int end = 3;
            Stages start = new Stages();
            start.StartStage();
            while (end == 3)
            {
                start.CyclicStage();
                end = start.EndStage();
            }
            switch (end)
            {
                case 0:
                    Console.WriteLine("Первый победил");
                    break;

                case 1:
                    Console.WriteLine("Второй победил");
                    break;

                case 2:
                    Console.WriteLine("Ничья");
                    break;
            }
            Console.ReadLine();
        }
    }
}
