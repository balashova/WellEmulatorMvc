using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public class DbReporter : IReporter
    {
        private readonly IHistorianAdapter _historianAdapter;
        private readonly Dictionary<string, List<double>> _cache = new Dictionary<string, List<double>>();

        public DbReporter(IHistorianAdapter historianAdapter)
        {
            _historianAdapter = historianAdapter;
        }

        public TimeSpan Delay { get; set; }

        public void Save()
        {
            lock (_cache)
            {
                _historianAdapter.InsertTagValues(_cache);
                _cache.Clear();
            }
        }

        public double this[string name]
        {
            set
            {
                lock (_cache)
                {
                    if (_cache.ContainsKey(name))
                    {
                        _cache[name].Add(value);
                    }
                    else
                    {
                        _cache.Add(name, new List<double>());
                        _cache[name].Add(value);
                    } 
                }
            }
        }
    }
}
