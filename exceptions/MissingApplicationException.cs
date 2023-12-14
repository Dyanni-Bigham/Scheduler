using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.exceptions
{
    internal class MissingApplicationException : Exception
    {
        public MissingApplicationException() { }

        public MissingApplicationException(string message) : base(message) { }

        public MissingApplicationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
