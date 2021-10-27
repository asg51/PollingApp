using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace PollingApp.Entities.P2PModel
{
    public class PostPoll
    {
        public IList<string> Urls { get; set; }
        public string PollingName { get; set; }
        public int Index { get; set; }
        public PollTime PollTime { get; set; }
        public IList<Admin> Admins { get; set; }
        public IList<Voter> Voters { get; set; }
        public IList<Chosen> Chosens { get; set; }

        public PostPoll(IList<string> urls, string pollname,int index,
            PollTime pollTime,IList<Admin> admins,IList<Voter> voters, IList<Chosen> chosens)
        {
            Urls = urls;
            PollingName = pollname;
            Index = index;
            PollTime = pollTime;
            Admins = admins;
            Voters = voters;
            Chosens = chosens;
        }
    }
}
