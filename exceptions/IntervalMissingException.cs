using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    internal class IntervalMissingException : Exception
    {
        public IntervalMissingException() { }

        public IntervalMissingException(string message) : base(message) { }

        public IntervalMissingException(string message,  Exception innerException) : base(message, innerException) { }  
    }
}
