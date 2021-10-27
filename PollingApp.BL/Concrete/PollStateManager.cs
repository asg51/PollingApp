using PollingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.BL.Contcat
{
    public static class PollStateManager
    {
        public static void PollIsControl(Poll poll)
        {
            if (PollingList.dbPoll.GetList().Count != 0)
            {
                if (PollingList.dbPoll.Search(poll).BlockChainStartState)
                    throw new Exception("Seçim başlatılmış durumdadır artık değişiklik yapamazsınız!");
                else if (PollingList.dbPoll.Search(poll) == null)
                    throw new Exception("Seçim Silinmiştir!");                
            }
        }
    }
}
