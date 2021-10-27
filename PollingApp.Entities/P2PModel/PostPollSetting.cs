using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class PostPollSetting
    {
        public string PollName { get; set; }
        public string NewPollName { get; set; }
        public int Index { get; set; }
        public PostPollSetting(string pollName, string newPollName,int index)
        {
            PollName = pollName;
            NewPollName = newPollName;
            Index = index;
        }
    }
}
