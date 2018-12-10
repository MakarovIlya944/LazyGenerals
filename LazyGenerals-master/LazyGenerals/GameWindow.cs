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
        private readonly bool isServer;
        private Server server;
        private Client client;
        private Stages stages;
        private bool isEnd;

        public GameWindow(Server server)
        {
            InitializeComponent();
            this.server = server;
            this.client = null;
            this.isServer = true;
            this.isEnd = false;
            gamedrive.Init(this.pictureBox1.Width, this.pictureBox1.Height);

            //create core logic
            //нужно чтобы стагес в конструкторе принимал Server и Client
            stages = new Stages();
            stages.StartStage();
            //send init field to client
            server.SendInitField();
        }

        public GameWindow(Client client)
		{
			InitializeComponent();
            this.server = null;
            this.client = client;
            this.isServer = false;
            this.isEnd = false;
            gamedrive.Init(this.pictureBox1.Width, this.pictureBox1.Height);

            stages = null;
            //recieve init field from server
            client.RecieveInitField();
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

        private void gameInitGroup_Enter(object sender, EventArgs e)
        {

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
        }
    }
}
