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
            string result = writeToFile(entry); // have this return the config.json file
            //processFile(result);

            //TODO pass the config file to the processer program
            // Create a method that will take the config file and execute the process application

            //TimeHandling.scheduleTask();

            // Testing to see if I can call the applicatin using config file
            //runApp();

        }
        public static string writeToFile(Entry entry)
        {
            string configFilePath = "config.json";
            List<Entry> entries = new List<Entry>();

            try
            {
                // Read the existing file if it exists
                if (File.Exists(configFilePath))
                {
                    string existingContent = File.ReadAllText(configFilePath);

                    // Try to deserialize to a list of entries
                    try
                    {
                        entries = JsonConvert.DeserializeObject<List<Entry>>(existingContent) ?? new List<Entry>();
                    }
                    catch (JsonSerializationException)
                    {
                        // If it fails, try to deserialize a single entry
                        Entry singleEntry = JsonConvert.DeserializeObject<Entry>(existingContent);
                        if (singleEntry != null)
                        {
                            entries.Add(singleEntry);
                        }
                    }
                }

                // Add the new entry to the list
                entries.Add(entry);

                // Serialize the updated list to JSON format
                string updatedContent = JsonConvert.SerializeObject(entries, Formatting.Indented);

                // Write the updated content back to the file
                File.WriteAllText(configFilePath, updatedContent);

                Console.WriteLine("Data successfully written to file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return configFilePath;
        }
        public static void processFile(string config)
        {
            // This method will execute the 
            Debug.WriteLine("Calling processFile method");
            Debug.WriteLine(config);
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
