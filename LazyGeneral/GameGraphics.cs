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
		private enum cellType {grass = 0, water};
		//ширина и высота поля в клетках
		public int width = 9, height = 9;
		//ширина и высота в пикселях
		private int widthWindow, heightWindow;
		//отступ в пикселях от границ окна и границ клетки
		private int netBorder = 15, armyBorder = 3;
		private int dx, dy;
		private cellType[][] BattleField;
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

			dx = (widthWindow - netBorder * 2) / width;
			dy = (heightWindow - netBorder * 2) / height;

			BattleField = new cellType[width][];
			for(int i = 0;i < width;i++)
				BattleField[i] = new cellType[height];

			BattleField[2][4] = cellType.water;
			BattleField[1][3] = cellType.water;
			BattleField[3][4] = cellType.water;
			BattleField[6][8] = cellType.water;
			BattleField[7][5] = cellType.water;
			BattleField[2][7] = cellType.water;

			return true;
		}

		public bool PaintBattleField()
		{
			for (int x = netBorder, ix = 0; ix < width; x += dx, ix++)
				for (int y = netBorder, iy = 0; iy < height; y += dy, iy++)
				{
					g.FillRectangle(BattleField[ix][iy] == cellType.grass ? Brushes.ForestGreen : Brushes.Aqua, x, y, x + dx, y + dy);
					g.DrawRectangle(netPen, x, y, x + dx, y + dy);
				}

			return false;
		}

		public bool DrawArmy(int x, int y)
		{
			if (x < 0 || x > width || y < 0 || y > height)
				return false;

			g.DrawEllipse(armyPen, armyBorder + netBorder + dx * x, armyBorder + netBorder + dy * y, dx - armyBorder * 2, dy - armyBorder * 2);

			return true;
		}
	}
}
