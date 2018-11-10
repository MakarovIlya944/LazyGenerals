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
	public partial class Form1 : Form
	{
		GameGraphics gamedrive = new GameGraphics();

		public Form1()
		{
			InitializeComponent();
			gamedrive.Init(this.pictureBox1.Width,this.pictureBox1.Height);
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			gamedrive.g = pe.Graphics;
			gamedrive.PaintBattleField();
			gamedrive.DrawArmy(2,3);
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}
	}
}
