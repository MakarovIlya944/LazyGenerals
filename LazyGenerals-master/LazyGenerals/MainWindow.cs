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
        IpConfig i;


        public MainWindow()
		{
			InitializeComponent();
            i = new IpConfig();
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            //string Host = Dns.GetHostName();
            //string IP = Dns.GetHostByName(Host).AddressList[0].ToString();
            //Client c = new Client(IP, 5001);
            /*if(!i.Visible)
                i.Show();
            string q;
            i.GetIp(out q);*/

            Client c = new Client("192.168.1.7", 5000);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 1);
            g.Show();
            Hide();
        }

        public void Connect(string ip)
        {

        }


        private void buttonConnectGame_Click(object sender, EventArgs e)
        {
            //string Host = Dns.GetHostName();
            //string IP = Dns.GetHostByName(Host).AddressList[0].ToString();
            //Client c = new Client(IP, 5001);
            Client c = new Client("192.168.1.7", 5001);
            c.RecieveIsCorrect();
            GameWindow g = new GameWindow(c, 2);
            g.Show();
            Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
