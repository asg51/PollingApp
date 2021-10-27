using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
   public class PostVoterSetting
    {
        public IList<Voter> Voters { get; set; }
        public Voter Voter { get; set; }
        public int Index { get; set; }
        public PostVoterSetting(IList<Voter> voters, Voter voter, int index)
        {
            Voters = voters;
            Voter = voter;
            Index = index;
        }
    }
}
