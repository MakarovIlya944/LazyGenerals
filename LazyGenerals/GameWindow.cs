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
        double maxOneArmy = 50.0, maxAllArmy = 100.0;

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

        private void trackBarA1_Scroll(object sender, EventArgs e)
        {
            labelA1num.Text = Math.Round((trackBarA1.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value)* maxOneArmy/100.0).ToString();
        }

        private void trackBarA2_Scroll(object sender, EventArgs e)
        {
            labelA2num.Text = Math.Round((trackBarA2.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
        }

        private void trackBarA3_Scroll(object sender, EventArgs e)
        {
            labelA3num.Text = Math.Round((trackBarA3.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
        }

        private void trackBarA4_Scroll(object sender, EventArgs e)
        {
            labelA4num.Text = Math.Round((trackBarA4.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
        }

        private void trackBarA5_Scroll(object sender, EventArgs e)
        {
            labelA5num.Text = Math.Round((trackBarA5.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
        }

        private void buttonAReady_Click(object sender, EventArgs e)
        {
            double curAllArmy = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0);
            if (curAllArmy > maxAllArmy)
                MessageBox.Show("Слишком большие армии!");
            else
            {
                //включить другой режим

            }
        }
    }
}
