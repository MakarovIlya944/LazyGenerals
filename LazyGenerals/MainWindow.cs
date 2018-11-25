using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            GameWindow g = new GameWindow();
            g.Show();
            Hide();
        }
    }
}
