﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LazyGeneral
{
	class GameGraphics
	{
		private enum cellType {field, water };
		//игровое поле с типами тайлов
		private cellType[][] gameFiled;
		//размеры поля в ячейках
		public int width = 7, height = 7;
		//размеры поля в пикселях
		private int widthWindow, heightWindow;
		//отступ в пикселях от границы
		private int netBorder = 15, cellBorder = 2;
		//размеры ячейки в пикселях
		private int dx, dy;
		public Graphics g { set; get; }

		private Pen netPen, armyPen;

		public bool Init(int _w, int _h)
		{
			if(_w < 0 || _h < 0)
				return false;
			widthWindow = _w;
			heightWindow = _h;

			dx = (widthWindow - netBorder * 2) / width;
			dy = (heightWindow - netBorder * 2) / height;

			gameFiled = new cellType[width][];
			for (int i = 0; i < width; i++)
				gameFiled[i] = new cellType[height];

			gameFiled[2][4] = cellType.water;
			gameFiled[1][4] = cellType.water;
			gameFiled[3][3] = cellType.water;

			netPen = new Pen(Color.Black);
			netPen.Width = 2;
			armyPen = new Pen(Color.Azure);
			armyPen.Width = 3;
			
			return true;
		}

		public bool PaintBattleField()
		{
			
			g.FillRectangle(Brushes.Black, netBorder, netBorder, widthWindow - netBorder * 2, heightWindow - netBorder * 2);

			for (int x = netBorder, ix = 0; ix < width; x += dx, ix++)
				for (int y = netBorder, iy = 0; iy < height; y += dy, iy++)
					g.FillRectangle(gameFiled[ix][iy] == cellType.field ? Brushes.ForestGreen : Brushes.Aqua, x + cellBorder, y + cellBorder, dx - cellBorder * 2, dy - cellBorder * 2);

			return false;
		}

		public bool ClickCell(Point mouse)
		{
			return true;
		}

		public bool DrawArmy(int x, int y)
		{
			if (x < 0 || x > width || y < 0 || y > height)
				return false;

			int armyReduce = 5;
			int _x = cellBorder + netBorder + dx * x + armyReduce;
			int _y = cellBorder + netBorder + dy * y + armyReduce;
			int _dx = dx - cellBorder * 2 - armyReduce * 2;
			int _dy = dy - cellBorder * 2 - armyReduce * 2;

			g.DrawRectangle(armyPen, _x, _y, _dx, _dy);

			return true;
		}
	}
}