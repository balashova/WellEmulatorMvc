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

    public class HistorianServerNotRunningException : Exception
    {
        public HistorianServerNotRunningException() : base("Wonderware Historian server is not running.") { }

        public HistorianServerNotRunningException(Exception innerException)
            : base("Wonderware Historian server is not running.",
                innerException) { }
    }
}
