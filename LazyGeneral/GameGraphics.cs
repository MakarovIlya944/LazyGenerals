using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LazyGeneral
{
	class GameGraphics
	{
		public int width = 5, height = 6;
		private int widthWindow, heightWindow;
		private int netBorder = 15, armyBorder = 5;
		private int dx, dy;
		public Graphics g { set; get; }


		private System.Drawing.Pen netPen, armyPen;

		public bool Init(int _w, int _h)
		{
			if(_w < 0 || _h < 0)
				return false;
			widthWindow = _w;
			heightWindow = _h;
			netPen = new System.Drawing.Pen(System.Drawing.Color.Black);
			netPen.Width = 2;
			armyPen = new System.Drawing.Pen(System.Drawing.Color.Azure);
			armyPen.Width = 3;
			return true;
		}

		public bool PaintBattleField()
		{

			dx = (widthWindow - netBorder*2) / width;
			dy = (heightWindow - netBorder * 2) / height;
			for (int x = netBorder; x < widthWindow - netBorder; x += dx)
				for (int y = netBorder; y < widthWindow - netBorder; y += dy)
				{
					g.DrawLine(netPen, x, netBorder, x, heightWindow - netBorder);
					g.DrawLine(netPen, netBorder, y, widthWindow - netBorder, y);
				}

			//g.DrawEllipse(netPen, 30, 150, 20, 50);

			return false;
		}

		public bool DrawArmy(int x, int y)
		{
			if (x < 0 || x > width || y < 0 || y > height)
				return false;

			g.DrawEllipse(armyPen, armyBorder + netBorder + dx * x, armyBorder + netBorder + dy * y, dx - armyBorder, dy- armyBorder);

			return true;
		}
	}
}
