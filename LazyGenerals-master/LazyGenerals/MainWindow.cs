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
            string Host = System.Net.Dns.GetHostName();
            string IP = System.Net.Dns.GetHostByName(Host).AddressList[0].ToString();
            Client c = new Client(IP, 5000);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 1);
            g.Show();
            Hide();
        }

        private void buttonConnectGame_Click(object sender, EventArgs e)
        {
            Client c = new Client("192.168.0.102", 5001);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 2);
            g.Show();
            Hide();
        }
    }
}
