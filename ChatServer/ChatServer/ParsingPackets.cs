using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatServer
{
    partial class Client
    {
        private int Parse(byte[] buffer,int startIndex)
        {
            try
            {
                Packet p = new Packet(buffer,startIndex);
                switch (p.Opcode)
                {
                    case 6:
                        string name = p.ReadString();
                        if (CheckIfNameLoggedIn(name))
                        {
                            Packet response = new Packet(6);
                            response.AddBYTE(2);
                            Send(response);
                        }
                        else
                        {
                            Name = name;
                            Packet response = new Packet(6);
                            response.AddBYTE(1);
                            Send(response);
                            Packet pc = new Packet(3);
                            List<string> clients = new List<string>();
                            foreach (Client client in Server.Clients)
                            {
                                if (client.Name != "")
                                    clients.Add(client.Name);
                            }
                            pc.AddWORD((ushort)clients.Count);
                            foreach (string client in clients)
                            {
                                pc.AddString(client);
                            }
                            Send(pc);
                            Packet pa = new Packet(7);
                            pa.AddString(Name);
                            Server.SendToAll(pa);
                        }
                        break;
                    case 1:
                        string msg = p.ReadString();
                        if (string.IsNullOrWhiteSpace(msg))
                            break;
                        Packet packet = new Packet(2);
                        packet.AddString(Name);
                        packet.AddString(msg);
                        Server.SendToAll(packet);
                        break;
                }
                return p.Size;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown packet. disconnected");
                Disconnect();
                Console.Read();
            }
            return -1;
        }
        public bool CheckIfNameLoggedIn(string name)
        {
            for (int i = 0; i < Server.Clients.Count; i++)
            {
                if (Server.Clients[i].Name == name)
                    return true;
            }
            return false;
        }
    }
}
