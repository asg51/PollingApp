using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollingApp.Entities;

namespace PollingApp.Entities.P2PModel
{
    public class PostAdminSetting
    {
        public IList<Admin> Admins { get; set; }
        public Admin Admin { get; set; }
        public int Index { get; set; }
        public PostAdminSetting(IList<Admin> admins, Admin admin,int index)
        {
            Admins = admins;
            Admin = admin;
            Index = index;
        }
    }
}