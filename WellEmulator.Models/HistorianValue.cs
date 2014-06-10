using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    public class HistorianValue
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public double Value { get; set; }
        public DateTime Time { get; set; }
    }
}
