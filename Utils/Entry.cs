using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    public class Entry
    {
        public List<string> Days 
        { get; set; }

        public List<string> Apps 
        { get; set; }

        public string TimeUnit
        { get; set; }

        public string Interval
        { get; set; }
    }
}
