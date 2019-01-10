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
        static private TcpClient client1, client2;
        static private NetworkStream nwStream1, nwStream2;
        static private bool getMessage = false;
        static string recieveData;

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
        //========================================================================================================================
        static public void SendInfo(int endState)
        {
            string data = endState.ToString();
            _SendData(data, 1);
            _SendData(data, 2);
        }

        static public void SendInfo(int w, int h, int power, Battleground.types[,] field)
        {
            string data = power.ToString();
            int n = field.GetLength(0);
            int m = field.GetLength(1);
            data += " " + n + " " + m;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                { data += " " + field[i, j]; }
            }
            _SendData(data, 1);
            _SendData(data, 2);
        }

        static public void SendInfo(int side, double[] power, int[] numbers, int[,] location)
        {
            string data = "";
            int n = power.Length;
            data += n;
            for (int i = 0; i < n; i++)
                data += " " + power[i];
            for (int i = 0; i < n; i++)
                data += " " + numbers[i];
            for (int i = 0; i < n; i++)
                data += " " + location[i, 0] + " " + location[i, 1];
            _SendData(data, side);
        }

        static public void SendInfo(bool check, int client)
        {
            string data = check.ToString();
            _SendData(data, client);
        }

        static public ValueTuple<int, double[], int[,]> GetInfoStart()
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
            return (team, power, army);
        }

        static public int RecvHello()
        {
            string data = _RecieveData();
            int connect = int.Parse(data);
            return connect;
        }

        static public ValueTuple<int, int, int[]> GetInfoMoveCheck()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int army = int.Parse(tokens[0]);
            int team = int.Parse(tokens[1]);
            int[] location = new int[2];
            location[0] = int.Parse(tokens[2]);
            location[1] = int.Parse(tokens[3]);
            return (army, team, location);
        }

        static public ValueTuple<int, int[,]> GetInfoOrders()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int team = int.Parse(tokens[0]);
            int n = int.Parse(tokens[1]);
            int[,] army = new int[n * 2, 3];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < 3; j++)
                    army[i, j] = int.Parse(tokens[2 + i * n + j]);
            return (team, army);
        }
        //==================================================================================================

        public Server(string serverIP, int portNo)
        {
            this.portNo = portNo;
            this.serverIP = serverIP;

            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(serverIP);
            listener1 = new TcpListener(localAdd, portNo);
            listener2 = new TcpListener(localAdd, portNo+1);
            //Console.WriteLine("Listening...");
            listener1.Start();
            listener2.Start();

            //---incoming client connected---
            Console.WriteLine("Listening...");
            client1 = listener1.AcceptTcpClient();
            Console.WriteLine("Client connected");

            client2 = listener2.AcceptTcpClient();
            Console.WriteLine("Client connected");

            //---get the incoming data through a network stream---
            nwStream1 = client1.GetStream();
            nwStream2 = client2.GetStream();
            SendIsCorrect(true, 1);
            SendIsCorrect(true, 2);
        }

        static void AsyncRecieve1()
        {
            byte[] buffer1 = new byte[client1.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = nwStream1.Read(buffer1, 0, client1.ReceiveBufferSize);
            //---convert the data received into a string---
            recieveData = Encoding.ASCII.GetString(buffer1, 0, bytesRead);
            getMessage = true;
        }

        static void AsyncRecieve2()
        {
            byte[] buffer1 = new byte[client1.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = nwStream1.Read(buffer1, 0, client1.ReceiveBufferSize);
            //---convert the data received into a string---
            recieveData = Encoding.ASCII.GetString(buffer1, 0, bytesRead);
            getMessage = true;
        }

        static private async void AsyncRecieve()
        {
            byte[] buffer1 = new byte[client1.ReceiveBufferSize];
            byte[] buffer2 = new byte[client2.ReceiveBufferSize];
            //---read incoming stream---
            int bytesRead = await nwStream1.ReadAsync(buffer1, 0, client1.ReceiveBufferSize);
            bytesRead = await nwStream2.ReadAsync(buffer2, 0, client2.ReceiveBufferSize);
            //---convert the data received into a string---
            recieveData = Encoding.ASCII.GetString(buffer1, 0, bytesRead);
            recieveData = Encoding.ASCII.GetString(buffer2, 0, bytesRead);
        }

        static private string _RecieveData()
        {
            Task.Run(() => AsyncRecieve());
            return recieveData;
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

        static private void _SendData(string data, int client)
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