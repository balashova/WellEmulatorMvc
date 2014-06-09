using System.Collections.Generic;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Models
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