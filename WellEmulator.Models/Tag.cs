using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WellEmulator.Models
{
    [DataContract]
    public class Tag
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public string WellName { get; set; }

        [DataMember]
        public string Object { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public double MaxValue { get; set; }

        [DataMember]
        public double MinValue { get; set; }

        [DataMember]
        public double Delta { get; set; }
    }
}