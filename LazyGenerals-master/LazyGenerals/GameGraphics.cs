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
		private enum cellType {field, water };
		//игровое поле с типами тайлов
		private cellType[][] gameFiled;
		//размеры поля в ячейках
		public int width = 8, height = 6;
		//размеры поля в пикселях
		private int widthWindow, heightWindow;
		//отступ в пикселях от границы
		private int netBorder = 15, cellBorder = 2;
		//размеры ячейки в пикселях
		private int dx, dy;
		//последнее выделение клетки
		private Point cellLightning;
		public Graphics g { set; get; }
		

		public bool Init(int _w, int _h, int _wCell, int _hCell, int[,] _field)
		{
			if(_w < 0 || _h < 0)
				return false;
			widthWindow = _w;
			heightWindow = _h;
            width = _wCell;
            height = _hCell;

			dx = (widthWindow - netBorder * 2) / width;
			dy = (heightWindow - netBorder * 2) / height;

			gameFiled = new cellType[width][];
			for (int i = 0; i < width; i++)
				gameFiled[i] = new cellType[height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    gameFiled[i][j] = (cellType)_field[i, j];

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

		private void CellLighting(Point cell)
		{
			cellLightning = cell;
			//ошибка недопустимый аргумент
			//g.DrawRectangle(new Pen(Color.Red, cellBorder), cell.X * dx + cellBorder, cell.Y * dy + cellBorder, dx, dy);
		}

		public Point ClickCell(Point mouse)
		{
			int _x = mouse.X, _y = mouse.Y;
			if (_x > netBorder &&
				_y > netBorder &&
				_x < widthWindow - netBorder &&
				_y < heightWindow - netBorder)
			{
				CellLighting(new Point((_x - netBorder) / dx + 1, (_y - netBorder) / dy + 1));
				return new Point((_x - netBorder) / dx + 1, (_y - netBorder) / dy + 1);
			}
			else
				return new Point(-1, -1);
		}

		public bool DrawArmy(int team, int x, int y)
		{
			if (x < 0 || x > width || y < 0 || y > height)
				return false;

			int armyReduce = 5;
			int _x = cellBorder + netBorder + dx * x + armyReduce;
			int _y = cellBorder + netBorder + dy * y + armyReduce;
			int _dx = dx - cellBorder * 2 - armyReduce * 2;
			int _dy = dy - cellBorder * 2 - armyReduce * 2;

			if(team == 0)
				g.DrawRectangle(new Pen(Color.DarkKhaki,4), _x, _y, _dx, _dy);
			else
				g.DrawRectangle(new Pen(Color.OrangeRed,4), _x, _y, _dx, _dy);

			return true;
		}
	}
}
