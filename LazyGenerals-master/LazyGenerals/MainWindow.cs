using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyServer;
using System.Net;

namespace LazyGeneral
{
	public partial class MainWindow : Form
	{
        public MainWindow()
		{
			InitializeComponent();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            string Host = Dns.GetHostName();
            string IP = Dns.GetHostByName(Host).AddressList[0].ToString();
            Client c = new Client(IP, 5001);
            //Client c = new Client("25.22.159.103", 5000);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 1);
            g.Show();
            Hide();
        }

        private void buttonConnectGame_Click(object sender, EventArgs e)
        {
            //string Host = Dns.GetHostName();
            //string IP = Dns.GetHostByName(Host).AddressList[0].ToString();
            //Client c = new Client(IP, 5001);
            label1.Visible = true;
            textBox1.Visible = true;
            panel1.Visible = true;
            button1.Visible = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client c = new Client(textBox1.Text, 5001);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 2);
            g.Show();
            Hide();
        }
    }
}
