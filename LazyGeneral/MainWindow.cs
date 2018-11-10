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

		private void buttonStartGame_Click(object sender, EventArgs e)
		{
			Form _form2 = new GameWindow();
			_form2.Show();
			//Application.OpenForms[0].Close();
		}
	}
}
