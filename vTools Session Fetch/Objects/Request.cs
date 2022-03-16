using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vTools_Session_Fetch.Objects
{
    public partial class Headers
    {
        public string Host { get; set; } = String.Empty;
        public string Connection { get; set; } = String.Empty;
        public string UserAgent { get; set; } = String.Empty;
        public Dictionary<string, string> Others { get; set; } = new Dictionary<string,string>();
    }
    public class Request
    {
        public string raw { get; set; } = String.Empty;
        public string Route { get; set; } = String.Empty;
        public string Method { get; set; } = String.Empty;
        public Headers Headers { get; set; } = new Headers();

        public Request(String incommingMessage)
        {
            this.raw = incommingMessage;
            var message = incommingMessage.Replace("\r", String.Empty).Split('\n');
            foreach (var item in message)
            {
                var line = item.Trim().ToLower().Split(':');
                switch (line[0])
                {
                    case "host":
                        Headers.Host = line[1].Trim();
                        break;
                    case "connection": 
                        Headers.Connection = line[1].Trim();
                        break;
                    case "user-agent":
                        Headers.UserAgent = line[1].TrimStart(' ');
                        break;
                    default:
                        try
                        {
                            Headers.Others.Add(line[0], line[1]);
                        } catch (IndexOutOfRangeException)
                        {
                            var route = item.Split(" HTTP/")[0].Split(' ');
                            if (route.Length < 2) break;
                            Route = route[1];
                            Method = route[0];
                        }
                        break;
                }
            }
        }
    }
}
