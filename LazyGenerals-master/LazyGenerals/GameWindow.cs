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
        private int activeArmyNum = -1;
        private int activeLayer = -1;
        private Point[][] curSteps = new Point[3][];
        private int[] curLevels = new int[5];

        public GameWindow(Client client, int t)
		{
            for (int i = 0; i < 3; i++)
            { curSteps[i] = new Point[5]; curSteps[i].Initialize(); }
            for (int i = 0; i < 5; i++)
                curLevels[i] = 0;
            team = t;
			InitializeComponent();
            this.client = client;
            isEnd = false;
            client.SendArmy(team, new int[1,1]);
            int[,] F;
            int max;
            (max,F) = client.RecieveInitField();
            maxAllArmy = max;
            maxOneArmy = max * 0.75;
            w = F.GetLength(0);
            h = F.GetLength(1);
            gamedrive.Init(pictureBox1.Width, pictureBox1.Height, F.GetLength(0), F.GetLength(1), F);
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
            if (isInitPhase && pos.X != -1)
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
                if (activeArmyNum != -1 && layer == -1)
                {
                    if ((Math.Abs(curSteps[activeLayer][activeArmyNum].X - pos.X) < 2 ^ Math.Abs(curSteps[activeLayer][activeArmyNum].Y - pos.Y) < 2) && SendStep())
                    {
                        if (activeLayer < 1)
                        {
                            activeLayer++;
                            curLevels[activeArmyNum]++;
                        }
                        curSteps[activeLayer][activeArmyNum] = pos;

                        activeArmyNum = -1;
                        activeLayer = -1;
                    }
                }
                else if (activeArmyNum == -1 && layer != -1)
                {
                    activeArmyNum = curSteps[layer].ToList().FindIndex(x => x == pos);
                    activeLayer = layer;
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
            if (isInitPhase)
            {
                isInitPhase = false;
                client.SendInitPlacement();
                for(int i=0;i<5;i++)
                    curSteps[i][0] = armies[i];
            }
            else
            {
                client.SendOrder();
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
