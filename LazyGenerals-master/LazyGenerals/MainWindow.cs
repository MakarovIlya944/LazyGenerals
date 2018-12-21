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
        
		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			System.Drawing.Pen myPen;
			myPen = new System.Drawing.Pen(System.Drawing.Color.Tomato);
			g.DrawEllipse(myPen, 30, 150, 20, 50);
		}

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            //create server instance
            Server server = new Server("127.0.0.1", 5000);
            //create game window
            GameWindow g = new GameWindow(server);
            //create core logic
            //send init field to client
            //recieve client start placement
            //while not end
            //  make armies moves
            //  recieve client armies moves
            //  calc battles core logic
            //  send to client field state

            g.Show();
            Hide();
        }

        private void buttonConnectGame_Click(object sender, EventArgs e)
        {
            //create game window
            GameWindow g = new GameWindow(new Client("127.0.0.1", 5000));
            //recieve init field from server
            //send to server start placement
            //while true
            //  recieve field state
            //  if is end: return 
            //  make armies moves
            //  send to server armies moves

            g.Show();
            Hide();
        }

        private void groupBoxConnection_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
