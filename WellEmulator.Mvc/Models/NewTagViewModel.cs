using System.Collections.Generic;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Models
{
    public class NewTagViewModel
    {
        public IEnumerable<Well> Wells { get; set; }
        public IEnumerable<string> WitsmlObjects { get; set; }
        public IEnumerable<string> WitsmlElements { get; set; }
    }
}