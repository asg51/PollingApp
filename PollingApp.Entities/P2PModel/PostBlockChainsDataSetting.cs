using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace PollingApp.Entities.P2PModel
{
    public class PostBlockChainsDataSetting
    {
        IList<string> Urls { get; set; }
        public IList<Block<Voter>> BlockChainForVoters { get; set; }
        public IList<Block<Chosen>> BlockChainForChosens { get; set; }
        public IList<Block<Admin>> BlockChainForAdmins { get; set; }
        public string BlockChainForPollName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int BlockIndex { get; set; }
        public int Index { get; set; }
        public PostBlockChainsDataSetting(IList<string> Urls,IList<Block<Voter>> BlockChainForVoters,
            IList<Block<Chosen>> BlockChainForChosens, IList<Block<Admin>> BlockChainForAdmins, string BlockChainForPollName,
            DateTime StartTime, DateTime FinishTime, int BlockIndex, int Index)
        {
            this.Urls = Urls;
            this.BlockChainForVoters = BlockChainForVoters;
            this.BlockChainForChosens = BlockChainForChosens;
            this.BlockChainForAdmins = BlockChainForAdmins;
            this.BlockChainForPollName = BlockChainForPollName;
            this.StartTime = StartTime;
            this.FinishTime = FinishTime;
            this.BlockIndex = BlockIndex;
            this.Index = Index;
        }
    }
}
