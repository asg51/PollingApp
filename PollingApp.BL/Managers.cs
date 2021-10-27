using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollingApp.BL.Contcat;

namespace PollingApp.BL
{
    public static class Managers
    {
        public static ChosenManager chosenManager = new ChosenManager();
        public static PollManager pollManager = new PollManager();
        public static AdminManager adminManager = new AdminManager();
        public static VoterManager voterManager = new VoterManager();
        public static BlockChainManager blockChainManager = new BlockChainManager();
        public static ServerManager serverManager = new ServerManager();
        public static ClientManager clientManager = new ClientManager();
    }
}
