using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class GetStateSetting
    {
        public bool State { get; set; }
        public int Index { get; set; }
        public GetStateSetting(bool state, int index)
        {
            State = state;
            Index = index;
        }
    }
}
