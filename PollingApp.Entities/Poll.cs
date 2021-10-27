using PollingApp.Entities.Context;
using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace PollingApp.Entities
{
    public class Poll
    {
        public IList<string> Urls { get; set; }
        public string PollingName { get; set; }
        public int Index { get; set; }
        public PollTime PollTime { get; set; }
        public DbAdmins Admins { get; set; }
        public DbVoter Voter { get; set; }
        public DbChosen Chosen { get; set; }
        public bool BlockChainStartState { get; set; }
        public Poll(string name, int index)
        {
            Urls = new List<string>();
            PollingName = name;
            Index = index;
            Admins = new DbAdmins();
            Voter = new DbVoter();
            Chosen = new DbChosen();
            BlockChainStartState = false;
        }
        public Poll(IList<string> urls, string pollName, int index,
            PollTime pollTime, IList<Admin> admins, IList<Voter> voters, IList<Chosen> chosens)
        {
            PollingName = pollName;
            Index = index;
            Admins = new DbAdmins();
            Admins.SetList(admins);
            Voter = new DbVoter();
            Voter.SetList(voters);
            Chosen = new DbChosen();
            Chosen.SetList(chosens);
            Urls = urls;
            PollTime = pollTime;
            BlockChainStartState = false;
        }
    }
}