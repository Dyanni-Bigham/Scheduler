using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    internal class MissingUnitException : Exception
    {
        public MissingUnitException() { }
        public MissingUnitException(string message) : base(message) { }
        public MissingUnitException(string message, Exception innerException) : base(message, innerException) { }
    }
}
