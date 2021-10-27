using FluentValidation.Results;
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
    public class VoterManager
    {
        VoterValidation validations;
        public VoterManager()
        {
            validations = new VoterValidation();
        }
        public void Add(Voter voter, Poll poll)
        {
            ValidationResult result = validations.Validate(voter);
            if (result.IsValid)
            {
                if (poll.Voter.GetList().Where(x => x.Key == voter.Key).ToList().Count > 0)
                {
                    throw new Exception("Aynı anahtar kullanılamaz!");
                }
                poll.Voter.Add(voter);
                Managers.clientManager.AddVoter(poll.Urls, poll.PollingName, poll.Voter.GetList(), voter);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PAdd(PostVoterSetting voterSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            poll.Voter.Add(voterSetting.Voter);
            try
            {
                for (int i = 0; i < voterSetting.Voters.Count; i++)
                {
                    if (!VoterEquivocation(voterSetting.Voters[i], poll.Voter.Get(i)))
                    {
                        poll.Voter.Delete(voterSetting.Voter);
                        return false;
                    }
                }
                if (poll.Voter.GetList().Where(x => x.Key == voterSetting.Voter.Key).ToList().Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Edit(Poll poll, Voter voter, Voter editVoter)
        {
            ValidationResult result = validations.Validate(editVoter);
            if (result.IsValid)
            {
                SwapVoter(voter, editVoter);
                Managers.clientManager.EditVoter(poll.Urls, poll.PollingName, poll.Voter.GetList(), voter);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PEdit(PostVoterSetting voterSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Voter> voters = new List<Voter>();
            voters.AddRange(poll.Voter.GetList());
            Voter voter = voters.FirstOrDefault(x => x.Index == voterSetting.Voter.Index);
            SwapVoter(voter, voterSetting.Voter);
            try
            {
                for (int i = 0; i < voterSetting.Voters.Count; i++)
                {
                    if (!VoterEquivocation(voterSetting.Voters[i], voters[i]))
                    {
                        return false;
                    }
                }
                SwapVoter(voters.FirstOrDefault(x => x.Index == voterSetting.Voter.Index), voter);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(Voter voter, Poll poll)
        {
            try
            {
                poll.Voter.Delete(voter);
                Managers.clientManager.DeleteVoter(poll.Urls, poll.PollingName, poll.Voter.GetList(), voter);
            }
            catch
            {
                throw new Exception("Silme başarısız!");
            }
        }
        public void Delete(int index, Poll poll)
        {
            try
            {
                Voter voter = poll.Voter.Get(index);
                poll.Voter.Delete(index);
                Managers.clientManager.DeleteVoter(poll.Urls, poll.PollingName, poll.Voter.GetList(), voter);
            }
            catch
            {
                throw new Exception("Silme başarısız!");
            }
        }
        public bool P2PDelete(PostVoterSetting voterSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Voter> voters = new List<Voter>();
            voters.AddRange(poll.Voter.GetList());
            voters.Remove(voters.FirstOrDefault(x => x.Index == voterSetting.Voter.Index));
            try
            {
                for (int i = 0; i < voterSetting.Voters.Count; i++)
                {
                    if (!VoterEquivocation(voterSetting.Voters[i], voters[i]))
                    {
                        return false;
                    }
                }
                poll.Voter.Delete(poll.Voter.GetList().FirstOrDefault(x => x.Index == voterSetting.Voter.Index));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void P2PRefreshList(IList<Voter> voters, string pollName)
        {
            PollingList.dbPoll.Search(pollName).Voter.SetList(voters);
        }
        public int LastIndex(Poll poll)
        {
            if (poll.Voter.Count == 0)
                return 0;
            return poll.Voter.GetIndex(poll.Voter.Count - 1);
        }
        public void VoterLogin(string key, string password, BlockChainsData blockChainsData)
        {
            int index;
            if ((index = GetBlockChainVoterIndex(key, password, blockChainsData.BlockChainForVoters.GetBlocks())) != -1)
            {
                if (IVoted(blockChainsData, index))
                    Managers.clientManager.UseVoter(blockChainsData.Urls, blockChainsData.BlockChainForPollName, index);
                else
                    throw new Exception("Bu seçmen oy kullanmış");
            }
            else
            {
                throw new Exception("Böyle bir şeçmen yok!");
            }
        }
        private bool IVoted(BlockChainsData blockChainsData, int index)
        {
            foreach (Block<int> block in blockChainsData.UsedVoter.GetBlocks())
            {
                if (block.Index != 0 && block.Transactions == index)
                    return false;
            }
            return true;
        }
        private int GetBlockChainVoterIndex(string key, string password, IList<Block<Voter>> blocks)
        {
            for (int i = 1; i < blocks.Count; i++)
            {
                if (blocks[i].Transactions.Key == key && blocks[i].Transactions.Password == password)
                    return blocks[i].Index;
            }
            return -1;
        }
        private void SwapVoter(Voter voter1, Voter voter2)
        {
            voter1.Key = voter2.Key;
            voter1.Name = voter2.Name;
            voter1.Password = voter2.Password;
            voter1.Surname = voter2.Surname;
            voter1.AddedAdminKey = CurrentAdmin.AdminKey;
        }
        private bool VoterEquivocation(Voter voter1, Voter voter2)
        {
            try
            {
                return (voter1.Index == voter2.Index &&
                      voter1.Key == voter2.Key &&
                      voter1.Name == voter2.Name &&
                      voter1.Password == voter2.Password &&
                      voter1.AddedAdminKey == voter2.AddedAdminKey &&
                      voter1.Surname == voter2.Surname);
            }
            catch
            {
                return false;
            }
        }
    }
}
