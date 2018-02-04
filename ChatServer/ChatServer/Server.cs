using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ChatServer
{
    class Server
    {
        public static List<Client> Clients = new List<Client>();
        private static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static void SendToAll(Packet p)
        {
            foreach (Client cl in Clients)
            {
                    cl.Send(p);
            }
        }
        public static void Start(string ip, int port)
        {
            try
            {
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Bind(new IPEndPoint(IPAddress.Any,port));
                s.Listen(1);
                s.BeginAccept(new AsyncCallback(OnAccept),s);
            }
            catch { }
        }
        private static void OnAccept(IAsyncResult ar)
        {
            Socket client = s.EndAccept(ar);
            if (Clients.Count < 2)
            {
                Client c = new Client();
                c.Socket = client;
                c.Socket.NoDelay = true;
                c.ip = ((IPEndPoint)client.RemoteEndPoint).Address;
                c.dRecv += new Client.Received(c.RecvEvent);
                client.BeginReceive(c.buffer, 0, c.buffer.Length, SocketFlags.None, new AsyncCallback(c.OnReceive), client);
                Clients.Add(c);
                Console.WriteLine(c.ip + " Connected");

            }
            else
            {
                Console.WriteLine("Someone tried to connect but the server is full.");
                Packet p = new Packet(20);
                client.Send(p.GetBytes());
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            s.BeginAccept(new AsyncCallback(OnAccept), s);
        }
    }
}
