using Newtonsoft.Json;
using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PollingApp.Entities.BlockChainList;
using PollingApp.BL.ValidationRules;
using FluentValidation.Results;

namespace PollingApp.BL.Contcat
{
    public delegate void BlockChain(BlockChainsData blockChainsData);
    public class BlockChainManager
    {
        public event BlockChain ViewerScreenUpdate;
        BlockChainValidaditon validations;
        public BlockChainManager()
        {
            validations = new BlockChainValidaditon();
        }
        private BlockChain<Chosen> BlockChainChosen(Poll poll)
        {
            BlockChain<Chosen> blockChosen = new BlockChain<Chosen>();
            foreach (Chosen chosen in poll.Chosen.GetList())
            {
                blockChosen.AddBlock(new Block<Chosen>(DateTime.Now, blockChosen.GetLatestBlock().Hash,
                    new Chosen(chosen.ChosenName, chosen.Index)));
            }
            return blockChosen;
        }
        private BlockChain<Admin> BlockChainAdmin(Poll poll)
        {
            BlockChain<Admin> blockAdmin = new BlockChain<Admin>();

            foreach (Admin admin in poll.Admins.GetList())
            {
                blockAdmin.AddBlock(new Block<Admin>(DateTime.Now, blockAdmin.GetLatestBlock().Hash,
                    new Admin(admin.Key, admin.Password, admin.Name, admin.Surname, admin.Index)));
            }

            return blockAdmin;
        }
        private BlockChain<Voter> BlockChainVoter(Poll poll)
        {
            BlockChain<Voter> blockVoter = new BlockChain<Voter>();

            foreach (Voter voter in poll.Voter.GetList())
            {
                blockVoter.AddBlock(new Block<Voter>(DateTime.Now, blockVoter.GetLatestBlock().Hash,
                    new Voter(voter.Key, voter.Password, voter.Name, voter.Surname, voter.AddedAdminKey, voter.Index)));
            }
            return blockVoter;
        }
        public async Task AddAsync(Poll poll)
        {
            PollingList.dbPoll.Delete(poll);

            string Pollname = poll.PollingName;
            PollTime PollTime = new PollTime(poll.PollTime.StartTime, poll.PollTime.FinishTime);
            int Index = PollingList.blockChainList.LastIndex() + 1;



            BlockChain<Chosen> blockChosen = null;
            BlockChain<Admin> blockAdmin = null;
            BlockChain<Voter> blockVoter = null;

            Parallel.Invoke(() =>
            {
                blockChosen = BlockChainChosen(poll);
            },
            () =>
            {
                blockAdmin = BlockChainAdmin(poll);
            },
            () =>
            {
                blockVoter = BlockChainVoter(poll);
            }
            );

            BlockChainsData blockChainsData = new BlockChainsData(poll.Urls, blockVoter, blockChosen, blockAdmin, Pollname, PollTime, Index,
               new List<int>(), new BlockChain<int>(), new BlockChain<int>());

            PollingList.blockChainList.Add(blockChainsData);
            Managers.clientManager.AddBlockChainList(blockChainsData.Urls, blockChainsData.BlockChainForPollName, blockChainsData);
        }
        public async Task<BlockChainsData> ConvertAsync(Poll poll)
        {
            string Pollname = poll.PollingName;
            PollTime PollTime = new PollTime(poll.PollTime.StartTime, poll.PollTime.FinishTime);
            int Index = PollingList.blockChainList.LastIndex() + 1;


            BlockChain<Chosen> blockChosen = null;
            BlockChain<Admin> blockAdmin = null;
            BlockChain<Voter> blockVoter = null;

            Parallel.Invoke(() =>
            {
                blockChosen = BlockChainChosen(poll);
            },
            () =>
            {
                blockAdmin = BlockChainAdmin(poll);
            },
            () =>
            {
                blockVoter = BlockChainVoter(poll);
            }
            );

            BlockChainsData blockChainsData = new BlockChainsData(poll.Urls, blockVoter, blockChosen, blockAdmin, Pollname, PollTime, Index, null, null, null);

            return blockChainsData;
        }
        public BlockChainsData Convert(Poll poll)
        {
            string Pollname = poll.PollingName;
            PollTime PollTime = new PollTime(poll.PollTime.StartTime, poll.PollTime.FinishTime);
            int Index = PollingList.blockChainList.LastIndex() + 1;

            BlockChain<Chosen> blockChosen = BlockChainChosen(poll);
            BlockChain<Admin> blockAdmin = BlockChainAdmin(poll);
            BlockChain<Voter> blockVoter = BlockChainVoter(poll);

            BlockChainsData blockChainsData = new BlockChainsData(poll.Urls, blockVoter, blockChosen, blockAdmin, Pollname, PollTime, Index, null, null, null);

            return blockChainsData;
        }
        public bool Equivocation(BlockChainsData blockChainsData, Poll poll)
        {
            try
            {
                Parallel.Invoke(() =>
                {
                    IList<Block<Admin>> adminBlocks = blockChainsData.BlockChainForAdmins.GetBlocks();
                    IList<Admin> admins = poll.Admins.GetList();
                    for (int i = 1; i < adminBlocks.Count; i++)
                    {
                        if (!AdminBlockEquivocation(adminBlocks[i], admins[i - 1]))
                        {
                            throw new Exception();
                        }
                    }
                }, 
                () =>
                {
                    IList<Block<Chosen>> chosenBlocks = blockChainsData.BlockChainForChosens.GetBlocks();
                    IList<Chosen> chosenes = poll.Chosen.GetList();
                    for (int i = 1; i < chosenBlocks.Count; i++)
                    {
                        if (!ChosenBlockEquivocation(chosenBlocks[i], chosenes[i - 1]))
                        {
                            throw new Exception();
                        }
                    }
                },
                () =>
                {
                    IList<Block<Voter>> voterBlocks = blockChainsData.BlockChainForVoters.GetBlocks();
                    IList<Voter> voters = poll.Voter.GetList();
                    for (int i = 1; i < voterBlocks.Count; i++)
                    {
                        if (!VoterBlockEquivocation(voterBlocks[i], voters[i - 1]))
                        {
                            throw new Exception();
                        }
                    }
                },
                ()=> 
                {
                    if (!ChianBlockEquivocation(blockChainsData, poll))
                    {
                        throw new Exception(); ;
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool AddFromServer(ref BlockChainsData blockChainsData)
        {
            ValidationResult result = validations.Validate(blockChainsData);
            if (result.IsValid)
            {
                BlockChain<Voter>.BlockChainSetting(blockChainsData.BlockChainForVoters.GetBlocks());
                BlockChain<Admin>.BlockChainSetting(blockChainsData.BlockChainForAdmins.GetBlocks());
                BlockChain<Chosen>.BlockChainSetting(blockChainsData.BlockChainForChosens.GetBlocks());
                BlockChain<int>.BlockChainSetting(blockChainsData.UsedVoter.GetBlocks());
                BlockChain<int>.BlockChainSetting(blockChainsData.UsedVote.GetBlocks());
                blockChainsData.Index = PollingList.blockChainList.LastIndex() + 1;
                PollingList.blockChainList.Add(blockChainsData);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void P2PRefreshList(BlockChainsData blockChainsData, string pollName)
        {
            BlockChainsData blockChains = PollingList.blockChainList.GetBlockChains().
                FirstOrDefault(x => x.BlockChainForPollName == pollName);
            blockChains = blockChainsData;
        }
        public void Delete(BlockChainsData blockChainsData, bool deleteFromServer)
        {

            if (deleteFromServer)
            {
                Managers.clientManager.DeleteBlockChain(blockChainsData.Urls, blockChainsData.BlockChainForPollName);
            }
            else
            {
                Managers.clientManager.ExitSystemBlockChain(blockChainsData.Urls, blockChainsData.BlockChainForPollName);
            }


            PollingList.blockChainList.Delete(blockChainsData);
        }
        private bool AdminBlockEquivocation(Block<Admin> block, Admin admin)
        {
            try
            {
                return
                    block.Transactions.Index == admin.Index &&
                    block.Transactions.Key == admin.Key &&
                    block.Transactions.Name == admin.Name &&
                    block.Transactions.Password == admin.Password &&
                    block.Transactions.Surname == admin.Surname;
            }
            catch
            {
                return false;
            }
        }
        private bool VoterBlockEquivocation(Block<Voter> block, Voter voter)
        {
            try
            {
                return
                    block.Transactions.Index == voter.Index &&
                    block.Transactions.Key == voter.Key &&
                    block.Transactions.Name == voter.Name &&
                    block.Transactions.Password == voter.Password &&
                    block.Transactions.Surname == voter.Surname &&
                    block.Transactions.AddedAdminKey == voter.AddedAdminKey;
            }
            catch
            {
                return false;
            }
        }
        private bool ChosenBlockEquivocation(Block<Chosen> block, Chosen chosen)
        {
            try
            {
                return
                    block.Transactions.Index == chosen.Index &&
                    block.Transactions.ChosenName == chosen.ChosenName;
            }
            catch
            {
                return false;
            }
        }
        private bool ChianBlockEquivocation(BlockChainsData block, Poll poll)
        {
            try
            {
                return
                    block.BlockChainForPollName == poll.PollingName &&
                    block.PollTime.StartTime.ToString("dddd, dd MMMM yyyy") == poll.PollTime.StartTime.ToString("dddd, dd MMMM yyyy") &&
                    block.PollTime.FinishTime.ToString("dddd, dd MMMM yyyy") == poll.PollTime.FinishTime.ToString("dddd, dd MMMM yyyy");
            }
            catch
            {
                return false;
            }
        }
        public void Voting(string pollName, int voter, int chosen)
        {
            BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().
                FirstOrDefault(x => x.BlockChainForPollName == pollName);

            blockChainsData.UsedVote.AddBlock(new Block<int>(DateTime.Now, blockChainsData.UsedVote.GetLatestBlock().Hash, chosen));
            blockChainsData.UsedVoter.AddBlock(new Block<int>(DateTime.Now, blockChainsData.UsedVoter.GetLatestBlock().Hash, voter));
            blockChainsData.UseVoter.Remove(voter);

            Managers.clientManager.Voting(pollName, voter, chosen, blockChainsData.UsedVoter.GetBlocks(),
                blockChainsData.UsedVote.GetBlocks(), blockChainsData.UseVoter);
        }
        public bool P2PVoting(string pollName, int voter, int chosen,
            IList<Block<int>> voterBlocks, IList<Block<int>> choesenBlocks, IList<int> useVoter)
        {
            if (BlockChain<int>.BlockChainSetting(voterBlocks) && BlockChain<int>.BlockChainSetting(choesenBlocks))
            {

                bool voterIsValid = BlockChain<int>.IsValid(voterBlocks);
                bool chosenIsValid = BlockChain<int>.IsValid(choesenBlocks);

                BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().
                    FirstOrDefault(x => x.BlockChainForPollName == pollName);

                bool voterState = VotingVoterEquivocation(blockChainsData.UsedVoter.GetBlocks(), voterBlocks, voter);
                bool chosenState = VotingChosenEquivocation(blockChainsData.UsedVote.GetBlocks(), choesenBlocks, chosen);
                bool useVoterState = VotingUseVoterEquivocation(blockChainsData.UseVoter, useVoter, voter);
                if (voterIsValid && chosenIsValid && voterState && chosenState && useVoterState)
                {
                    blockChainsData.UsedVote.ChainSet(choesenBlocks);
                    blockChainsData.UsedVoter.ChainSet(voterBlocks);
                    blockChainsData.UseVoter = useVoter;

                    if (ViewerScreenUpdate != null)
                        ViewerScreenUpdate(blockChainsData);

                    return true;
                }
            }

            return false;
        }
        private bool VotingVoterEquivocation(IList<Block<int>> voterBlocks1, IList<Block<int>> voterBlocks2, int voter)
        {
            for (int i = 0; i < voterBlocks1.Count; i++)
            {
                if (!(voterBlocks1[i].Hash == voterBlocks2[i].Hash &&
                    voterBlocks1[i].Index == voterBlocks2[i].Index &&
                    voterBlocks1[i].Nonce == voterBlocks2[i].Nonce &&
                    voterBlocks1[i].PreviousHash == voterBlocks2[i].PreviousHash &&
                    voterBlocks1[i].TimeStamp == voterBlocks2[i].TimeStamp &&
                    voterBlocks1[i].Transactions == voterBlocks2[i].Transactions))
                {
                    return false;
                }
            }
            if (voterBlocks2[voterBlocks2.Count - 1].Transactions == voter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool VotingChosenEquivocation(IList<Block<int>> chosenBlocks1, IList<Block<int>> chosenBlocks2, int chosen)
        {
            for (int i = 0; i < chosenBlocks1.Count; i++)
            {
                if (!(chosenBlocks1[i].Hash == chosenBlocks2[i].Hash &&
                    chosenBlocks1[i].Index == chosenBlocks2[i].Index &&
                    chosenBlocks1[i].Nonce == chosenBlocks2[i].Nonce &&
                    chosenBlocks1[i].PreviousHash == chosenBlocks2[i].PreviousHash &&
                    chosenBlocks1[i].TimeStamp == chosenBlocks2[i].TimeStamp &&
                    chosenBlocks1[i].Transactions == chosenBlocks2[i].Transactions))
                {
                    return false;
                }
            }
            if (chosenBlocks2[chosenBlocks2.Count - 1].Transactions == chosen)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool VotingUseVoterEquivocation(IList<int> useVoterBlocks1, IList<int> useVoterBlocks2, int useVoter)
        {
            for (int i = 0; i < useVoterBlocks2.Count; i++)
            {
                if (!(useVoterBlocks1[i] == useVoterBlocks2[i]))
                {
                    return false;
                }
            }
            if (useVoterBlocks1[useVoterBlocks1.Count - 1] == useVoter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
