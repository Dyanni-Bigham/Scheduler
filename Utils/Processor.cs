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
        public static void HandleMethod(Entry entry)
        {
            /*
            Debug.WriteLine("This data is being handled\n");
            entry.testEntry();
            Debug.WriteLine("After data is handled."); // Delete Later
            */

            // write to config file
            WriteToFile(entry);

            //TimeHandling.scheduleTask();

            // Testing to see if I can call the applicatin using config file
            //runApp();

        }
        public static void WriteToFile(Entry entry)
        {
            string configFilePath = "config.json";

            try
            {
                // Initialize the list of entries
                List<Entry> existingEntries = new List<Entry>();

                // Check if the file exists and read the content
                if (File.Exists(configFilePath))
                {
                    string existingContent = File.ReadAllText(configFilePath);
                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        // Check if the content is a valid JSON array
                        if (existingContent.TrimStart().StartsWith("["))
                        {
                            existingEntries = JsonConvert.DeserializeObject<List<Entry>>(existingContent);
                        }
                        else
                        {
                            // Handle case where existing content is not a JSON array (e.g., empty or invalid)
                            Console.WriteLine("Existing content is not a valid JSON array. Initializing with an empty list.");
                        }
                    }
                }

                // Add new entry
                existingEntries.Add(entry);

                // Serialize the object to JSON format
                string content = JsonConvert.SerializeObject(existingEntries, Formatting.Indented);

                // Write content back to the file
                File.WriteAllText(configFilePath, content);

                Console.WriteLine("Data successfully appended to file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error processing JSON: " + ex.Message);
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
