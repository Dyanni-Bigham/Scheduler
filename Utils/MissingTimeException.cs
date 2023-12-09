using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utils
{
    internal class MissingTimeException : Exception
    {
        public MissingTimeException() { }
        public MissingTimeException(string message) : base(message) { }
        public MissingTimeException(string messaage, Exception ineer) : base(messaage, ineer) { } 
    }
}
