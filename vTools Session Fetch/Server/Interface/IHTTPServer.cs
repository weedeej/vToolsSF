using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace vTools_Session_Fetch.Server.Interface
{
    public interface IHttpServer
    {
        void StartServer(StackPanel outputPanel, String cookies);
    }
}
