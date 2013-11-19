using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public class InvalidTimeSpanException : Exception
    {
        public InvalidTimeSpanException() : base("Invalid TimeStamp value.") { }
    }
}
