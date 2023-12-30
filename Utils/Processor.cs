using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    public class Processor
    {
        // rename this method to handleEntry
        public static void handleMethod(Entry entry)
        {
            /*
            Debug.WriteLine("This data is being handled\n");
            entry.testEntry();
            Debug.WriteLine("After data is handled."); // Delete Later
            */

            // write to config file
            writeToFile(entry);

            // Testing to see if I can call the applicatin using config file

        }

        public static void writeToFile(Entry entry)
        {
            // write to a .json file that is stored config
            
            var configuration = new
            {
                Days = entry.Days,
                Interval = entry.Interval,
                Applications = entry.Apps
            };

            string configFilePath = "config.json";

            try
            {
                // Serialize the object to JSON format
                string content = JsonConvert.SerializeObject
                    (configuration, Formatting.Indented);

                File.WriteAllText(configFilePath, content);

                Debug.WriteLine("Data successfully written to file.");
            }
            catch (IOException ex) 
            {
                Debug.WriteLine(ex.ToString());
            }
 
        }

        public static void runApp()
        {
            // read the config file to get the application

            // read the 
        }
    }
}
