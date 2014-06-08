using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public interface IReporter
    {
        void Save(TimeSpan delay = default(TimeSpan));
        double this[string name] { set; }
    }
}
