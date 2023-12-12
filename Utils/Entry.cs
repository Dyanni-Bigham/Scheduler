﻿using System;
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

        public string TimeUnit
        { get; set; }

        public string Interval
        { get; set; }

        public string output;

        // test method for debugging
        public void testEntry()
        {
            formatDays();

            Debug.WriteLine("Selected Days:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(output);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Selected Interval:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(Interval);

            Debug.WriteLine("\n\n");

            Debug.WriteLine("Time Unit:");
            Debug.WriteLine("==============\n");
            Debug.WriteLine(TimeUnit);
        }

        public void formatDays()
        {
            output = string.Join(", ", Days);
        }
    }
}
