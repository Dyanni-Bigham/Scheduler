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

        public string Interval
        { get; set; }


        // test method for debugging will delete later
        public void testEntry()
        {

            Debug.WriteLine("Selected Days:");
            Debug.WriteLine("==============\n");
            //Debug.WriteLine(daysOutput);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Selected Interval:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(Interval);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Selected applications");
            Debug.WriteLine("==============\n");
            //Debug.WriteLine(appsOutput);
        }
    }
}
