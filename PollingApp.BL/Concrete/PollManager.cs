using FluentValidation.Results;
using Newtonsoft.Json;
using PollingApp.BL.ValidationRules;
using PollingApp.Entities;
using PollingApp.Entities.P2PModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.Contcat
{
    public class PollManager
    {
        PollValidation pollValidations;
        PollTimeValidation pollTimeValidations;
        public PollManager()
        {
            pollValidations = new PollValidation();
            pollTimeValidations = new PollTimeValidation();
        }
        public void Add(Poll poll, Admin admin)
        {
            ValidationResult result = pollValidations.Validate(poll);
            if (result.IsValid)
            {
                try
                {
                    Managers.adminManager.AdminControl(admin);
                    poll.Admins.Add(admin);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public void P2PAdd(Poll poll)
        {
            poll.Index = PollingList.dbPoll.GetLastIndex() + 1;
            PollingList.dbPoll.Add(poll);
        }
        public void Edit(Poll poll, Poll editpoll)
        {
            ValidationResult result = pollValidations.Validate(editpoll);
            if (result.IsValid)
            {
                if (PollNewNameControl(editpoll.PollingName))
                    Managers.clientManager.EditPoll(poll.Urls, poll.PollingName, editpoll.PollingName);
                else
                    throw new Exception("Aynı isimde seçim var!");
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PEdit(string newPollName)
        {
            if (PollingList.dbPoll.Search(newPollName) == null)
                return true;
            return false;
        }
        public void P2PSetPollName(string pollName, string newPollName)
        {
            PollingList.dbPoll.Search(pollName).PollingName = newPollName;
        }
        public void Delete(Poll poll, bool deleteFromServer)
        {
            if (deleteFromServer)
            {
                Managers.clientManager.DeletePoll(poll.Urls, poll.PollingName);
            }
            else
            {
                Managers.clientManager.ExitSystemPoll(poll.Urls, poll.PollingName);
            }
            PollingList.dbPoll.Delete(poll);

        }
        public bool P2PDelete(string newPollName)
        {
            Poll poll = PollingList.dbPoll.Search(newPollName);
            if (poll != null)
            {
                PollingList.dbPoll.Delete(poll);
            }
            return false;
        }
        public void P2PBlockChainStart(string pollName)
        {
            PollingList.dbPoll.Search(pollName).BlockChainStartState = true;
        }
        public int LastIndex(Poll poll)
        {
            if (poll.Chosen.Count == 0)
                return 0;
            return poll.Chosen.GetIndex(poll.Chosen.Count - 1);
        }
        public void AddPollTime(Poll poll, PollTime pollTime)
        {
            if (poll.Admins.Count <= 0)
                throw new Exception("En az 1 tane admin olmak zorunda!");

            if (poll.Chosen.Count <= 1)
                throw new Exception("En az iki aday olmak zorunda!");

            if (poll.Voter.Count <= 0)
                throw new Exception("En az bir seçmen olmak zorunda");

            ValidationResult result = pollTimeValidations.Validate(pollTime);
            if (result.IsValid)
            {
                poll.PollTime = pollTime;

                if (PollingList.dbPoll.GetList().FirstOrDefault(x => x.Index == poll.Index) == null)
                    PollingList.dbPoll.Add(poll);

            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool PollNewNameControl(string newName)
        {
            foreach (Poll poll in PollingList.dbPoll.GetList())
            {
                if (poll.PollingName == newName)
                    return false;
            }
            foreach (BlockChainsData blockChainsData in PollingList.blockChainList.GetBlockChains())
            {
                if (blockChainsData.BlockChainForPollName == newName)
                    return false;
            }
            return true;
        }
        public Poll GetPoll(int index) => PollingList.dbPoll[index];
        public bool P2PAddBlockChainList(PostBlockChainsDataSetting blockChainsDataSetting, out BlockChainsData
            blockChainsData, out Poll poll)
        {
            poll = PollingList.dbPoll.Search(blockChainsDataSetting.BlockChainForPollName);
            blockChainsData = new BlockChainsData(null,
                new BlockChain<Voter>(blockChainsDataSetting.BlockChainForVoters),
              new BlockChain<Chosen>(blockChainsDataSetting.BlockChainForChosens),
              new BlockChain<Admin>(blockChainsDataSetting.BlockChainForAdmins), blockChainsDataSetting.BlockChainForPollName,
                new PollTime(blockChainsDataSetting.StartTime, blockChainsDataSetting.FinishTime), blockChainsDataSetting.Index,
                new List<int>(), new BlockChain<int>(), new BlockChain<int>());
            return Managers.blockChainManager.Equivocation(blockChainsData, poll);
        }
        public void LoginPoll(string pollName, string ıp)
        {
            P2PContext.client.PollConnect(new ConnectAsAdmin(P2PContext.IP, null, null, pollName), ıp);
        }
        public void FillPoll()
        {
            foreach (var poll in PollingList.dbPoll.GetList())
            {
                    Managers.clientManager.ExitSystemPoll(poll.Urls, poll.PollingName);
            }
            PollingList.dbPoll.GetList().Clear();

            foreach (var poll in PollingList.blockChainList.GetBlockChains())
            {
                Managers.clientManager.ExitSystemBlockChain(poll.Urls, poll.BlockChainForPollName);
            }
            PollingList.blockChainList.GetBlockChains().Clear();
        }
    }
}
