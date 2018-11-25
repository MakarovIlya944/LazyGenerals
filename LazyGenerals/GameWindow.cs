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
	public partial class GameWindow : Form
	{
		GameGraphics gamedrive = new GameGraphics();
		bool isLight = false;

		public GameWindow()
		{
			InitializeComponent();
			gamedrive.Init(this.pictureBox1.Width, this.pictureBox1.Height);
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			gamedrive.g = pe.Graphics;
			gamedrive.PaintBattleField();
			gamedrive.DrawArmy(0, 2, 3);
			gamedrive.DrawArmy(1, 4, 4);
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			isLight = true;
			Point pos = gamedrive.ClickCell(pictureBox1.PointToClient(MousePosition));
			//MessageBox.Show($"X:{pos.X} Y:{pos.Y}");
		}

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
