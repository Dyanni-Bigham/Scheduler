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

        public string daysOutput;
        public string appsOutput;

        // test method for debugging
        public void testEntry()
        {
            formatDays();
            testApps();

            Debug.WriteLine("Selected Days:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(daysOutput);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Selected Interval:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(Interval);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Time Unit:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(TimeUnit);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Selected applications");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(appsOutput);
        }

        public void formatDays()
        {
            daysOutput = string.Join(", ", Days);
        }

        public void testApps()
        {
            appsOutput = string.Join(", ", Apps);
        }
    }
}
