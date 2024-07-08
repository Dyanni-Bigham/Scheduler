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
        public static void HandleMethod(Entry entry)
        {
            WriteToFile(entry);

        }
        public static void WriteToFile(Entry entry)
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Debug.WriteLine($"In the directory {appDirectory}");
            string configFilePath = Path.Combine(appDirectory, "config.json");

            try
            {
                List<Entry> existingEntries = new List<Entry>();

                if (File.Exists(configFilePath))
                {
                    string existingContent = File.ReadAllText(configFilePath);
                    if (!string.IsNullOrWhiteSpace(existingContent))
                    {
                        if (existingContent.TrimStart().StartsWith("["))
                        {
                            existingEntries = JsonConvert.DeserializeObject<List<Entry>>(existingContent);
                        }
                        else
                        {
                            Console.WriteLine("Existing content is not a valid JSON array. Initializing with an empty list.");
                        }
                    }
                }

                existingEntries.Add(entry);

                string content = JsonConvert.SerializeObject(existingEntries, Formatting.Indented);

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
    }
}
