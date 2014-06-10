using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Models
{
    public class PdgtmValue
    {
        public int Id { get; set; }
        public int WellId { get; set; }
        public double OilRate { get; set; }
        public double GasRate { get; set; }
        public double WaterRate { get; set; }
        public DateTime Time { get; set; }
    }
}
