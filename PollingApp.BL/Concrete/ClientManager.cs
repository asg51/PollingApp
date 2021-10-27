using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PollingApp.Entities.P2PModel;
using PollingApp.BL.ValidationRules;
using FluentValidation.Results;
using PollingApp.Entities;
using WebSocketSharp;
using Newtonsoft.Json;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.Contcat
{
    public delegate void ClientControl(BlockChainsData blockChainsData, int index);
    public class ClientManager
    {
        public event ClientControl Login;
        public event ClientControl NotLogin;
        public event ClientControl Voted;
        public event ClientControl NotVoted;

        ConnectAsAdminValidation validations;
        IList<Tuple<IList<Tuple<string, bool>>, int>> AdminAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int>> ChosenAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int>> VoterAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int, string, string>> PollAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int>> BlockChainAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int, int>> UseVoterAddListControl;
        IList<Tuple<IList<Tuple<string, bool>>, int>> VotingAddListControl;
        int Adminindex = 0;
        int Chosenindex = 0;
        int Voterindex = 0;
        int Pollindex = 0;
        int Blockindex = 0;
        int UseVoterindex = 0;
        int Votingindex = 0;
        public ClientManager()
        {
            AdminAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int>>();
            ChosenAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int>>();
            VoterAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int>>();
            PollAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int, string, string>>();
            BlockChainAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int>>();
            UseVoterAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int, int>>();
            VotingAddListControl = new List<Tuple<IList<Tuple<string, bool>>, int>>();

            validations = new ConnectAsAdminValidation();
        }
        public void Connect(ConnectAsAdmin connectAsAdmin)
        {
            if (PollingList.blockChainList.GetBlockChains().Where(x => x.BlockChainForPollName == connectAsAdmin.Name).ToList().Count == 0 &&
               PollingList.dbPoll.GetList().Where(x => x.PollingName == connectAsAdmin.Name).ToList().Count == 0)
            {
                ValidationResult result = validations.Validate(connectAsAdmin);
                if (result.IsValid)
                {
                    string dnsString = connectAsAdmin.IP;
                    connectAsAdmin.IP = P2PContext.IP;

                    P2PContext.client.Connect(connectAsAdmin, dnsString);
                }
                else
                {
                    throw new Exception(result.Errors[0].ErrorMessage);
                }
            }
            else
            {
                throw new Exception("Böyle bir seçim bulmaktadır!");
            }
        }
        public void DeletePoll(IList<string> urls, string pollName)
        {
            BaseModel modelBase = new BaseModel("delete poll", new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key,
                UserAdmin.Password, pollName), pollName);
            P2PContext.client.Send(urls, JsonConvert.SerializeObject(modelBase), pollName);
        }
        public void ExitSystemPoll(IList<string> urls, string pollName)
        {
            BaseModel modelBase = new BaseModel("exit system poll", new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key,
                UserAdmin.Password, pollName), pollName);
            P2PContext.client.Send(urls, JsonConvert.SerializeObject(modelBase), pollName);
        }
        public void DeleteBlockChain(IList<string> urls, string pollName)
        {
            BaseModel modelBase = new BaseModel("delete blockchain", new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key,
                UserAdmin.Password, pollName), pollName);
            P2PContext.client.Send(urls, JsonConvert.SerializeObject(modelBase), pollName);
        }
        public void ExitSystemBlockChain(IList<string> urls, string pollName)
        {
            BaseModel modelBase = new BaseModel("exit system blockchain", new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key,
                UserAdmin.Password, pollName), pollName);
            P2PContext.client.Send(urls, JsonConvert.SerializeObject(modelBase), pollName);
        }

        public void AddAdmin(IList<string> urls, string pollName, IList<Admin> admins, Admin admin)
        {
            AdminAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Adminindex));
            string data = JsonConvert.SerializeObject(new BaseModel("add admin",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostAdminSetting(admins, admin, Adminindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void DeleteAdmin(IList<string> urls, string pollName, IList<Admin> admins, Admin admin)
        {
            AdminAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Adminindex));
            string data = JsonConvert.SerializeObject(new BaseModel("delete admin",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostAdminSetting(admins, admin, Adminindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void EditAdmin(IList<string> urls, string pollName, IList<Admin> admins, Admin admin)
        {
            AdminAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Adminindex));
            string data = JsonConvert.SerializeObject(new BaseModel("edit admin",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostAdminSetting(admins, admin, Adminindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void GetCorrectAdminData(string pollName, IList<Admin> admins)
        {
            PollingList.dbPoll.Search(pollName).Admins.SetList(admins);
        }
        public void AddAdminState(ConnectAsAdmin connectAsAdmin, GetStateSetting adminSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int> list in AdminAddListControl)
            {
                if (list.Item2 == adminSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, adminSetting.State));
                    if (list.Item1.Count == PollingList.dbPoll.Search(connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect admin data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.dbPoll.Search(connectAsAdmin.Name).Admins.GetList()));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct admin data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }
                        }
                        AdminAddListControl.Remove(AdminAddListControl.FirstOrDefault(x => x.Item2 == adminSetting.Index));
                    }
                    break;
                }
            }
        }
        public void AddChosen(IList<string> urls, string pollName, IList<Chosen> chosens, Chosen chosen)
        {
            ChosenAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Chosenindex));
            string data = JsonConvert.SerializeObject(new BaseModel("add chosen",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostChosenSetting(chosens, chosen, Chosenindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void DeleteChosen(IList<string> urls, string pollName, IList<Chosen> chosens, Chosen chosen)
        {
            ChosenAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Chosenindex));
            string data = JsonConvert.SerializeObject(new BaseModel("delete chosen",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostChosenSetting(chosens, chosen, Chosenindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void EditChosen(IList<string> urls, string pollName, IList<Chosen> chosens, Chosen chosen)
        {
            AdminAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Chosenindex));
            string data = JsonConvert.SerializeObject(new BaseModel("edit chosen",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostChosenSetting(chosens, chosen, Chosenindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void GetCorrectChosenData(string pollName, IList<Chosen> chosens)
        {
            PollingList.dbPoll.Search(pollName).Chosen.SetList(chosens);
        }
        public void AddChosenState(ConnectAsAdmin connectAsAdmin, GetStateSetting chosenSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int> list in ChosenAddListControl)
            {
                if (list.Item2 == chosenSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, chosenSetting.State));
                    if (list.Item1.Count == PollingList.dbPoll.Search(connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect chosen data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.dbPoll.Search(connectAsAdmin.Name).Admins.GetList()));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct chosen data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }
                        }
                        ChosenAddListControl.Remove(ChosenAddListControl.FirstOrDefault(x => x.Item2 == chosenSetting.Index));
                    }
                    break;
                }
            }
        }
        public void AddVoter(IList<string> urls, string pollName, IList<Voter> voters, Voter voter)
        {
            VoterAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Voterindex));
            string data = JsonConvert.SerializeObject(new BaseModel("add voter",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostVoterSetting(voters, voter, Voterindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void DeleteVoter(IList<string> urls, string pollName, IList<Voter> voters, Voter voter)
        {
            VoterAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Voterindex));
            string data = JsonConvert.SerializeObject(new BaseModel("delete voter",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostVoterSetting(voters, voter, Voterindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void EditVoter(IList<string> urls, string pollName, IList<Voter> voters, Voter voter)
        {
            VoterAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Voterindex));
            string data = JsonConvert.SerializeObject(new BaseModel("edit voter",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostVoterSetting(voters, voter, Voterindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void GetCorrectVoterData(string pollName, IList<Voter> voters)
        {
            PollingList.dbPoll.Search(pollName).Voter.SetList(voters);
        }
        public void AddVoterState(ConnectAsAdmin connectAsAdmin, GetStateSetting voterSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int> list in VoterAddListControl)
            {
                if (list.Item2 == voterSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, voterSetting.State));
                    if (list.Item1.Count == PollingList.dbPoll.Search(connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect voter data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.dbPoll.Search(connectAsAdmin.Name).Admins.GetList()));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct voter data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }
                        }
                        VoterAddListControl.Remove(VoterAddListControl.FirstOrDefault(x => x.Item2 == voterSetting.Index));
                    }
                    break;
                }
            }
        }
        public void EditPoll(IList<string> urls, string pollName, string newPollName)
        {
            PollAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int, string, string>(new List<Tuple<string, bool>>(), ++Pollindex, pollName, newPollName));
            string data = JsonConvert.SerializeObject(new BaseModel("edit poll",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostPollSetting(pollName, newPollName, Pollindex)));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void BlockChainStart(IList<string> urls, string pollName)
        {
            VoterAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Pollindex));
            string data = JsonConvert.SerializeObject(new BaseModel("blockChainStart",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                pollName));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void AddPollState(ConnectAsAdmin connectAsAdmin, GetStateSetting pollSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int, string, string> list in PollAddListControl)
            {
                if (list.Item2 == pollSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, pollSetting.State));
                    if (list.Item1.Count == PollingList.dbPoll.Search(connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter == list.Item1.Count - 1)
                        {
                            PollingList.dbPoll.Search(list.Item3).PollingName = list.Item4;
                            string data = JsonConvert.SerializeObject(new BaseModel("set correct poll name",
                               new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                               connectAsAdmin.Name)
                               , list.Item4));

                            P2PContext.client.Send(PollingList.dbPoll.Search(list.Item4).Urls,
                               data, connectAsAdmin.Name);
                        }
                        PollAddListControl.Remove(PollAddListControl.FirstOrDefault(x => x.Item2 == pollSetting.Index));
                    }
                    break;
                }
            }
        }
        public void AddBlockChainList(IList<string> urls, string pollName, BlockChainsData blockChainsData)
        {
            AdminAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Blockindex));
            string data = JsonConvert.SerializeObject(new BaseModel("poll transformed blockchain",
                new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
                new PostBlockChainsDataSetting(urls, blockChainsData.BlockChainForVoters.GetBlocks(),
                blockChainsData.BlockChainForChosens.GetBlocks(), blockChainsData.BlockChainForAdmins.GetBlocks(),
                blockChainsData.BlockChainForPollName, blockChainsData.PollTime.StartTime,
                blockChainsData.PollTime.FinishTime, blockChainsData.Index, Blockindex)), Formatting.Indented);
            P2PContext.client.Send(urls, data, pollName);
        }
        public void AddStartBlockChainListState(ConnectAsAdmin connectAsAdmin, GetStateSetting blockChainSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int> list in BlockChainAddListControl)
            {
                if (list.Item2 == blockChainSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, blockChainSetting.State));
                    if (list.Item1.Count == PollingList.dbPoll.Search(connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect poll blockchain data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name)));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct poll blockchain data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }
                        }
                        BlockChainAddListControl.Remove(BlockChainAddListControl.FirstOrDefault(x => x.Item2 == blockChainSetting.Index));
                    }
                    break;
                }
            }
        }
        public void UseVoter(IList<string> urls, string pollName, int index)
        {
            UseVoterAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int, int>(new List<Tuple<string, bool>>(), ++UseVoterindex, index));
            string data = JsonConvert.SerializeObject(new BaseModel("use voter",
               new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password, pollName),
               index.ToString() + "-" + UseVoterindex.ToString()));
            P2PContext.client.Send(urls, data, pollName);
        }
        public void AddUseVoterListState(ConnectAsAdmin connectAsAdmin, GetStateSetting useVoterSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int, int> list in UseVoterAddListControl)
            {
                if (list.Item2 == useVoterSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, useVoterSetting.State));
                    if (list.Item1.Count == PollingList.blockChainList.GetBlockChains().
                        FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect use voter data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name)));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }
                            if (Login != null)
                                Login(PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name),
                                            list.Item3);
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct use voter data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }
                            if (NotLogin != null)
                                NotLogin(PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name),
                                           list.Item3);
                        }
                        UseVoterAddListControl.Remove(UseVoterAddListControl.FirstOrDefault(x => x.Item2 == useVoterSetting.Index));
                    }
                    break;
                }
            }
        }
        public void Voting(string pollName, int voter, int chosen,
            IList<Block<int>> voterBlocks, IList<Block<int>> choesenBlocks, IList<int> useVoter)
        {
            VotingAddListControl.Add(new Tuple<IList<Tuple<string, bool>>, int>(new List<Tuple<string, bool>>(), ++Votingindex));
            BlockChainsData blockChainsData = PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == pollName);
            string data = JsonConvert.SerializeObject(new BaseModel("voting", new ConnectAsAdmin(P2PContext.IP, null, null, pollName),
                new PostVoting(chosen, voter, choesenBlocks, voterBlocks, useVoter, Votingindex)));
            P2PContext.client.Send(blockChainsData.Urls, data, pollName);
        }
        public void AddVotingListState(ConnectAsAdmin connectAsAdmin, GetStateSetting votingListSetting)
        {
            foreach (Tuple<IList<Tuple<string, bool>>, int> list in VotingAddListControl)
            {
                if (list.Item2 == votingListSetting.Index)
                {
                    list.Item1.Add(new Tuple<string, bool>(connectAsAdmin.IP, votingListSetting.State));
                    if (list.Item1.Count == PollingList.blockChainList.GetBlockChains().
                        FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name).Urls.Count)
                    {
                        int counter = 0;
                        foreach (Tuple<string, bool> state in list.Item1)
                        {
                            if (state.Item2)
                                counter++;
                        }
                        if (counter > list.Item1.Count * 50 / 100)
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("incorrect use voter data refresh",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name)));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                }
                            }

                            if (Voted != null)
                            {
                                Voted(PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name),
                                            list.Item2);
                            }
                        }
                        else
                        {
                            foreach (Tuple<string, bool> state in list.Item1)
                            {
                                if (!state.Item2)
                                {
                                    string data = JsonConvert.SerializeObject(new BaseModel("get correct use voter data",
                                       new ConnectAsAdmin(P2PContext.server.GetIpAddress(), UserAdmin.Key, UserAdmin.Password,
                                       connectAsAdmin.Name)
                                       , null));

                                    P2PContext.client.Send(state.Item1, data, connectAsAdmin.Name);
                                    break;
                                }
                            }

                            if (NotVoted != null)
                            {
                                NotVoted(PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name),
                                           list.Item2);
                            }
                        }
                        VotingAddListControl.Remove(VotingAddListControl.FirstOrDefault(x => x.Item2 == votingListSetting.Index));
                    }
                    break;
                }
            }
        }
    }
}
