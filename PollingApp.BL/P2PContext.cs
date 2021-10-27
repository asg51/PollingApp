using PollingApp.BL.P2P;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PollingApp.BL
{
    public static class P2PContext
    {
        internal static string IP = string.Empty;
        public static Client client = new Client();
        public static Server server = new Server();
    }
}
