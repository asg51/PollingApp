using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using PollingApp.Entities.P2PModel;
using PollingApp.Entities;
using Newtonsoft.Json;
using System.Net;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.P2P
{
    public class Server : WebSocketBehavior
    {
        private Dictionary<string, Action<ConnectAsAdmin, object>> keyValuePairs;
        private WebSocketServer wss = null;
        public Server()
        {
            keyValuePairs = new Dictionary<string, Action<ConnectAsAdmin, object>>()
            {
                {"Hello Server", new Action<ConnectAsAdmin,object>(ActionHelloServer) },
                {"admin login data poll", new Action<ConnectAsAdmin,object>(ActionOnMessagePoll) },
                {"admin login data blockchain", new Action<ConnectAsAdmin,object>(ActionOnMessageBlockChain)},
                {"admin login",new Action<ConnectAsAdmin,object>(ActionConnectAsAdmin) },

                {"add admin",new Action<ConnectAsAdmin,object>(ActionAddAdmin) },
                {"delete admin",new Action<ConnectAsAdmin,object>(ActionDeleteAdmin) },
                {"edit admin",new Action<ConnectAsAdmin,object>(ActionEditAdmin) },
                {"incorrect admin data refresh",new Action<ConnectAsAdmin,object>(ActionRefreshAddAdmin) },
                {"get correct admin data",new Action<ConnectAsAdmin,object>(ActionGetCorrectAdminData) },

                {"add chosen",new Action<ConnectAsAdmin,object>(ActionAddChosen) },
                {"delete chosen",new Action<ConnectAsAdmin,object>(ActionDeleteChosen) },
                {"edit chosen",new Action<ConnectAsAdmin,object>(ActionEditChosen) },
                {"incorrect chosen data refresh",new Action<ConnectAsAdmin,object>(ActionRefreshAddChosen) },
                {"get correct chosen data",new Action<ConnectAsAdmin,object>(ActionGetCorrectChosenData) },

                {"add voter",new Action<ConnectAsAdmin,object>(ActionAddVoter) },
                {"delete voter",new Action<ConnectAsAdmin,object>(ActionDeleteVoter) },
                {"edit voter",new Action<ConnectAsAdmin,object>(ActionEditVoter) },
                {"incorrect voter data refresh",new Action<ConnectAsAdmin,object>(ActionRefreshAddVoter) },
                {"get correct voter data",new Action<ConnectAsAdmin,object>(ActionGetCorrectVoterData) },

                {"edit poll",new Action<ConnectAsAdmin,object>(ActionEditPoll) },
                {"set correct poll name",new Action<ConnectAsAdmin,object>(ActionSetCorrectPollName) },
                {"blockChainStart",new Action<ConnectAsAdmin,object>(ActionBlockChainStart) },
                {"poll transformed blockchain",new Action<ConnectAsAdmin,object>(ActionPollTransformedBlockchain) },
                {"incorrect poll blockchain data refresh",new Action<ConnectAsAdmin,object>(ActionIncorrectPollBlockchainDataRefresh) },
                {"get correct poll blockchain data",new Action<ConnectAsAdmin,object>(ActionGetCorrectPollBlockChainData) },
                {"delete poll",new Action<ConnectAsAdmin,object>(ActionDeletePoll) },
                {"exit system poll",new Action<ConnectAsAdmin,object>(ActionExitSystemPoll) },
                {"delete blockchain",new Action<ConnectAsAdmin,object>(ActionDeleteBlockChain) },
                {"exit system blockchain",new Action<ConnectAsAdmin,object>(ActionExitSystemBlockChain) },

                {"get poll",new Action<ConnectAsAdmin,object>(ActionGetPoll) },
                {"use voter",new Action<ConnectAsAdmin,object>(ActionUseVoter) },
                {"incorrect use voter data refresh",new Action<ConnectAsAdmin,object>(ActionIncorrectUseVoterDataRefresh) },
                {"get correct use voter data",new Action<ConnectAsAdmin,object>(ActionGetCorrectUseVoterData) },
                {"voting",new Action<ConnectAsAdmin,object>(ActionVoting) }
            };
        }
        public void Start()
        {
            while (true)
            {
                try
                {
                    int port = new Random().Next(0, 1000);
                    if (P2PContext.IP == string.Empty)
                        wss = new WebSocketServer($"ws://127.0.0.1:{port}");
                    else
                        wss = new WebSocketServer(P2PContext.IP);
                    wss.AddWebSocketService<Server>("/Blockchain");
                    wss.WaitTime = new TimeSpan(1, 0, 0, 0);
                    wss.Start();
                    P2PContext.IP = wss.Address.ToString() + ":" + wss.Port;
                    break;
                }
                catch
                {

                }
            }
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            lock (PollingList._lockObject)
            {
                BaseModel baseModel = JsonConvert.DeserializeObject<BaseModel>(e.Data);
                keyValuePairs.FirstOrDefault(x => x.Key == baseModel.EventToBeHeld).Value.Invoke(
                    baseModel.ConnectAsAdmin, baseModel.IncomingModel);
            }
        }

        public string GetIpAddress()
        {
            return P2PContext.IP;
        }
        private void ActionHelloServer(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            try
            {
                var url = PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == baseModel.ToString())
                      .Urls.FirstOrDefault(x => x == connectAsAdmin.IP);
                if (url == null)
                {
                    PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == baseModel.ToString())
                    .Urls.Add(connectAsAdmin.IP);
                }
            }
            catch
            {

            }
            try
            {
                var url = PollingList.dbPoll.GetList().FirstOrDefault(x => x.PollingName == baseModel.ToString()).Urls.FirstOrDefault(x => x == connectAsAdmin.IP);
                if (url == null)
                {
                    PollingList.dbPoll.GetList().FirstOrDefault(x => x.PollingName == baseModel.ToString()).Urls.Add(connectAsAdmin.IP);
                }
            }
            catch
            {

            }

            Send(JsonConvert.SerializeObject(new BaseModel("Hello Client", null, null)));
        }
        private void ActionConnectAsAdmin(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            string WhichOne = string.Empty;
            object obj = Managers.serverManager.AdminControl(connectAsAdmin, ref WhichOne);

            if (obj != null)
            {
                string eventToBeHeld = WhichOne == "Poll"
                       ? "admin login poll post"
                       : "admin login blockChain post";

                string data = JsonConvert.SerializeObject(new BaseModel(eventToBeHeld,
                    new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name), obj));
                Send(data);
                P2PContext.client.Connect(connectAsAdmin.IP, connectAsAdmin.Name);
            }
            else
            {
                string data = JsonConvert.SerializeObject(new BaseModel("selection not found",
                    new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name), null));
                Send(data);
            }
        }

        private void ActionOnMessagePoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Poll poll = JsonConvert.DeserializeObject<Poll>(baseModel.ToString());
            Managers.pollManager.P2PAdd(poll);
        }
        private void ActionOnMessageBlockChain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BlockChainsData blockChainsData =
                JsonConvert.DeserializeObject<BlockChainsData>(baseModel.ToString());
            Managers.blockChainManager.AddFromServer(ref blockChainsData);
        }
        private void ActionAddAdmin(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostAdminSetting adminSetting =
                JsonConvert.DeserializeObject<PostAdminSetting>(baseModel.ToString());
            bool state = Managers.adminManager.P2PAdd(adminSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add admin state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, adminSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }

        private void ActionDeleteAdmin(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostAdminSetting adminSetting =
                JsonConvert.DeserializeObject<PostAdminSetting>(baseModel.ToString());
            bool state = Managers.adminManager.P2PDelete(adminSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add admin state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, adminSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionEditAdmin(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostAdminSetting adminSetting =
                JsonConvert.DeserializeObject<PostAdminSetting>(baseModel.ToString());
            bool state = Managers.adminManager.P2PEdit(adminSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add admin state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, adminSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionRefreshAddAdmin(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Admin> admins = JsonConvert.DeserializeObject<IList<Admin>>(baseModel.ToString());
            Managers.adminManager.P2PRefreshList(admins, connectAsAdmin.Name);
        }
        private void ActionGetCorrectAdminData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = new BaseModel("post correct admin data",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                PollingList.dbPoll.Search(connectAsAdmin.Name).Admins.GetList());
            Send(JsonConvert.SerializeObject(modelbase));
        }

        private void ActionAddChosen(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostChosenSetting chosenSetting =
                JsonConvert.DeserializeObject<PostChosenSetting>(baseModel.ToString());
            bool state = Managers.chosenManager.P2PAdd(chosenSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add chosen state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, chosenSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }

        private void ActionDeleteChosen(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostChosenSetting chosenSetting =
                JsonConvert.DeserializeObject<PostChosenSetting>(baseModel.ToString());
            bool state = Managers.chosenManager.P2PDelete(chosenSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add chosen state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, chosenSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionEditChosen(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostChosenSetting chosenSetting =
                JsonConvert.DeserializeObject<PostChosenSetting>(baseModel.ToString());
            bool state = Managers.chosenManager.P2PEdit(chosenSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add chosen state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, chosenSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionRefreshAddChosen(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Chosen> chosens = JsonConvert.DeserializeObject<IList<Chosen>>(baseModel.ToString());
            Managers.chosenManager.P2PRefreshList(chosens, connectAsAdmin.Name);
        }
        private void ActionGetCorrectChosenData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = new BaseModel("post correct chosen data",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                PollingList.dbPoll.Search(connectAsAdmin.Name).Chosen.GetList());
            Send(JsonConvert.SerializeObject(modelbase));
        }

        private void ActionAddVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostVoterSetting voterSetting =
                JsonConvert.DeserializeObject<PostVoterSetting>(baseModel.ToString());
            bool state = Managers.voterManager.P2PAdd(voterSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add voter state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, voterSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }

        private void ActionDeleteVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostVoterSetting voterSetting =
               JsonConvert.DeserializeObject<PostVoterSetting>(baseModel.ToString());
            bool state = Managers.voterManager.P2PDelete(voterSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add voter state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, voterSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionEditVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostVoterSetting voterSetting =
               JsonConvert.DeserializeObject<PostVoterSetting>(baseModel.ToString());
            bool state = Managers.voterManager.P2PEdit(voterSetting, connectAsAdmin.Name);

            BaseModel PostBaseModel = new BaseModel("add voter state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, voterSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionRefreshAddVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Voter> voters = JsonConvert.DeserializeObject<IList<Voter>>(baseModel.ToString());
            Managers.voterManager.P2PRefreshList(voters, connectAsAdmin.Name);
        }
        private void ActionGetCorrectVoterData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = new BaseModel("post correct voter data",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                PollingList.dbPoll.Search(connectAsAdmin.Name).Chosen.GetList());
            Send(JsonConvert.SerializeObject(modelbase));
        }

        private void ActionEditPoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostPollSetting pollSetting =
               JsonConvert.DeserializeObject<PostPollSetting>(baseModel.ToString());
            bool state = Managers.pollManager.P2PEdit(pollSetting.PollName);

            BaseModel PostBaseModel = new BaseModel("add poll state",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new GetStateSetting(state, pollSetting.Index));
            Send(JsonConvert.SerializeObject(PostBaseModel));
        }
        private void ActionSetCorrectPollName(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            string newPollName = baseModel.ToString();

            Managers.pollManager.P2PSetPollName(connectAsAdmin.Name, newPollName);
        }
        private void ActionBlockChainStart(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            string pollname = baseModel.ToString();

            Managers.pollManager.P2PBlockChainStart(pollname);

            Send(JsonConvert.SerializeObject(new BaseModel("blockChainStart completed",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                null)));
        }
        private void ActionPollTransformedBlockchain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostBlockChainsDataSetting blockChainsDataSetting = JsonConvert.DeserializeObject<PostBlockChainsDataSetting>(baseModel.ToString());
            BlockChainsData blockChainsData;
            Poll poll;
            bool state = Managers.pollManager.P2PAddBlockChainList(blockChainsDataSetting, out blockChainsData, out poll);
            BaseModel modelbase = new BaseModel("blockChainStart state",
            new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
            new GetStateSetting(state, blockChainsDataSetting.BlockIndex));
            Send(JsonConvert.SerializeObject(modelbase));
            if (state)
            {
                PollingList.dbPoll.Delete(poll);
                blockChainsData.Urls = poll.Urls;
                PollingList.blockChainList.Add(blockChainsData);
            }

        }
        private void ActionIncorrectPollBlockchainDataRefresh(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BlockChainsData blockChainsData = JsonConvert.DeserializeObject<BlockChainsData>(baseModel.ToString());

            Managers.blockChainManager.P2PRefreshList(blockChainsData, connectAsAdmin.Name);
        }
        private void ActionGetCorrectPollBlockChainData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Poll poll = PollingList.dbPoll.Search(connectAsAdmin.Name);

            string data = JsonConvert.SerializeObject(new BaseModel("post correct poll blockchain data",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                new PostPoll(poll.Urls, poll.PollingName, -1, poll.PollTime, poll.Admins.GetList(),
                poll.Voter.GetList(), poll.Chosen.GetList())));

            Send(data);
        }
        private void ActionDeletePoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.serverManager.DeletePoll(baseModel.ToString());

            string data = JsonConvert.SerializeObject(new BaseModel("completed delete poll",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                connectAsAdmin.Name));

            Send(data);
        }
        private void ActionExitSystemPoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.serverManager.ExitSystemPoll(baseModel.ToString(), connectAsAdmin.IP);

            string data = JsonConvert.SerializeObject(new BaseModel("completed delete poll",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                connectAsAdmin.Name));

            Send(data);
        }
        private void ActionDeleteBlockChain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.serverManager.DeleteBlockChain(baseModel.ToString());

            string data = JsonConvert.SerializeObject(new BaseModel("completed delete blockchain",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                connectAsAdmin.Name));

            Send(data);
        }
        private void ActionExitSystemBlockChain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.serverManager.ExitSystemBlockChain(baseModel.ToString(), connectAsAdmin.IP);

            string data = JsonConvert.SerializeObject(new BaseModel("completed delete blockchain",
                new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
                connectAsAdmin.Name));

            Send(data);
        }
        private void ActionGetPoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BlockChainsData blockChainsData;
            if ((blockChainsData = Managers.serverManager.GetPoll(baseModel.ToString())) != null)
            {
                string data = JsonConvert.SerializeObject(new BaseModel("post poll",
               new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
               blockChainsData));

                blockChainsData.Urls.Add(connectAsAdmin.IP);
                P2PContext.client.Connect(connectAsAdmin.IP, connectAsAdmin.Name);

                Send(data);
            }
            else
            {
                string data = JsonConvert.SerializeObject(new BaseModel("post poll not found",
               new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
               null));

                blockChainsData.Urls.Add(connectAsAdmin.IP);
                P2PContext.client.Connect(connectAsAdmin.IP, connectAsAdmin.Name);
                Send(data);
            }
        }
        private void ActionUseVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            bool state = Managers.serverManager.GetUseVoter(connectAsAdmin.Name, int.Parse(baseModel.ToString().Split('-')[0]));

            string data = JsonConvert.SerializeObject(new BaseModel("state use voter",
           new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
           new GetStateSetting(state, int.Parse(baseModel.ToString().Split('-')[1]))));

            Send(data);
        }
        private void ActionIncorrectUseVoterDataRefresh(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.serverManager.IncorrectUseVoterDataRefresh(connectAsAdmin.Name, JsonConvert.DeserializeObject<IList<int>>(baseModel.ToString()));

            string data = JsonConvert.SerializeObject(new BaseModel("completed use voter",
           new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name), null));

            Send(data);
        }
        private void ActionGetCorrectUseVoterData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            string data = JsonConvert.SerializeObject(new BaseModel("post correct use voter data",
             new ConnectAsAdmin(GetIpAddress(), UserAdmin.Key, UserAdmin.Password, connectAsAdmin.Name),
            PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name).UseVoter));

            Send(data);
        }
        private void ActionVoting(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PostVoting postVoting = JsonConvert.DeserializeObject<PostVoting>(baseModel.ToString());

            bool state = Managers.blockChainManager.P2PVoting(connectAsAdmin.Name, postVoting.VoterIndex, postVoting.ChosenIndex, postVoting.VoterBlocks,
                 postVoting.ChosenBlocks, postVoting.UseVoter);

            string data = JsonConvert.SerializeObject(new BaseModel("state voting",
                new ConnectAsAdmin(GetIpAddress(), null, null, connectAsAdmin.Name),
                new GetStateSetting(state, postVoting.Index)));
            Send(data);
        }
    }
}
