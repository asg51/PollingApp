using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollingApp.Entities;
using PollingApp.BL.ValidationRules;
using FluentValidation.Results;
using PollingApp.Entities.P2PModel;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.Contcat
{
    public class ChosenManager
    {
        ChosenValidation validations;
        public ChosenManager()
        {
            validations = new ChosenValidation();
        }
        public void Add(Chosen chosen, Poll poll)
        {
            ValidationResult result = validations.Validate(chosen);
            if (result.IsValid)
            {
                if (poll.Chosen.GetList().Where(x => x.ChosenName == chosen.ChosenName).ToList().Count > 0)
                {
                    throw new Exception("Aynı isim kullanılamaz!");
                }

                poll.Chosen.Add(chosen);
                Managers.clientManager.AddChosen(poll.Urls, poll.PollingName, poll.Chosen.GetList(), chosen);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PAdd(PostChosenSetting chosenSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            poll.Chosen.Add(chosenSetting.Chosen);
            try
            {
                for (int i = 0; i < chosenSetting.Chosens.Count; i++)
                {
                    if (!ChosenEquivocation(chosenSetting.Chosens[i], poll.Chosen.Get(i)))
                    {
                        poll.Chosen.Delete(chosenSetting.Chosen);
                        return false;
                    }
                }
                if (poll.Chosen.GetList().Where(x => x.ChosenName == chosenSetting.Chosen.ChosenName).ToList().Count > 0)
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
        public void Edit(Poll poll, Chosen chosen, Chosen editChosen)
        {
            ValidationResult result = validations.Validate(editChosen);
            if (result.IsValid)
            {
                SwapChosen(chosen, editChosen);
                Managers.clientManager.EditChosen(poll.Urls, poll.PollingName, poll.Chosen.GetList(), chosen);
            }
            else
            {
                throw new Exception(result.Errors[0].ErrorMessage);
            }
        }
        public bool P2PEdit(PostChosenSetting chosenSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Chosen> chosens = new List<Chosen>();
            chosens.AddRange(poll.Chosen.GetList());
            Chosen chosen = chosens.FirstOrDefault(x => x.Index == chosenSetting.Chosen.Index);
            SwapChosen(chosen, chosenSetting.Chosen);
            try
            {
                for (int i = 0; i < chosenSetting.Chosens.Count; i++)
                {
                    if (!ChosenEquivocation(chosenSetting.Chosens[i], chosens[i]))
                    {
                        return false;
                    }
                }
                SwapChosen(chosens.FirstOrDefault(x => x.Index == chosenSetting.Chosen.Index), chosen);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(Chosen chosen, Poll poll)
        {
            try
            {
                poll.Chosen.Delete(chosen);
                Managers.clientManager.DeleteChosen(poll.Urls, poll.PollingName, poll.Chosen.GetList(), chosen);
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
                poll.Chosen.Delete(index);
                Managers.clientManager.DeleteChosen(poll.Urls, poll.PollingName, poll.Chosen.GetList(), poll.Chosen.Get(index));
            }
            catch
            {
                throw new Exception("Silme başarısız!");
            }
        }
        public bool P2PDelete(PostChosenSetting chosenSetting, string pollName)
        {
            Poll poll = PollingList.dbPoll.Search(pollName);
            List<Chosen> chosens = new List<Chosen>();
            chosens.AddRange(poll.Chosen.GetList());
            chosens.Remove(chosens.FirstOrDefault(x => x.Index == chosenSetting.Chosen.Index));
            try
            {
                for (int i = 0; i < chosenSetting.Chosens.Count; i++)
                {
                    if (!ChosenEquivocation(chosenSetting.Chosens[i], chosens[i]))
                    {
                        return false;
                    }
                }
                poll.Chosen.Delete(poll.Chosen.GetList().FirstOrDefault(x => x.Index == chosenSetting.Chosen.Index));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void P2PRefreshList(IList<Chosen> chosens, string pollName)
        {
            PollingList.dbPoll.Search(pollName).Chosen.SetList(chosens);
        }
        public int LastIndex(Poll poll)
        {
            if (poll.Chosen.Count == 0)
                return 0;
            return poll.Chosen.GetIndex(poll.Chosen.Count - 1);
        }
        public bool EnoughVoters(Poll poll) => poll.Chosen.Count > 1;

        private void SwapChosen(Chosen chosen1, Chosen chosen2)
        {
            chosen1.ChosenName = chosen2.ChosenName;
        }
        private bool ChosenEquivocation(Chosen chosen1, Chosen chosen2)
        {
            try
            {
                return (chosen1.Index == chosen2.Index &&
                      chosen1.ChosenName == chosen2.ChosenName);
            }
            catch
            {
                return false;
            }
        }
    }
}
