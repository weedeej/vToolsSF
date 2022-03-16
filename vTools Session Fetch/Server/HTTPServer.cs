using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using vTools_Session_Fetch.Server.Interface;
using vTools_Session_Fetch.Objects;

namespace vTools_Session_Fetch.Server
{
    public class HttpServer : IHttpServer
    {
        private readonly TcpListener listener;
        private int _port;
        public HttpServer(int port)
        {
            _port = port;
            this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        }

        public async void StartServer(StackPanel outputPanel, String cookies)
        {
            this.listener.Start();
            while (true)
            {
                var client = await this.listener.AcceptTcpClientAsync();
                var buffer = new byte[10240];
                var stream = client.GetStream();
                var length = stream.Read(buffer, 0, buffer.Length);
                var incomingMessage = new Request(Encoding.UTF8.GetString(buffer, 0, length));
                if (!incomingMessage.Headers.UserAgent.Contains($"port/{_port}")) continue;
                var result = $"{{\"cookies\":\"{cookies}\"}}";
                var response = "HTTP/1.0 200 OK" + Environment.NewLine
                        + "Content-Length: " + result.Length + Environment.NewLine
                        + "Access-Control-Allow-Origin: *" + Environment.NewLine
                        + "Content-Type: " + "application/json" + Environment.NewLine
                        + Environment.NewLine
                        + result
                        + Environment.NewLine + Environment.NewLine;
                stream.Write(Encoding.UTF8.GetBytes(response));
                Logger.InfoLogger.Log(message: incomingMessage.raw);
                Logger.InfoLogger.Log(message: response);
            }
        }
    }
}
