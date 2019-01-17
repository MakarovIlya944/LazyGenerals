using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LazyServer;

namespace LazyGeneral
{
	public partial class GameWindow : Form
	{
		private GameGraphics gamedrive = new GameGraphics();
        private Color armyColorDefault, armyColorSelected, armyColorStep;
        private Color enemyArmyColorDefault, enemyArmyColorSelected, enemyArmyColorStep;
        private double maxOneArmy = 50.0, maxAllArmy = 100.0;
        private int w, h;
        private Client client;
        private bool isInitPhase = true;
        private int team;
        private double[] powers, enemyPowers;
        private Point[] armies, enemyArmies;
        private int[] order;
        private int activeArmyNum = -1;
        private int activeLayer = -1;
        private int approvedOrders = 0;
        private Point[][] curSteps = new Point[3][];
        private int curStep = 0;
        private const int armyCount = 5;
        private const int limitArea = 3;
        private int isEnd = 3;

        public GameWindow(Client client, int t)
		{
            armyColorDefault = t == 1 ? Color.DarkRed : Color.ForestGreen;
            armyColorSelected = t == 1 ? Color.Crimson : Color.LimeGreen;
            armyColorStep = t == 1 ? Color.Firebrick : Color.Green;
            enemyArmyColorDefault = t != 1 ? Color.DarkRed : Color.ForestGreen;
            enemyArmyColorSelected = t != 1 ? Color.Crimson : Color.LimeGreen;
            enemyArmyColorStep = t != 1 ? Color.Firebrick : Color.Green;
            for (int i = 0; i < 3; i++)
            {
                curSteps[i] = new Point[armyCount];
                for (int j = 0; j < armyCount; j++)
                    curSteps[i][j] = new Point(-1, -1);
            }
            order = new int[armyCount];
            powers = new double[armyCount];
            enemyPowers = new double[armyCount];
            armies = new Point[armyCount];
            enemyArmies = new Point[armyCount];
            team = t;
			InitializeComponent();
            this.client = client;
            int[,] F;
            int max;
            //w = 10;
            //h = 10;
            //max = 15000;
            //F = new int[10, 10] { { 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1},
            //{ 1,1,1,1,1,1,1,1,1,1}};
            (w,h,max,F) = client.RecieveInitField();
            maxAllArmy = max;
            maxOneArmy = max * 0.75;
            labelAmaxnum.Text = maxAllArmy.ToString();
            labelAmaxonenum.Text = maxOneArmy.ToString();
            labelA1num.Text = Math.Round((1 * maxOneArmy) / 100.0).ToString();
            labelA2num.Text = Math.Round((1 * maxOneArmy) / 100.0).ToString();
            labelA3num.Text = Math.Round((1 * maxOneArmy) / 100.0).ToString();
            labelA4num.Text = Math.Round((1 * maxOneArmy) / 100.0).ToString();
            labelA5num.Text = Math.Round((1 * maxOneArmy) / 100.0).ToString();
            gamedrive.Init(pictureBox1.Width, pictureBox1.Height, w, h, F);
            Text = team == 1 ? "Хост: Красные" : "Клиент: Синие";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs pe)
		{
            int enemyteam = team == 1 ? 2 : 1;
            gamedrive.g = pe.Graphics;
            gamedrive.PaintBattleField();
            //int[] rect = new int[4];
            gamedrive.DrawBase(team);
            gamedrive.DrawBase(enemyteam);
            if (isInitPhase)
            {
                for (int i = 0; i < armyCount; i++)
                {
                    if (powers[i] > 0)
                    {
                        //rect = gamedrive.DrawArmy(armies[i].X, armies[i].Y);
                        //pe.Graphics.DrawString(i.ToString(), new Font("Microsoft Sans Serif", 13), Brushes.Black, new Point(rect[0] + rect[2] / 2, rect[1]));
                        //pe.Graphics.DrawRectangle(new Pen(i != activeArmyNum ? armyColorDefault : armyColorSelected, 4), rect[0], rect[1], rect[2], rect[3]);
                        //gamedrive.DrawArmy(i != activeArmyNum ? armyColorDefault : armyColorSelected, armies[i].X, armies[i].Y, i + 1, powers[i]);
                        gamedrive.DrawArmy(team, armies[i].X, armies[i].Y, i + 1, powers[i], 1);
                        
                    }
                }
                gamedrive.DrawConditionLine(team == 1 ? limitArea : h - limitArea);
            }
            else
            {
                for (int i = 0; i < armyCount; i++)
                {
                    int j = 0;
                    if (powers[i] > 0)
                    {
                        for (; j < armyCount && armies[i] != enemyArmies[j]; j++) ;

                        if (j == armyCount)
                        {
                            gamedrive.DrawArmy(team, curSteps[0][i].X, curSteps[0][i].Y, i + 1, powers[i], 1);
                            if (curSteps[1][i].X != -1)
                            {
                                gamedrive.DrawPath(armyColorStep, curSteps[0][i], curSteps[1][i]);
                                gamedrive.DrawArmy(team, curSteps[1][i].X, curSteps[1][i].Y, i + 1, powers[i], (float)0.5);
                            }
                            if (curSteps[2][i].X != -1)
                            {
                                gamedrive.DrawPath(armyColorStep, curSteps[1][i], curSteps[2][i]);
                                gamedrive.DrawArmy(team, curSteps[2][i].X, curSteps[2][i].Y, i + 1, powers[i], (float)0.5);
                            }
                        }
                        else
                            gamedrive.DrawBattle(armies[i].X, armies[i].Y, powers[i], enemyPowers[j]);
                    }
                    
                    if(enemyPowers[i] > 0 && j == armyCount)
                        gamedrive.DrawArmy(enemyteam, enemyArmies[i].X, enemyArmies[i].Y, i + 1, enemyPowers[i], 1);
                }
            }
		}

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point pos = gamedrive.ClickCell(pictureBox1.PointToClient(MousePosition));
                if (pos.X != -1)
                    if (isInitPhase)
                    {
                        //bool exists = armies.Exists(x => x == pos);
                        bool exists = armies.Any(x => x == pos);
                        if (activeArmyNum != -1 && !exists && (team == 1 ? pos.Y : h - 1 - pos.Y) < limitArea)
                        {
                            if (team == 1)
                            {
                                if (pos != new Point(0, 0))
                                {
                                    armies[activeArmyNum] = pos;
                                    activeArmyNum = -1;
                                }
                            }
                            else
                            {
                                if (pos != new Point(h - 1, h - 1))
                                {
                                    armies[activeArmyNum] = pos;
                                    activeArmyNum = -1;
                                }
                            }

                        }
                        else if (activeArmyNum == -1 && exists)
                        {
                            activeArmyNum = 0;
                            for (; activeArmyNum < armyCount && armies[activeArmyNum] != pos; activeArmyNum++) ;
                        }
                    }
                    else
                    {
                        int layer = -1;
                        if (curSteps[0].Any(x => x == pos))
                            layer = 0;
                        else if (curSteps[1].Any(x => x == pos))
                            layer = 1;
                        else if (curSteps[2].Any(x => x == pos))
                            layer = 2;
                        if (activeArmyNum == -1 && layer != -1)
                        {
                            activeArmyNum = 0;
                            for (; activeArmyNum < armyCount && curSteps[layer][activeArmyNum] != pos; activeArmyNum++) ;
                            activeLayer = layer;
                        }
                        else if (activeArmyNum != -1 && layer == -1 && activeLayer < 2)
                        {
                            if (client.SendXY(activeArmyNum, team, pos.X, pos.Y) && !curSteps[activeLayer].Any(x => x == pos))
                            {
                                if (activeLayer == 0 && !order.Any(x => x == activeArmyNum))
                                {
                                    order[approvedOrders] = activeArmyNum;
                                    approvedOrders++;
                                }
                                activeLayer++;
                                curSteps[activeLayer][activeArmyNum] = pos;
                            }
                        }
                        if (activeLayer == 2)
                        {
                            activeArmyNum = -1;
                            activeLayer = -1;
                        }
                    }
            }
            else
            {
                activeArmyNum = -1;
                activeLayer = -1;
                client.SendXY(-2, team, -2, -2);
            }
            pictureBox1.Invalidate();
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

        private void buttonAction_Click(object sender, EventArgs e)
        {
            int[,] position = new int[armyCount, 2];
            int[,] enemyPosition = new int[armyCount, 2];
            if (isInitPhase)
            {
                isInitPhase = false;
                for (int i = 0; i < armyCount; i++)
                {
                    position[i, 0] = armies[i].X;
                    position[i, 1] = armies[i].Y;
                }
                client.SendInitPlacement(team, powers, position);
                for (int i = 0; i < armyCount; i++)
                    curSteps[0][i] = armies[i];
                
            }
            else
            {
                if (approvedOrders != armyCount)
                    for (int i = 0; i < armyCount; i++)
                        if (!order.Any(x => x == i))
                            order[approvedOrders++] = i;
                for (int i = 0; i < armyCount; i++)
                {
                    if (curSteps[1][i].X == -1)
                        curSteps[1][i] = curSteps[0][i];
                    if (curSteps[2][i].X == -1)
                        curSteps[2][i] = curSteps[1][i];
                }
                /*if (activeArmyNum != -1)
                    client.SendXY(-2, team, -2, -2);*/
                client.SendXY(-1, team, -1, -1);
                client.RecieveIsCorrect();
                client.SendOrder(team, order, curSteps);
                isEnd = client.RecieveEnd();
                curStep++;
                approvedOrders = 0;
            }
            if (isEnd == 3)
            {
                (powers, order, position, enemyPowers, order, enemyPosition) = client.RecievePlacement(team);
                for (int i = 0; i < armyCount; i++)
                {
                    armies[i].X = position[i, 0];
                    armies[i].Y = position[i, 1];
                    enemyArmies[i].X = enemyPosition[i, 0];
                    enemyArmies[i].Y = enemyPosition[i, 1];
                    curSteps[0][i] = armies[i];
                    curSteps[1][i].X = -1;
                    curSteps[2][i].X = -1;
                    order[i] = -1;
                }
            }
            activeArmyNum = -1;
            activeLayer = -1;
            labelCurPhase.Text = "Выдача приказов| #" + curStep.ToString();
            pictureBox1.Invalidate();
        }

        private void buttonAReady_Click(object sender, EventArgs e)
        {
            double curAllArmy = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0);
            if (curAllArmy > maxAllArmy)
            {
                MessageBox.Show("Слишком большие армии!");
                return;
            }

            powers[0] = trackBarA1.Value * maxOneArmy / 100.0;
            powers[1] = trackBarA2.Value * maxOneArmy / 100.0;
            powers[2] = trackBarA3.Value * maxOneArmy / 100.0;
            powers[3] = trackBarA4.Value * maxOneArmy / 100.0;
            powers[4] = trackBarA5.Value * maxOneArmy / 100.0;

            int _h = team == 1 ? 0 : h - 1;
            armies[0] = new Point(1, _h);
            armies[1] = new Point(2, _h);
            armies[2] = new Point(3, _h);
            armies[3] = new Point(4, _h);
            armies[4] = new Point(5, _h);

            //включить другой режим
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            gameInitGroup.Enabled = false;
            gameInitGroup.Visible = false;

            buttonAction.Visible = true;
            buttonAction.Enabled = true;
            labelCurPhase.Text = "Расстановка армий";
            pictureBox1.Invalidate();
            labelCurPhase.BackColor = Color.OliveDrab;
            labelPhase.BackColor = Color.OliveDrab;
        }
    }
}
