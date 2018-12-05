using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralsField
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //client net part
        bool SendStepsToServer(List<Tuple<int, int>> steps)
        {
            MessageBox.Show("Data sended");
            return true;
        }

        //client logic
        int count = 0;
        List<Tuple<int, int>> steps = new List<Tuple<int, int>>();
        void ProcessClickCell(int x, int y)
        {
            MessageBox.Show("clicled : " + x + " " + y);
            steps.Add(new Tuple<int, int>(x, y));
            if (++count == 2)
            {
                SendStepsToServer(steps);
            }
        }

        //client graphic
        private void button1_Click(object sender, EventArgs e)
        {
            ProcessClickCell(1, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessClickCell(1, 2);
        }
    }
}
