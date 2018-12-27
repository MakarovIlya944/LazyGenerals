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
            if (isInitPhase)
            {
                if (pos.X != -1)
                {
                    bool exists = armies.Exists(x => x == pos);
                    if (activeArmyNum != -1 && !exists)
                    {
                        armies[activeArmyNum] = pos;
                        activeArmyNum = -1;
                    }
                    else if(activeArmyNum == -1 && exists)
                        activeArmyNum = armies.FindIndex(x => x == pos);
                }
            }
            else
            {
                if (pos.X != -1)
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
                        if (activeLayer < 1)
                           { activeLayer++;
			    curLevels[activeArmyNum]++;}
                        curSteps[activeLayer][activeArmyNum] = pos;
                        
                        activeArmyNum = -1;
                        activeLayer = -1;
                    }
                    else if (activeArmyNum == -1 && layer != -1)
                    {
                        activeArmyNum = curSteps[layer].Where(x => x == pos)[0];
                        //if (Math.Abs(curSteps[layer][activeArmyNum].X - pos.X) < 2 ^ Math.Abs(curSteps[layer][activeArmyNum].Y - pos.Y) < 2)
                          //  if (SendStep())
			  activeLayer =layer;
			    
                    }
                    
                }
            }


            //if click select
            //если нажато на армию, чтобы ее выбрать(а не походить)
            //запоминаем какая армия выбрана
            //return
            isLight = true;
			Point pos = gamedrive.ClickCell(pictureBox1.PointToClient(MousePosition));
            //MessageBox.Show($"X:{pos.X} Y:{pos.Y}");

            //if first palcement not ended
            //если расстановка армий не завершена 
            //{
            //accumulate placement
            //накопить (запомнить) стартовое расположние армий
            //}
            //else
            //{
            //if i here first time now
            //когда расстановка завершена отправляем данные
            //{
            //if (isServer)
            //{
            //    //recieve client start placement
                  // если сервер, то получаем данные
            //    server.RecievePlacement();
            //}
            //else
            //{
            //    //send to server start placement
                  // если клиент, то отправляем
            //    client.SendPlacement();
            //}
            //}
            if (isServer)
            {
                if (isEnd) return;
                //if need server move
                // если сервер не отходил все свои ходы 
                //{
                    //move army
                    // происходит циклическая стадия
                    // запоминаем ходы(какая армия куда походила)  
                    // в stages должно быть учтено какая армия куда ходит
                    //return;
                //}
                //if all moves done
                // если сервер отходил 
                //{
                    //while need get client move
                    //{
                        //recieve client armies moves
                        // сервер получает все ходы клиента
                        server.RecieveArmy();
                //}
                //}

                //calc battles core logic
                // обработка полного хода
                //stages.CyclicStage();
                isEnd = stages.EndStage() == 3;
                if (!isEnd)
                {
                    //send to client NOT_END
                    server.SendIsCorrect(false);
                    //send to client field state
                    server.SendPlacement();
                }
                else
                {
                    //send to client END
                    server.SendIsCorrect(true);
                    //send to client WINNER
                }
            }
            else // код клиента
            {
                if (isEnd) return;
                //if NOT all moves done
                //{
                    //send to server army move
                    client.SendArmy(/*?*/, new int[]
                    { pos.X, pos.Y });
                //}
                //else
                //{
                    //recieve from server IS_END
                    isEnd = client.RecieveIsCorrect();
                    if (!isEnd)
                    {
                        //recieve from server field state
                        client.RecievePlacement();
                    }
                    else
                    {
                        //recieve from server WINNER
                        //display WINNER
                    }
            }
            //}
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
                    curSteps[i, 0] = armies[i];
            }
            else
            {

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
