﻿using System;
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
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{

		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			System.Drawing.Pen myPen;
			myPen = new System.Drawing.Pen(System.Drawing.Color.Tomato);
			g.DrawEllipse(myPen, 30, 150, 20, 50);
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			
		}
	}
}
