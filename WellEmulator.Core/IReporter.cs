using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public interface IReporter
    {
        void Save();
        double this[string name] { set; }
        TimeSpan Delay { get; set; }
    }
}
