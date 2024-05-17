using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


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

            //TimeHandling.scheduleTask();

            // Testing to see if I can call the applicatin using config file
            //runApp();

        }

        public static void writeToFile(Entry entry)
        {
            // write to a .json file that stores the config
            
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
            string filePath = "config.json";

            string jsonContent = File.ReadAllText(filePath);

            dynamic config = JsonConvert.DeserializeObject(jsonContent);

            if (config.Applications != null && config.Applications is JArray)
            {
                string application = config.Applications[0]?.ToString();
                string applicationPath = $@"C:\Users\dyann\Documents\Development\HelloWPFApp\HelloWPFApp\bin\Release\net7.0-windows\{application}.exe";

                Debug.WriteLine("Getting the application field");
                Debug.WriteLine($"Application: {application}");

                // Executing the application
                ProcessStartInfo startInfo = new ProcessStartInfo { FileName = applicationPath };

                // Start the process
                using (Process process = new Process { StartInfo = startInfo})
                {
                    process.Start();
                    process.WaitForExit();
                }

                // filepath to list of application(s) to execute
            }
            else
            {
                Debug.WriteLine("Error");
            }

        }

        public static void showTime()
        {
            DateTime currentDateTime = DateTime.Now;
            Debug.WriteLine($"Current Date amd Time: {currentDateTime} on {DateTime.Now.DayOfWeek}");
        }
    }
}
