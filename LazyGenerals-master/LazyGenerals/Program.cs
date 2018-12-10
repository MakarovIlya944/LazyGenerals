using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyGeneral
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static void Logic()
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
