using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class BaseModel
    {
        public string EventToBeHeld { get; set; }
        public ConnectAsAdmin ConnectAsAdmin { get; set; }
        public object IncomingModel { get; set; }

        public BaseModel(string eventToBeHeld,ConnectAsAdmin connectAsAdmin, object incomingModel)
        {
            EventToBeHeld = eventToBeHeld;
            ConnectAsAdmin = connectAsAdmin;
            IncomingModel = incomingModel;
        }
    }
}
