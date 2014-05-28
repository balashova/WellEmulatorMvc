using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorMvc.Models
{
    public class NewTagViewModel
    {
        public IEnumerable<Well> Wells { get; set; }
        public IEnumerable<string> WitsmlObjects { get; set; }
        public IEnumerable<string> WitsmlElements { get; set; }
    }
}