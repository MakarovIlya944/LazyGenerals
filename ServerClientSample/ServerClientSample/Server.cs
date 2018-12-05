using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerClientSample
{
    class Server
    {
        private readonly int portNo;
        private readonly string serverIP;
        private TcpListener listener;
        private TcpClient client;
        private NetworkStream nwStream;

        public Server(string serverIP, int portNo)
        {
            this.portNo = portNo;
            this.serverIP = serverIP;

            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(serverIP);
            listener = new TcpListener(localAdd, portNo);
            //Console.WriteLine("Listening...");
            listener.Start();

            //---incoming client connected---
            client = listener.AcceptTcpClient();

            //---get the incoming data through a network stream---
            nwStream = client.GetStream();
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
            {  power[i] = double.Parse(tokens[shift_index + i]); }
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

        static void Main(string[] args)
        {
            //Server s_1 = new Server("127.0.0.1", 5000);
            //Server s_2 = new Server("127.0.0.1", 5001);
            //Console.WriteLine(s_1._RecieveData() +
            //    " by s_1");
            //Console.WriteLine(s_2._RecieveData() +
            //    " via s_2");
            //s_1._SendData("Returned from s_1");
            //s_2._SendData("Backed from s_2");

            //Server s_init = new Server("127.0.0.1", 5000);
            //s_init.SendInitField(100, new int[,]
            //    { { 1, 2, 3 }, { 4, 5, 6 },  });

            Server s_place = new Server("127.0.0.1", 5000);
            int maxArmy;
            int[,] army;
            double[] power;
            (maxArmy, army, power) = s_place.RecievePlacement();
            Console.WriteLine(maxArmy);
            int army_rowLength = army.GetLength(0);
            int army_colLength = army.GetLength(1);
            for (int i = 0; i < army_rowLength; i++)
            {
                for (int j = 0; j < army_colLength; j++)
                {
                    Console.Write(string.Format("{0} ", army[i, j]));
                }
                Console.Write(Environment.NewLine);
            }
            Console.WriteLine();
            int power_length = power.GetLength(0);
            for (int i = 0; i < power_length; i++)
            {
                Console.Write(string.Format("{0} ", power[i]));
            }
        }
    }
}
