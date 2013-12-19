using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    [DataContract]
    public class MapItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string HistorianTag { get; set; }

        [DataMember]
        public string PdgtmTag { get; set; }
    }
}
