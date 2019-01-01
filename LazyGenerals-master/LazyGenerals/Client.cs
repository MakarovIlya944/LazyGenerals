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

        public ValueTuple<int, int[,]> RecieveInitField()
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
            return (maxArmy, F);
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

        public void SendInitPlacement(int team, int[] order, double[] power, int[,] army)
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

        public void SendArmy(int maxArmy, int[,] army)
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

        private void _SendData(string data)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            nwStream.Write(buffer, 0, buffer.Length);
        }

        ~Client() { client.Close(); }
    }
}