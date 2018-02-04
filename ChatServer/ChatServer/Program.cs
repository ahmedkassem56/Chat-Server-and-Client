using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("########################################################################");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to NoN_Stop's Chat Server");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("########################################################################");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Write //help for help");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("########################################################################");
            Console.ForegroundColor = ConsoleColor.Gray;
            new Thread(Commands).Start();
            Server.Start("127.0.0.1", 1000);
        }
        public static void Commands()
        {
            while (true)
            {
                string s = Console.ReadLine();
                if (s.StartsWith("//"))
                {
                    string command = s.Substring(2, s.Length - 2);
                    Console.WriteLine(command);
                    if (command == "exit")
                    {
                        for (int i = 0; i < Server.Clients.Count; i++)
                        {
                            Server.Clients[i].Disconnect();
                        }

                        Environment.Exit(0);
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
