using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace WellEmulator.Models
{
    [DataContract]
    public class Settings
    {
        [DataMember]
        public TimeSpan SamplingRate { get; set; }

        [DataMember]
        public TimeSpan ReportSave { get; set; }

        [DataMember]
        public TimeSpan Delay { get; set; }

        [DataMember]
        public TimeSpan ReplicationPeriod { get; set; }

        [DataMember]
        public bool IsRunning { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}