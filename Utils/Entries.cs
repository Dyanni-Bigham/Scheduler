using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    public class Entries
    {
        private List<Entry> entries { get; set; }

        public void addEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public void removeEntry(Entry entry)
        {
            entries.Remove(entry);
        }

        /*
        public void listEntries()
        {
            foreach (Entry entry in entries)
            {
                Debug.WriteLine(entry.Day);
            }
        }
        */
    }
}
