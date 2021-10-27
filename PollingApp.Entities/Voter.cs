using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities
{
    public class Voter
    {
        public string Key { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AddedAdminKey { get; set; }
        public int Index { get; set; }

        public Voter(string key,string password,string name,string surname,string addedAdminKey,int index)
        {
            Key = key;
            Password = password;
            Name = name;
            Surname = surname;
            AddedAdminKey = addedAdminKey;
            Index = index;
        }
    }
}
