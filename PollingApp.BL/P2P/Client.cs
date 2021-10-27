using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp;
using System.Text;
using System.Threading.Tasks;
using PollingApp.Entities;
using PollingApp.Entities.P2PModel;
using static PollingApp.Entities.BlockChainList;

namespace PollingApp.BL.P2P
{
    public delegate void ClientControl(BlockChainsData blockChainsData);
    public delegate void ClientMessageControl();
    public class Client
    {
        public event ClientControl ClientPostPollEvent;
        public event ClientControl ClientPostPollNotNullEvent;
        public event ClientMessageControl ErrorSendingData;
        public event ClientMessageControl CompletedSendingData;
        public event ClientMessageControl NotCompletedSendingData;

        private WebSocket ws;
        private string url;
        private Dictionary<string, Action<ConnectAsAdmin, object>> keyValuePairs;
        public Client()
        {
            keyValuePairs = new Dictionary<string, Action<ConnectAsAdmin, object>>()
            {
                {"Hello Client", new Action<ConnectAsAdmin,object>(ActionHelloClient)},
                {"admin login poll post", new Action<ConnectAsAdmin,object>(ActionAddPoll)},
                {"admin login blockChain post", new Action<ConnectAsAdmin,object>(ActionAddBlockChain)},
                {"selection not found", new Action<ConnectAsAdmin,object>(ActionSelectionNotFound)},

                {"add admin state", new Action<ConnectAsAdmin,object>(ActionAdminAddState)},
                {"post correct admin data", new Action<ConnectAsAdmin,object>(ActionPostCorrectAdminData)},

                {"add chosen state", new Action<ConnectAsAdmin,object>(ActionChosenAddState)},
                {"post correct chosen data", new Action<ConnectAsAdmin,object>(ActionPostCorrectChosenData)},

                {"add voter state", new Action<ConnectAsAdmin,object>(ActionVoterAddState)},
                {"post correct voter data", new Action<ConnectAsAdmin,object>(ActionPostCorrectVoterData)},

                {"add poll state", new Action<ConnectAsAdmin,object>(ActionPollAddState)},
                {"blockChainStart completed", new Action<ConnectAsAdmin,object>(ActionBlockChainStartCompleted)},
                {"blockChainStart state", new Action<ConnectAsAdmin,object>(ActionStartBlockChainListAddState)},
                {"post correct poll blockchain data", new Action<ConnectAsAdmin,object>(ActionPostCorrectPollBlockChainData)},
                {"completed delete poll", new Action<ConnectAsAdmin,object>(ActionCompletedDeletePoll)},
                {"completed delete blockchain", new Action<ConnectAsAdmin,object>(ActionCompletedDeleteBlockChain)},

                {"post poll", new Action<ConnectAsAdmin,object>(ActionPostPoll)},
                {"post poll not found", new Action<ConnectAsAdmin,object>(ActionPostPollNotFound)},
                {"state use voter", new Action<ConnectAsAdmin,object>(ActionStateUseVoter)},
                {"completed use voter", new Action<ConnectAsAdmin,object>(ActionCompletedUseVoter)},
                {"post correct use voter data", new Action<ConnectAsAdmin,object>(ActionPostCorrectUseVoterData)},
                {"state voting", new Action<ConnectAsAdmin,object>(ActionStateVoting)}
            };
        }
        public void Connect(ConnectAsAdmin connectAsAdmin, string url)
        {
            if (PollingList.dbPoll.GetList().FirstOrDefault(x => x.PollingName == connectAsAdmin.Name) == null &&
                PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name) == null)
            {
                if (!PollingList.wsDict.ContainsKey(url) && url != P2PContext.IP)
                {
                    this.url = url;
                    ws = new WebSocket("ws://" + url + "/Blockchain");
                    ws.OnMessage += (sender, e) =>
                    {
                        lock (PollingList._lockObject)
                        {
                            Console.WriteLine(e.Data);
                            BaseModel baseModel = JsonConvert.DeserializeObject<BaseModel>(e.Data);
                            keyValuePairs.FirstOrDefault(x => x.Key == baseModel.EventToBeHeld).Value.Invoke(baseModel.ConnectAsAdmin, baseModel.IncomingModel);
                        }
                    };
                    ws.OnError += (sender, e) =>
                    {
                        if (e.Message == "An error has occurred in sending data." ||
                        e.Message == "An error has occurred in sending data.")
                        {
                            if (ErrorSendingData != null)
                                ErrorSendingData();
                        }

                        string key = PollingList.wsDict.FirstOrDefault(x => x.Value == ws).Key;

                        PollingList.dbPoll.GetList().Select(x => x.Urls.Remove(key));
                        PollingList.blockChainList.GetBlockChains().Select(x => x.Urls.Remove(key));
                        PollingList.wsDict.Remove(url);
                    };
                    ws.WaitTime = new TimeSpan(1, 0, 0, 0);
                    ws.Connect();
                    PollingList.wsDict.Add(url, ws);
                }
                PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value.Send(
                    JsonConvert.SerializeObject(new BaseModel("admin login", connectAsAdmin, null)));
            }
        }
        public void PollConnect(ConnectAsAdmin connectAsAdmin, string url)
        {
            if (PollingList.dbPoll.GetList().FirstOrDefault(x => x.PollingName == connectAsAdmin.Name) == null &&
                PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name) == null)
            {
                if (!PollingList.wsDict.ContainsKey(url) && url != P2PContext.IP)
                {
                    this.url = url;
                    ws = new WebSocket("ws://" + url + "/Blockchain");
                    ws.OnMessage += (sender, e) =>
                    {
                        lock (PollingList._lockObject)
                        {
                            Console.WriteLine(e.Data);
                            BaseModel baseModel = JsonConvert.DeserializeObject<BaseModel>(e.Data);
                            keyValuePairs.FirstOrDefault(x => x.Key == baseModel.EventToBeHeld).Value.Invoke(baseModel.ConnectAsAdmin, baseModel.IncomingModel);
                        }
                    };
                    ws.OnError += (sender, e) =>
                    {
                        if (e.Message == "An error has occurred in sending data." ||
                        e.Message == "An error has occurred in sending data.")
                        {
                            if (ErrorSendingData != null)
                                ErrorSendingData();
                        }

                        string key = PollingList.wsDict.FirstOrDefault(x => x.Value == ws).Key;

                        PollingList.dbPoll.GetList().Select(x => x.Urls.Remove(key));
                        PollingList.blockChainList.GetBlockChains().Select(x => x.Urls.Remove(key));
                        PollingList.wsDict.Remove(url);
                    };
                    ws.WaitTime = new TimeSpan(1, 0, 0, 0);
                    ws.Connect();
                    PollingList.wsDict.Add(url, ws);
                }
                PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value.Send(
                    JsonConvert.SerializeObject(new BaseModel("get poll", connectAsAdmin, connectAsAdmin.Name)));
            }
        }
        public void Connect(string url, string pollName)
        {
            if (!PollingList.wsDict.ContainsKey(url) && url != P2PContext.IP)
            {
                WebSocket ws = new WebSocket("ws://" + url + "/Blockchain");
                ws.OnMessage += (sender, e) =>
                {
                    lock (PollingList._lockObject)
                    {
                        BaseModel baseModel = JsonConvert.DeserializeObject<BaseModel>(e.Data);
                        keyValuePairs.FirstOrDefault(x => x.Key == baseModel.EventToBeHeld).Value.Invoke(baseModel.ConnectAsAdmin, baseModel.IncomingModel);
                    }
                };
                ws.OnClose += (sender, e) =>
                {
                    ws.Connect();
                };
                ws.OnError += (sender, e) =>
                {
                    if (e.Message == "An error has occurred in sending data." ||
                         e.Message == "An error has occurred in sending data.")
                    {
                        if (ErrorSendingData != null)
                            ErrorSendingData();
                    }

                    string key = PollingList.wsDict.FirstOrDefault(x => x.Value == ws).Key;

                    PollingList.dbPoll.GetList().Select(x => x.Urls.Remove(key));
                    PollingList.blockChainList.GetBlockChains().Select(x => x.Urls.Remove(key));
                    PollingList.wsDict.Remove(url);
                };
                ws.WaitTime = new TimeSpan(1, 0, 0, 0);
                ws.Connect();
                ws.Send(JsonConvert.SerializeObject(new BaseModel("Hello Server", new ConnectAsAdmin(P2PContext.IP, UserAdmin.Key, UserAdmin.Password, pollName), pollName)));
                PollingList.wsDict.Add(url, ws);
            }
            else if (PollingList.wsDict.ContainsKey(url))
            {
                PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value.Send(JsonConvert.SerializeObject(new BaseModel("Hello Server", new ConnectAsAdmin(P2PContext.IP, UserAdmin.Key, UserAdmin.Password, pollName), pollName)));
            }
        }
        private void ActionHelloClient(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
        }
        private void ActionAdminAddState(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting adminSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddAdminState(connectAsAdmin, adminSetting);
        }
        private void ActionPostCorrectAdminData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Admin> admins = JsonConvert.DeserializeObject<IList<Admin>>(baseModel.ToString());
            Managers.clientManager.GetCorrectAdminData(connectAsAdmin.Name, admins);
        }
        private void ActionAddPoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = JsonConvert.DeserializeObject<BaseModel>(JsonConvert.SerializeObject(baseModel));
            PostPoll postPoll = JsonConvert.DeserializeObject<PostPoll>(JsonConvert.SerializeObject(modelbase.IncomingModel));
            IList<string> list;
            if (postPoll.Urls != null)
            {
                postPoll.Urls.Add(connectAsAdmin.IP);
                list = WebSocketAndUrlSetting(postPoll.Urls, connectAsAdmin.Name);
            }
            else
            {
                list = WebSocketAndUrlSetting(new List<string> { connectAsAdmin.IP }, connectAsAdmin.Name);
            }
            list = WebSocketAndUrlSetting(list, connectAsAdmin.Name);
            if (postPoll != null)
                CompletedSendingData();
            else
                NotCompletedSendingData();

            Managers.pollManager.P2PAdd(new Poll(list, postPoll.PollingName, postPoll.Index, postPoll.PollTime,
                postPoll.Admins, postPoll.Voters, postPoll.Chosens));
        }
        private void ActionAddBlockChain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = JsonConvert.DeserializeObject<BaseModel>(JsonConvert.SerializeObject(baseModel));
            BlockChainsData blockChainsData = JsonConvert.DeserializeObject<BlockChainsData>(JsonConvert.SerializeObject(modelbase.IncomingModel));

            IList<string> list;
            if (blockChainsData.Urls != null)
            {
                blockChainsData.Urls.Add(connectAsAdmin.IP);
                list = WebSocketAndUrlSetting(blockChainsData.Urls, connectAsAdmin.Name);
            }
            else
            {
                list = WebSocketAndUrlSetting(new List<string> { connectAsAdmin.IP }, connectAsAdmin.Name);
            }

            if (blockChainsData != null)
                CompletedSendingData();
            else
                NotCompletedSendingData();

            Managers.blockChainManager.AddFromServer(ref blockChainsData);
        }

        private void ActionSelectionNotFound(ConnectAsAdmin connectAsAdmin, object baseModel)
        {

            PollingList.wsDict.Remove(PollingList.wsDict.FirstOrDefault(x => x.Key == connectAsAdmin.IP));
            if (ErrorSendingData != null)
                ErrorSendingData();
        }

        private void ActionChosenAddState(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting chosenSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddChosenState(connectAsAdmin, chosenSetting);
        }
        private void ActionPostCorrectChosenData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Chosen> chosens = JsonConvert.DeserializeObject<IList<Chosen>>(baseModel.ToString());
            Managers.clientManager.GetCorrectChosenData(connectAsAdmin.Name, chosens);
        }
        private void ActionVoterAddState(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting voterSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddVoterState(connectAsAdmin, voterSetting);
        }
        private void ActionPostCorrectVoterData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            IList<Voter> voters = JsonConvert.DeserializeObject<IList<Voter>>(baseModel.ToString());
            Managers.clientManager.GetCorrectVoterData(connectAsAdmin.Name, voters);
        }
        private void ActionPollAddState(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting pollSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddPollState(connectAsAdmin, pollSetting);
        }
        private void ActionBlockChainStartCompleted(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
        }
        private void ActionStartBlockChainListAddState(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting chosenSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddStartBlockChainListState(connectAsAdmin, chosenSetting);
        }
        private void ActionPostCorrectPollBlockChainData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BaseModel modelbase = JsonConvert.DeserializeObject<BaseModel>(JsonConvert.SerializeObject(baseModel));
            PostPoll postPoll = JsonConvert.DeserializeObject<PostPoll>(JsonConvert.SerializeObject(modelbase.IncomingModel));
            postPoll.Urls.Add(connectAsAdmin.IP);
            postPoll.Urls = WebSocketAndUrlSetting(postPoll.Urls, connectAsAdmin.Name);
            Managers.pollManager.P2PAdd(new Poll(postPoll.Urls, postPoll.PollingName, postPoll.Index, postPoll.PollTime,
                postPoll.Admins, postPoll.Voters, postPoll.Chosens));
            PollingList.blockChainList.Delete(PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name));
        }
        private void ActionCompletedDeletePoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            if (UrlControl(connectAsAdmin.IP, baseModel.ToString()))
            {
                Close(connectAsAdmin.IP);
            }
        }
        private void ActionCompletedDeleteBlockChain(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            if (UrlControl(connectAsAdmin.IP, baseModel.ToString()))
            {
                Close(connectAsAdmin.IP);
            }
        }
        private void ActionPostPoll(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            BlockChainsData blockChainsData = JsonConvert.DeserializeObject<BlockChainsData>(baseModel.ToString());
            blockChainsData.Urls.Add(connectAsAdmin.IP);
            blockChainsData.Urls = WebSocketAndUrlSetting(blockChainsData.Urls, connectAsAdmin.Name);
            Managers.blockChainManager.AddFromServer(ref blockChainsData);
            if (ClientPostPollEvent != null)
                ClientPostPollEvent(blockChainsData);
        }
        private void ActionPostPollNotFound(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            ClientPostPollNotNullEvent(null);
        }
        private void ActionStateUseVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            Managers.clientManager.AddUseVoterListState(connectAsAdmin, JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString()));
        }
        private void ActionCompletedUseVoter(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
        }
        private void ActionPostCorrectUseVoterData(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            PollingList.blockChainList.GetBlockChains().FirstOrDefault(x => x.BlockChainForPollName == connectAsAdmin.Name).
                UseVoter = JsonConvert.DeserializeObject<IList<int>>(baseModel.ToString());
        }
        private void ActionStateVoting(ConnectAsAdmin connectAsAdmin, object baseModel)
        {
            GetStateSetting stateSetting = JsonConvert.DeserializeObject<GetStateSetting>(baseModel.ToString());
            Managers.clientManager.AddVotingListState(connectAsAdmin, stateSetting);
        }
        public void Send(string url, string data, string pollName)
        {
            if (url != P2PContext.IP)
            {
                WebSocket webSocket = PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value;
                if (webSocket != null)
                {
                    if (webSocket.ReadyState != WebSocketState.Open && !webSocket.IsAlive)
                        webSocket.Connect();

                    webSocket.Send(data);
                }
                else
                {
                    Connect(url, pollName);
                    webSocket = PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value;
                    if (webSocket.ReadyState != WebSocketState.Open && !webSocket.IsAlive)
                        webSocket.Connect();

                    webSocket.Send(data);
                }
            }
        }
        public void Send(IList<string> urls, string data, string pollName)
        {
            foreach (string url in urls)
            {
                if (url != P2PContext.IP)
                {
                    WebSocket webSocket = PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value;
                    if (webSocket != null)
                    {
                        if (webSocket.ReadyState != WebSocketState.Open && !webSocket.IsAlive)
                            webSocket.Connect();

                        webSocket.Send(data);
                    }
                    else
                    {
                        Connect(url, pollName);
                        webSocket = PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value;
                        if (webSocket.ReadyState != WebSocketState.Open && !webSocket.IsAlive)
                            webSocket.Connect();

                        webSocket.Send(data);
                    }
                }

            }

        }
        public void Close(string url)
        {
            if (P2PContext.IP != url)
            {
                PollingList.wsDict.FirstOrDefault(x => x.Key == url).Value.Close();
            }
            PollingList.wsDict.Remove(url);
        }
        public bool UrlControl(string url, string pollName)
        {
            foreach (Poll poll in PollingList.dbPoll.GetList())
            {
                if (poll.PollingName != pollName)
                {
                    if (poll.Urls.FirstOrDefault(x => x == url) != null)
                        return false;
                }
            }
            foreach (BlockChainsData blockChainsData in PollingList.blockChainList.GetBlockChains())
            {
                if (blockChainsData.BlockChainForPollName != pollName)
                {
                    if (blockChainsData.Urls.FirstOrDefault(x => x == url) != null)
                        return false;
                }
            }
            return true;
        }
        private IList<string> WebSocketAndUrlSetting(IList<string> urls, string pollName)
        {
            IList<string> datas = new List<string>();

            foreach (string value in urls)
            {
                if (value != P2PContext.IP && datas.Where(x => x == value).Count() == 0)
                {
                    Connect(value, pollName);
                    datas.Add(value);
                }
            }
            return datas;
        }
    }
}
