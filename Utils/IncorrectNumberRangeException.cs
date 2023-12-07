using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    internal class IncorrectNumberRangeException : Exception
    {
        public IncorrectNumberRangeException() { }

        public IncorrectNumberRangeException(string message) { }

        public IncorrectNumberRangeException (string message, Exception innerException) { } 
    }
}
