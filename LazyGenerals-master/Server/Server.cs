using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using LazyGeneral;

namespace LazyServer
{
    public class Server
    {
        private readonly int portNo;
        private readonly string serverIP;
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream nwStream;
        static private Thread serv1,serv2;
        private List<(TcpClient, NetworkStream)> listClients = new List<(TcpClient, NetworkStream)>();
        static public int connections = 0;

        private class MyThread
        {
            Thread thread;

            MyThread(int mode)
            {
                thread = new Thread(this.)
            }
        }


        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 5000);
            Logic();
        }

        static public void Logic()
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

        public Server(string serverIP, int portNo)
        {
            this.portNo = portNo;
            this.serverIP = serverIP;

            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(serverIP);
            listener = new TcpListener(localAdd, portNo);
            listener.Start();

            Console.WriteLine($"Waiting connections on {serverIP}:{portNo} .....");
            //---incoming client connected---
            for (int i = 0; i < 2; i++)
            {
                client = listener.AcceptTcpClient();
                nwStream = client.GetStream();
                listClients.Add((client, nwStream));
                Console.WriteLine("Client connected!");
                _SendData(i.ToString());
                connections++;
            }
            //---get the incoming data through a network stream---
        }

        static public void ac(object y)
        {

        }

        static public void SendInfo(int a,int b, int d, Battleground.types[,] c)
        {
            var rqw = new ParameterizedThreadStart(ac);
            serv1 = new Thread(new ThreadStart(ac));
        }

        static public ValueTuple<int, double[], int[], int[,]> GetInfo()
        {

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

       static public void SendInit(int x, int y, int maxPower, Battleground.types[,] field)
        {
            string data = $"{x} {y} {maxPower}";
            int n = field.GetLength(0);
            int m = field.GetLength(1);
            data += " " + n + " " + m;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                { data += " " + (int)field[i, j]; }
            }
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

        public void SendIsCorrect(bool isCorrect)
        {
            string data = isCorrect.ToString();
            _SendData(data);
        }

        private void _SendData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        ~Server()
        {
            client.Close();
            listener.Stop();
        }
    }
}