using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WellEmulator.Models
{
    public class Tag
    {
        public string Group { get; set; }
        public string Object { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public double Delta { get; set; }
    }
}