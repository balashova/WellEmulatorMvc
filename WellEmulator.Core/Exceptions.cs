using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    [Serializable]
    public class InvalidTimeSpanException : Exception
    {
        public InvalidTimeSpanException() : base("Invalid TimeStamp value.") { }
    }

    [Serializable]
    public class HistorianServerNotRunningException : Exception
    {
        private new const string Message = "Wonderware Historian server is not running.";

        public HistorianServerNotRunningException() : base(Message) { }

        public HistorianServerNotRunningException(Exception innerException)
            : base(Message + "\n\n" + innerException.StackTrace) { }

        public HistorianServerNotRunningException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    public class HistorianConnectionStringException : Exception
    {
        private new const string Message = "Failed loading a connection string to Historian server.";

        public HistorianConnectionStringException() : base(Message) { }

        public HistorianConnectionStringException(Exception innerException)
            : base(Message + "\n\n" + innerException.StackTrace) { }

        public HistorianConnectionStringException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    public class PDGTMConnectionStringException : Exception
    {
        private new const string Message = "Failed loading a connection string to PDGTM server.";

        public PDGTMConnectionStringException() : base(Message) { }

        public PDGTMConnectionStringException(Exception innerException)
            : base(Message + "\n\n" + innerException.StackTrace) { }

        public PDGTMConnectionStringException(string message, Exception innerException)
            : base(message, innerException) { }
    }

    [Serializable]
    public class PDGTMSelectTopValuesException : Exception
    {
        public PDGTMSelectTopValuesException(string message) : base(message)
        {
            
        }
    }
}
