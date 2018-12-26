using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using LazyGeneral;

namespace LazyServer
{
    public class Server
    {
        private readonly int portNo;
        private readonly string serverIP;
        private TcpListener listener1, listener2;
        private TcpClient client1, client2;
        private NetworkStream nwStream1, nwStream2;

        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 5000);
            Logic();
        }

        static void Logic()
        {
            int end = 3;
            Stages start = new Stages();
            start.StartStage();
            while (end == 3)
            {
                start.CyclicStage();
                end = start.EndStage();
            }
            switch (end)
            {
                case 0:
                    Console.WriteLine("Первый победил");
                    break;

                case 1:
                    Console.WriteLine("Второй победил");
                    break;

                case 2:
                    Console.WriteLine("Ничья");
                    break;
            }
            Console.ReadLine();
        }

        static public void SendInfo(int endState)
        {

        }

        static public void SendInfo(int w, int h, int power, Battleground.types[,] field)
        {

        }

        static public ValueTuple<> GetInfo()
        {

        }

        public Server(string serverIP, int portNo)
        {
            this.portNo = portNo;
            this.serverIP = serverIP;

            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(serverIP);
            listener1 = new TcpListener(localAdd, portNo);
            listener2 = new TcpListener(localAdd, portNo);
            //Console.WriteLine("Listening...");
            listener1.Start();
            listener2.Start();

            //---incoming client connected---
            Console.WriteLine("Listening...");
            client1 = listener1.AcceptTcpClient();
            Console.WriteLine("Client connected");

            //---get the incoming data through a network stream---
            nwStream1 = client1.GetStream();

            client2 = listener2.AcceptTcpClient();
            Console.WriteLine("Client connected");

            nwStream2 = client2.GetStream();
        }

        public void SendInitField(int maxArmy, int[,] F)
        {
            string data = maxArmy.ToString();
            int n = F.GetLength(0);
            int m = F.GetLength(1);
            data += " " + n + " " + m;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                { data += " " + F[i, j]; }
            }
            _SendData(data);
        }

        public ValueTuple<int, int[,], double[]> RecievePlacement()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int maxArmy = int.Parse(tokens[0]);
            int n_army = int.Parse(tokens[1]);
            int m_army = int.Parse(tokens[2]);
            int[,] army = new int[n_army, m_army];
            for (int i = 0; i < n_army; i++)
            {
                for (int j = 0; j < m_army; j++)
                {
                    int index = i * m_army + j + 3;
                    army[i, j] = int.Parse(tokens[index]);
                }
            }
            int shift_index = (n_army - 1) * m_army + n_army + 5;
            int n_power = int.Parse(tokens[shift_index - 1]);
            double[] power = new double[n_power];
            for (int i = 0; i < n_power; i++)
            { power[i] = double.Parse(tokens[shift_index + i]); }
            return (maxArmy, army, power);
        }

        public ValueTuple<int, int[], double[], int[,]> RecieveInitPlacement()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int team = int.Parse(tokens[0]);
            int n = int.Parse(tokens[1]);
            int[] order = new int[n];
            int ind = 2;
            for (int i = 0; i < n; i++)
            { order[i] = int.Parse(tokens[ind++]); }

            n = int.Parse(tokens[ind++]);
            double[] power = new double[n];
            for (int i = 0; i < n; i++)
            { power[i] = double.Parse(tokens[ind++]); }

            n = int.Parse(tokens[ind++]);
            int m = int.Parse(tokens[ind++]);
            int[,] army = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int index = i * m + j + 3;
                    army[i, j] = int.Parse(tokens[index]);
                }
            }
            return (team, order, power, army);
        }

        public void SendPlacement(int maxArmy, int[,] army, double[] power)
        {
            string data = maxArmy.ToString();
            int n = army.GetLength(0);
            int m = army.GetLength(1);
            data += " " + n + " " + m;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                { data += " " + army[i, j]; }
            }
            data += " " + power.Length;
            for (int i = 0; i < power.Length; i++)
            { data += " " + power[i].ToString(); }
            _SendData(data);
        }

        private string _RecieveData()
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return data;
        }

        public ValueTuple<int, int, int, int> RecieveXY()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int team = int.Parse(tokens[0]);
            int id = int.Parse(tokens[1]);
            int x = int.Parse(tokens[2]);
            int y = int.Parse(tokens[3]);
            return (team, id, x, y);
        }

        public ValueTuple<int, int[,]> RecieveArmy()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int maxArmy = int.Parse(tokens[0]);
            int n = int.Parse(tokens[1]);
            int m = int.Parse(tokens[2]);
            int[,] army = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int index = i * m + j + 3;
                    army[i, j] = int.Parse(tokens[index]);
                }
            }
            return (maxArmy, army);
        }

        public void SendIsCorrect(bool isCorrect, int client)
        {
            string data = isCorrect.ToString();
            _SendData(data, client);
        }

        private void _SendData(string data, int client)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            if(client == 1)
                nwStream1.Write(buffer, 0, buffer.Length);
            else
                nwStream2.Write(buffer, 0, buffer.Length);
        }

        ~Server()
        {
            client1.Close();
            listener1.Stop();
            client2.Close();
            listener2.Stop();
        }
    }
}