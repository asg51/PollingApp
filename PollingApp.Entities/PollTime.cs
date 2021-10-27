using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingApp.Entities
{
    public class PollTime
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }

        public PollTime(DateTime startTime, DateTime finishTime)
        {
            StartTime = startTime;
            FinishTime = finishTime;
        }
    }
}
