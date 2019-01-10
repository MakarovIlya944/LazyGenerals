using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LazyServer;

namespace LazyGeneral
{
	public partial class GameWindow : Form
	{
		private GameGraphics gamedrive = new GameGraphics();
        private bool isLight = false;
        private double maxOneArmy = 50.0, maxAllArmy = 100.0;
        private int w, h;
        private Client client;
        private bool isEnd, isInitPhase = true;
        private int team;
        private List<Point> armies = new List<Point>();
        private int[] order = new int[5];
        private int activeArmyNum = -1;
        private int activeLayer = -1;
        private int approvedOrders = 0;
        private Point[][] curSteps = new Point[3][];

        public GameWindow(Client client, int t)
		{
            for (int i = 0; i < 3; i++)
            { curSteps[i] = new Point[5]; curSteps[i].Initialize(); }
            team = t;
			InitializeComponent();
            this.client = client;
            isEnd = false;
            client.SendHello(team);
            int[,] F;
            int max;
            (w,h,max,F) = client.RecieveInitField();
            maxAllArmy = max;
            maxOneArmy = max * 0.75;
            gamedrive.Init(pictureBox1.Width, pictureBox1.Height, w, h, F);
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
            Point pos = gamedrive.ClickCell(pictureBox1.PointToClient(MousePosition));
            if(pos.X != -1)
                if (isInitPhase)
                {
                    bool exists = armies.Exists(x => x == pos);
                    if (activeArmyNum != -1 && !exists)
                    {
                        armies[activeArmyNum] = pos;
                        activeArmyNum = -1;
                    }
                    else if (activeArmyNum == -1 && exists)
                        activeArmyNum = armies.FindIndex(x => x == pos);
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
                        for (int activeArmyNum = 0, _n = curSteps.GetLength(layer); activeArmyNum < _n && curSteps[layer][activeArmyNum] != pos; activeArmyNum++);
                        activeLayer = layer;
                    }
                    else if (activeLayer == 2)
                    {
                        activeArmyNum = -1;
                    }
                    else if (activeArmyNum != -1 && layer == -1)
                    {
                        client.SendXY(team, activeArmyNum, pos.X, pos.Y);
                        if (!order.Any(x => x == activeArmyNum) && client.RecieveIsCorrect())
                        {
                            order[approvedOrders] = activeArmyNum;
                            approvedOrders++;
                            curSteps[activeLayer][activeArmyNum] = pos;
                            activeLayer++;
                        }
                    }
                }
        }

        private void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void trackBarA1_Scroll(object sender, EventArgs e)
        {
            labelA1num.Text = Math.Round((trackBarA1.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value)* maxOneArmy/100.0).ToString();
            trackBarA2.Maximum -= trackBarA1.Value;
            trackBarA3.Maximum -= trackBarA1.Value;
            trackBarA4.Maximum -= trackBarA1.Value;
            trackBarA5.Maximum -= trackBarA1.Value;
        }

        private void trackBarA2_Scroll(object sender, EventArgs e)
        {
            labelA2num.Text = Math.Round((trackBarA2.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
            trackBarA1.Maximum -= trackBarA2.Value;
            trackBarA3.Maximum -= trackBarA2.Value;
            trackBarA4.Maximum -= trackBarA2.Value;
            trackBarA5.Maximum -= trackBarA2.Value;
        }

        private void trackBarA3_Scroll(object sender, EventArgs e)
        {
            labelA3num.Text = Math.Round((trackBarA3.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
            trackBarA1.Maximum -= trackBarA3.Value;
            trackBarA2.Maximum -= trackBarA3.Value;
            trackBarA4.Maximum -= trackBarA3.Value;
            trackBarA5.Maximum -= trackBarA3.Value;
        }

        private void trackBarA4_Scroll(object sender, EventArgs e)
        {
            labelA4num.Text = Math.Round((trackBarA4.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
            trackBarA1.Maximum -= trackBarA4.Value;
            trackBarA2.Maximum -= trackBarA4.Value;
            trackBarA3.Maximum -= trackBarA4.Value;
            trackBarA5.Maximum -= trackBarA4.Value;
        }

        private void trackBarA5_Scroll(object sender, EventArgs e)
        {
            labelA5num.Text = Math.Round((trackBarA5.Value * maxOneArmy) / 100.0).ToString();
            labelAcurnum.Text = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0).ToString();
            trackBarA1.Maximum -= trackBarA5.Value;
            trackBarA2.Maximum -= trackBarA5.Value;
            trackBarA3.Maximum -= trackBarA5.Value;
            trackBarA4.Maximum -= trackBarA5.Value;
        }
     
        private void buttonAction_Click(object sender, EventArgs e)
        {
            if (isInitPhase)
            {
                isInitPhase = false;
                int[,] position = new int[5, 2];
                double[] power = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    position[i, 0] = armies[i].X;
                    position[i, 1] = armies[i].Y;
                }
                power[0] = trackBarA1.Value * maxOneArmy / 100.0;
                power[1] = trackBarA2.Value * maxOneArmy / 100.0;
                power[2] = trackBarA3.Value * maxOneArmy / 100.0;
                power[3] = trackBarA4.Value * maxOneArmy / 100.0;
                power[4] = trackBarA5.Value * maxOneArmy / 100.0;
                client.SendInitPlacement(team, power, position);
                for(int i=0;i<5;i++)
                    curSteps[i][0] = armies[i];
            }
            else
            {
                client.SendOrder(team, order, curSteps);
            }
        }

        private void buttonAReady_Click(object sender, EventArgs e)
        {
            double curAllArmy = Math.Round((trackBarA1.Value + trackBarA2.Value + trackBarA3.Value + trackBarA4.Value + trackBarA5.Value) * maxOneArmy / 100.0);
            if (curAllArmy > maxAllArmy)
            {
                MessageBox.Show("Слишком большие армии!");
                return;
            }
            //включить другой режим
            pictureBox1.Visible = true;
            pictureBox1.Enabled = true;
            gameInitGroup.Enabled = false;
            gameInitGroup.Visible = false;

            int _h = team == 1 ? 0 : h - 1;
            armies.Add(new Point(1, _h));
            armies.Add(new Point(2, _h));
            armies.Add(new Point(3, _h));
            armies.Add(new Point(4, _h));
            armies.Add(new Point(5, _h));

            buttonAction.Visible = true;
            buttonAction.Enabled = true;
        }
    }
}
