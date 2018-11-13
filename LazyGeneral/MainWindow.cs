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
			GameWindow gameWindow = new GameWindow();
			gameWindow.Show();
		}

		private void buttonStartGame_Click(object sender, EventArgs e)
		{
			GameWindow gameWindow = new GameWindow();
			gameWindow.Show();
			//this.Hide();
		}
	}
}
