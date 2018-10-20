
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Server1
    {
        static Dictionary<string, ClientHandler> dic = new Dictionary<string, ClientHandler>();
        static List<int> li = new List<int>();
        static Socket s;
        static Socket a;

        static void Connection()
        {
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            s.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234));
            s.Listen(5);
        }

        static void Operation()
        {
            while (true)
            {
                // Accept the incoming request
                a = s.Accept();
                try
                {

                    byte[] Buf = new byte[255];
                    int rec = a.Receive(Buf, 0, Buf.Length, 0);
                    Array.Resize(ref Buf, rec);
                    string client_name = Encoding.Default.GetString(Buf);
                    Console.WriteLine(client_name + " Join");
                    byte[] send = Encoding.Default.GetBytes(client_name);
                    a.Send(send, 0, send.Length, 0);
                    byte[] Buf1 = new byte[255];
                    int rec1 = a.Receive(Buf1, 0, Buf1.Length, 0);
                    Array.Resize(ref Buf1, rec1);
                    string dis1 = Encoding.Default.GetString(Buf1);
                    Console.WriteLine(dis1);
                    ClientHandler mtch = new ClientHandler(a, client_name, dis1, dic);
                    dic.Add(client_name, mtch);
                   // li.LastIndexOf();        
                    Thread t = new Thread(mtch.Run);
                    Thread t1 = new Thread(mtch.Send);
                    t.Start();
                    t1.Start();
                }
                catch
                {
                    Console.WriteLine("Hello");
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("WelCome To Chat Server");
            Connection();
            Operation();

        }

    }
}