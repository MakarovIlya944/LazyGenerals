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
			Pen myPen;
			myPen = new Pen(Color.Tomato);
			g.DrawEllipse(myPen, 30, 150, 20, 50);
		}

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            GameWindow g = new GameWindow(new Client("127.0.0.1", 5000), 1);
            g.Show();
            Hide();
        }

        private void buttonConnectGame_Click(object sender, EventArgs e)
        {
            GameWindow g = new GameWindow(new Client("127.0.0.1", 5000), 2);
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
