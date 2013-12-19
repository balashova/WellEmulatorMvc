using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public TimeSpan ReportAutoSavePeriod { get; set; }

        [DataMember]
        public TimeSpan ValuesDelay { get; set; }

        [DataMember]
        public TimeSpan ReplicationPeriod { get; set; }

        //[NotMapped]
        [DataMember]
        public bool IsRunning { get; set; }

        //[NotMapped]
        [DataMember]
        public string Message { get; set; }
    }
}