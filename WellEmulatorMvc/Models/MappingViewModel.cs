using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorMvc.Models
{
    public class MappingViewModel
    {
        public IEnumerable<string> HistWells { get; set; }
        public IEnumerable<string> PdgtmWells { get; set; }
        public IEnumerable<string> HistTags { get; set; }
        public IEnumerable<string> PdgtmTags { get; set; }
        public IEnumerable<MapItem> MapItems { get; set; }
    }
}