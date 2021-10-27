using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class ConnectAsAdmin
    {
        public string IP { get; set; }
        public string Key { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public ConnectAsAdmin(string ıp,string key,string password,string name)
        {
            IP = ıp;
            Key = key;
            Password = password;
            Name = name;
        }
    }
}
