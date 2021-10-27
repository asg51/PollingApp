using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities.P2PModel
{
    public class PostChosenSetting
    {
        public IList<Chosen> Chosens { get; set; }
        public Chosen Chosen { get; set; }
        public int Index { get; set; }
        public PostChosenSetting(IList<Chosen>  chosens, Chosen  chosen, int index)
        {
            Chosens = chosens;
            Chosen = chosen;
            Index = index;
        }
    }
}
