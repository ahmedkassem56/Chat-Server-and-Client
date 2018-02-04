using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ChatServer
{
    partial class Client
    {
        public Socket Socket;
        public IPAddress ip;
        public string Name = "";
        public byte[] buffer = new byte[8196];
        public delegate void Received(byte[] buffer, int size);
        public event Received dRecv;
        public void RecvEvent(byte[] buffer, int size)
        {
            int startIndex = 0;
            startIndex = Parse(buffer,startIndex);
            while (size > startIndex)
            {
                startIndex += Parse(buffer, startIndex);
            }
        }
        public void Send(Packet p)
        {
            if (Socket.Connected && Socket != null)

                Socket.BeginSend(p.GetBytes(), 0, p.GetBytes().Length, SocketFlags.None, new AsyncCallback(Sent), Socket);
        }
        void Sent(IAsyncResult ar)
        {
            int bytesSent = Socket.EndSend(ar);
        }

        public void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket tmp = (Socket)ar.AsyncState;
                int rcvdBytes = tmp.EndReceive(ar);
                if (rcvdBytes > 0 && tmp.Connected)
                {
                    Array.Resize(ref buffer, rcvdBytes);
                    dRecv(buffer, rcvdBytes);
                    Array.Resize(ref buffer, 8196);
                }
                tmp.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), tmp);
            }
            catch
            {
                Console.WriteLine(ip.ToString() + " Disconnected");
                Disconnect();
                Packet pc = new Packet(8);
                pc.AddString(Name);
                Server.SendToAll(pc);
            }

        }
        public void Disconnect()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Disconnect(false);
            Socket = null;
            Server.Clients.Remove(this);
        }
    }
}
