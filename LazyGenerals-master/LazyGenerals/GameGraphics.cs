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
        //выделение ограничение расстановки
        private Pen conditionPen;
		public Graphics g { set; get; }
		

		public bool Init(int _w, int _h, int _wNum, int _hNum, int[,] fieled)
		{
			if(_w < 0 || _h < 0)
				return false;
			widthWindow = _w;
			heightWindow = _h;
            width = _wNum;
            height = _hNum;
            
			dx = (widthWindow - netBorder * 2) / width;
			dy = (heightWindow - netBorder * 2) / height;

			gameFiled = new cellType[width][];
			for (int i = 0; i < width; i++)
				gameFiled[i] = new cellType[height];

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    gameFiled[i][j] = (cellType)fieled[i,j];


            conditionPen = new Pen(Color.Yellow, 4);
            conditionPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
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
				return new Point((_x - netBorder) / dx, (_y - netBorder) / dy);
			}
			else
				return new Point(-1, -1);
		}

		public bool DrawArmy(Color c, int x, int y, int num, double power)
		{
			if (x < 0 || x > width || y < 0 || y > height)
				return false;

			int armyReduce = 5;
			int _x = cellBorder + netBorder + dx * x + armyReduce;
			int _y = cellBorder + netBorder + dy * y + armyReduce;
			int _dx = dx - cellBorder * 2 - armyReduce * 2;
			int _dy = dy - cellBorder * 2 - armyReduce * 2;
            
			g.DrawRectangle(new Pen(c,4), _x, _y, _dx, _dy);
            g.DrawString(Math.Round(power,0).ToString(), new Font("Microsoft Sans Serif", 12), Brushes.Black, new Point(_x + 15, _y + 5));
            g.DrawString(num.ToString(), new Font("Microsoft Sans Serif", 8), Brushes.Black, new Point(_x + 2, _y + _dy - 15));
            return true;
		}

        public void DrawPath(Color c, Point b, Point e)
        {
            int armyReduce = 5;
            b.X = cellBorder + netBorder + dx * b.X + armyReduce + (dx - cellBorder * 2 - armyReduce * 2) / 2;
            b.Y = cellBorder + netBorder + dy * b.Y + armyReduce + (dy - cellBorder * 2 - armyReduce * 2) / 2;
            e.X = cellBorder + netBorder + dx * e.X + armyReduce + (dx - cellBorder * 2 - armyReduce * 2) / 2;
            e.Y = cellBorder + netBorder + dy * e.Y + armyReduce + (dy - cellBorder * 2 - armyReduce * 2) / 2;

            g.DrawLine(new Pen(c, 3), b, e);
        }

        public void DrawConditionLine(int row)
        {
            int _x = cellBorder + netBorder;
            int _y = cellBorder + netBorder + dy * row;
            int _dx = cellBorder + netBorder + dx * width;
            g.DrawLine(conditionPen, new Point(_x, _y), new Point(_dx, _y));
        }

        public int[] DrawArmy(int x, int y)
        {
            if (x < 0 || x > width || y < 0 || y > height)
                return null;

            int armyReduce = 5;
            int _x = cellBorder + netBorder + dx * x + armyReduce;
            int _y = cellBorder + netBorder + dy * y + armyReduce;
            int _dx = dx - cellBorder * 2 - armyReduce * 2;
            int _dy = dy - cellBorder * 2 - armyReduce * 2;
            return new int[4] { _x, _y, _dx, _dy };
        }
    }
}
