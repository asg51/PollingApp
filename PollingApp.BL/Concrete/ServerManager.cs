using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollingApp.Entities;
using PollingApp.Entities.P2PModel;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.Contcat
{
    public class ServerManager
    {
        public object AdminControl(ConnectAsAdmin connectAsAdmin, ref string whichOne)
        {
            foreach (Poll poll in PollingList.dbPoll.GetList())
            {
                if (poll.PollingName == connectAsAdmin.Name)
                {
                    foreach (Admin admin in poll.Admins.GetList())
                    {
                        if (admin.Key == connectAsAdmin.Key && admin.Password == connectAsAdmin.Password)
                        {
                            whichOne = "Poll";
                            PostPoll postPoll = new PostPoll(poll.Urls, poll.PollingName, poll.Index, poll.PollTime,
                                poll.Admins.GetList(), poll.Voter.GetList(), poll.Chosen.GetList());
                            BaseModel modelbase = new BaseModel("admin login data poll",
                                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name), postPoll);
                            if (postPoll.Urls.Where(x => x == connectAsAdmin.Name).ToList().Count == 0)
                            {
                                postPoll.Urls.Add(connectAsAdmin.IP);
                            }
                            return modelbase;
                        }
                    }
                }
            }
            foreach (BlockChainsData data in PollingList.blockChainList.GetBlockChains())
            {
                if (data.BlockChainForPollName == connectAsAdmin.Name)
                {
                    IList<Block<Admin>> admin = data.BlockChainForAdmins.GetBlocks();
                    for (int i = 1; i < admin.Count; i++)
                    {
                        if (admin[i].Transactions.Key == connectAsAdmin.Key &&
                            admin[i].Transactions.Password == connectAsAdmin.Password)
                        {
                            whichOne = "BlockChain";
                            BaseModel modelbase = new BaseModel("admin login data blockchain",
                                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name), data);
                            if (data.Urls.Where(x => x == connectAsAdmin.Name).ToList().Count == 0)
                            {
                                data.Urls.Add(connectAsAdmin.IP);
                            }
                            return modelbase;
                        }
                    }
                }
            }
            return null;
        }
        public string GetIpAddress() => P2PContext.server.GetIpAddress();

        public void DeletePoll(string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            foreach (string url in poll.Urls)
            {
                if (P2PContext.client.UrlControl(url, pollName))
                {
                    P2PContext.client.Close(url);
                }
            }
            PollingList.dbPoll.Delete(poll);
        }
        public void ExitSystemPoll(string pollName, string url)
        {
            Poll poll = PollingList.dbPoll.GetList().FirstOrDefault(x => x.PollingName == pollName);
            if (P2PContext.client.UrlControl(url, pollName))
            {
                P2PContext.client.Close(url);
                poll.Urls.Remove(url);
            }
        }
        public void DeleteBlockChain(string pollName)
        {
            BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().
                FirstOrDefault(x => x.BlockChainForPollName == pollName);
            foreach (string url in blockChainsData.Urls)
            {
                if (P2PContext.client.UrlControl(url, pollName))
                {
                    P2PContext.client.Close(url);
                }
            }
            PollingList.blockChainList.Delete(blockChainsData);
        }
        public void ExitSystemBlockChain(string pollName, string url)
        {
            BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == pollName);
            if (P2PContext.client.UrlControl(url, pollName))
            {
                P2PContext.client.Close(url);
                blockChainsData.Urls.Remove(url);
            }
        }
        public BlockChainsData GetPoll(string pollName)
        {
            return PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == pollName);
        }
        public bool GetUseVoter(string pollName, int index)
        {
            try
            {
                BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == pollName);
                if (blockChainsData.UseVoter.SingleOrDefault(x => x == index) <= 0)
                {
                    blockChainsData.UseVoter.Add(index);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public void IncorrectUseVoterDataRefresh(string pollName, IList<int> list)
        {
            PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == pollName).UseVoter = list;
        }
    }
}
