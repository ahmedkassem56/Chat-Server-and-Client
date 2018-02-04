using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
namespace MiniChat
{
    public partial class Client
    {
        public static byte[] buffer = new byte[8196];
        public static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public delegate void Received(byte[] buffer, int size);
        public static event Received dRecv;

        public static void Start(string ip,int port)
        {
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                s.NoDelay = true;
                s.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), s);
                dRecv += new Received(Client_dRecv);
            }
            catch
            {
                MessageBox.Show("Can't connect to the server.");
                Environment.Exit(0);
            }
        }

        static void Client_dRecv(byte[] buffer, int size)
        {
            int startIndex = 0;
            startIndex = Parse(buffer, startIndex);
            while (size > startIndex)
            {
                startIndex += Parse(buffer, startIndex);
            }
        }
        public static void Close()
        {
            s.Shutdown(SocketShutdown.Both);
            s.Disconnect(false);
            s.Close();
        }
        public static void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket tmp = (Socket)ar.AsyncState;
                int rcvdBytes = tmp.EndReceive(ar);
                if (rcvdBytes > 0)
                {
                    Array.Resize<byte>(ref buffer, rcvdBytes);
                    dRecv(buffer, rcvdBytes);
                    Array.Resize<byte>(ref buffer, 8196);
                    tmp.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), tmp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disconnected from the server.");
                Close();
            }
        }
    }
}
