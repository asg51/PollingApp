using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class PostVoting
    {
        public int ChosenIndex { get; set; }
        public int VoterIndex { get; set; }
        public IList<Block<int>> ChosenBlocks { get; set; }
        public IList<Block<int>> VoterBlocks { get; set; }
        public IList<int> UseVoter { get; set; }
        public int Index { get; set; }
        public PostVoting(int chosenIndex, int voterIndex, IList<Block<int>> chosenBlocks,
            IList<Block<int>> voterBlocks, IList<int> useVoter,int index)
        {
            ChosenIndex = chosenIndex;
            VoterIndex = voterIndex;
            ChosenBlocks = chosenBlocks;
            VoterBlocks = voterBlocks;
            UseVoter = useVoter;
            Index = index;
        }
    }
}
