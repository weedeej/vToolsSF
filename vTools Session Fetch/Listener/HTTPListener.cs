using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using vTools_Session_Fetch.Server;
using vTools_Session_Fetch.Server.Interface;

namespace vTools_Session_Fetch.Listener
{
    public class HTTPListener
    {
        public static void Listen(StackPanel outputPanel, String cookies, int Port = 6969)
        {
            IHttpServer server = new HttpServer(Port);
            server.StartServer(outputPanel, cookies);
        }

        public static int GetPort()
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            return ((IPEndPoint)socket.LocalEndPoint!).Port;
        }
    }
}
