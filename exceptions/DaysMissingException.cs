using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    internal class DaysMissingException : Exception
    {
        public DaysMissingException() { }

        public DaysMissingException(string message) : base(message) { }

        public DaysMissingException(string message, Exception inner) : base(message, inner) { }
    }
}
