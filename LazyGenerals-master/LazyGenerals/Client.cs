using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace LazyServer
{
    public class Client
    {
        private readonly int portNo;
        private readonly string serverIP;
        private TcpClient client;
        private NetworkStream nwStream;

        public Client(string serverIP, int portNo)
        {
            this.portNo = portNo;
            this.serverIP = serverIP;

            //---create a TCPClient object at the IP and port no.---
            client = new TcpClient(serverIP, portNo);
            nwStream = client.GetStream();
        }

        public void SendHello(int team)
        {
            _SendData(team.ToString());
        }

        public int RecieveEnd()
        {
            string data = _RecieveData();
            return int.Parse(data);
        }

        public ValueTuple<int, int, int, int[,]> RecieveInitField()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int maxArmy = int.Parse(tokens[0]);
            int n = int.Parse(tokens[1]);
            int m = int.Parse(tokens[2]);
            int[,] F = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    int index = i * m + j + 3;
                    F[i, j] = int.Parse(tokens[index]);
                }
            }
            return (n, m, maxArmy, F);
        }

        public ValueTuple<double[], int[], int[,]> RecievePlacement()
        {
            string data = _RecieveData();
            string[] tokens = data.Split(new string[]
                {" "}, StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(tokens[0]);
            double[] power = new double[n];
            int[,] locations = new int[n, 2];
            int[] armyNums = new int[n];
            int ind = 1;
            for (int i = 0; i < n; i++)
                power[i] = double.Parse(tokens[ind++]);
            for (int i = 0; i < n; i++)
                armyNums[i] = int.Parse(tokens[ind++]);
            for (int i = 0; i < n; i++)
            {
                locations[i, 0] = int.Parse(tokens[ind++]);
                locations[i, 1] = int.Parse(tokens[ind++]);
            }
            return (power, armyNums, locations);
        }

        public void SendInitPlacement(int team, double[] power, int[,] army)
        {
            string data = team.ToString();
            int n = power.Length;
            data += " " + n;
            for (int i = 0; i < power.Length; i++)
                data += " " + power[i]; 
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    data += " " + army[i, j];
            _SendData(data);
        }

        public void SendOrder(int team, int[] orders, Point[][] armies)
        {
            string data = team.ToString();
            int n = orders.Length;
            data += " " + n;
            for(int i=0;i<n;i++)
            {
                data += $" {orders[i]} {armies[orders[i]][1].X} {armies[orders[i]][1].Y}";
                data += $" 0 {armies[orders[i]][2].X} {armies[orders[i]][2].Y}";
            }
            _SendData(data);
        }

        public bool SendXY(int team, int id, int x, int y)
        {
            string data = team.ToString() +
                " " + id + " " + x + " " + y;
            _SendData(data);
            return RecieveIsCorrect();
        }

        public bool RecieveIsCorrect()
        {
            string data = _RecieveData();
            bool isCorrect = bool.Parse(data);
            return isCorrect;
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

        private void _SendData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        ~Client() { client.Close(); }
    }
}