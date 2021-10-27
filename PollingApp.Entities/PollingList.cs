using PollingApp.Entities.Context;
using System.Collections.Generic;
using WebSocketSharp;

namespace PollingApp.Entities
{
    public static class PollingList
    {
        public static IDictionary<string, WebSocket> wsDict { get; set; } = new Dictionary<string, WebSocket>();
        public static readonly DbPoll dbPoll = new DbPoll();
        public static readonly BlockChainList blockChainList = new BlockChainList();
        public static readonly object _lockObject = new object();
    }
}
