using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellEmulator.Models
{
    public class Settings
    {
        public TimeSpan SamplingRate { get; set; }
        public TimeSpan ReportSave { get; set; }
        public TimeSpan Delay { get; set; }
        public string Message { get; set; }
    }
}