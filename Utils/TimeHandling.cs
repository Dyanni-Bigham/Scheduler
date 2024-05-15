using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Scheduler.Utils
{
    public  class TimeHandling
    {
        private static Timer timer;
        private static string interval;

        public static void scheduleTask()
        {
            string[] days = getValues("config.json");
            //Debug.WriteLine("Inside of the scheduleTask method");

            //Debug.WriteLine("Inside of schedule tasks here are the values to use.");
            //Debug.WriteLine(interval);
            Debug.WriteLine(string.Join(", ", days));
            bool result = isInDays(days);
            Debug.WriteLine("Is the current day in the range?");
            Debug.WriteLine(result);
            

            // logic for determining when to run task
        }

        // This checks if the current day is in the days selected by the user
        private static bool isInDays(string[] days)
        {
            DayOfWeek currentDay = DateTime.Today.DayOfWeek;
            Debug.WriteLine(currentDay);

            foreach (string day in days)
            {
                DayOfWeek dayOfWeek;
                if (Enum.TryParse(day, true, out dayOfWeek)) // converts day to DayofWeek
                {
                    if (dayOfWeek == currentDay) 
                    {
                        return true;
                    }
                }

            }

            return false;

        }

        private static string[] getValues(string fileName)
        {
            string jsonContent = File.ReadAllText(fileName);
            dynamic config = JsonConvert.DeserializeObject(jsonContent);
            var listofDays = new List<string>();
            
            if (config.Days != null && config.Days.Count > 0 &&
                !string.IsNullOrEmpty((string)config.Interval))
            {
                foreach (string day in config.Days)
                {
                    listofDays.Add(day);
                }

                interval = config.Interval;
            }
            string[] Days = listofDays.ToArray();
            return Days;
        }
    }
}
