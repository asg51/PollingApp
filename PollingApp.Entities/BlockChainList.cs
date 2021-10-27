using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace PollingApp.Entities
{
    public delegate void BlockChainControl();
    public class BlockChainList
    {
        public event BlockChainControl BlockChainControlEvent;
        private IList<BlockChainsData> list;

        public BlockChainList()
        {
            list = new List<BlockChainsData>();
        }
        public void Add(BlockChainsData blockChainsData)
        {
            list.Add(blockChainsData);
            if (BlockChainControlEvent != null)
                BlockChainControlEvent();
        }
        public void Delete(BlockChainsData blockChainsData)
        {
            list.Remove(blockChainsData);
            if (BlockChainControlEvent != null)
                BlockChainControlEvent();
        }
        public BlockChainsData this[int index]
        {
            get
            {
                return list.FirstOrDefault(x => x.Index == index);
            }
        }
        public IList<BlockChainsData> GetBlockChains()
        {
            return list;
        }
        public void SetList(BlockChainsData blockChainsData)
        {
            BlockChainsData blockChains = list.FirstOrDefault(x => x.BlockChainForPollName == blockChainsData.BlockChainForPollName);
            blockChains = blockChainsData;
        }
        public int LastIndex()
        {
            if (list.Count == 0)
                return 0;
            return list[list.Count - 1].Index;
        }
        public class BlockChainsData
        {
            public IList<string> Urls { get; set; }
            public BlockChain<Voter> BlockChainForVoters { get; set; }
            public BlockChain<Chosen> BlockChainForChosens { get; set; }
            public BlockChain<Admin> BlockChainForAdmins { get; set; }
            public string BlockChainForPollName { get; set; }
            public PollTime PollTime { get; set; }
            public int Index { get; set; }
            public IList<int> UseVoter { get; set; }
            public BlockChain<int> UsedVoter { get; set; }
            public BlockChain<int> UsedVote { get; set; }

            public BlockChainsData(IList<string> urls, BlockChain<Voter> blockChainForVoters, BlockChain<Chosen> blockChainForChosens,
                 BlockChain<Admin> blockChainForAdmins, string blockChainForPollName, PollTime pollTime, int index, IList<int> useVoter,
                 BlockChain<int> usedVoter, BlockChain<int> usedVote)
            {
                Urls = urls;
                BlockChainForVoters = blockChainForVoters;
                BlockChainForChosens = blockChainForChosens;
                BlockChainForAdmins = blockChainForAdmins;
                BlockChainForPollName = blockChainForPollName;
                PollTime = pollTime;
                Index = index;
                UseVoter = useVoter;
                UsedVoter = usedVoter;
                UsedVote = usedVote;
            }
        }
    }
}
