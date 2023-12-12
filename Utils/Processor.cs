using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    public class Processor
    {
        public static void handleMethod(Entry entry)
        {
            Debug.WriteLine("This data is being handled\n");
            entry.testEntry();
        }
    }
}
