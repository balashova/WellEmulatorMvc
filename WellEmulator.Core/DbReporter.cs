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
        private readonly IDictionary<string, IList<double>> _cache;

        public DbReporter(IHistorianAdapter historianAdapter)
        {
            _cache = new Dictionary<string, IList<double>>();
            _historianAdapter = historianAdapter;
        }

        public DbReporter(IHistorianAdapter historianAdapter, IDictionary<string, IList<double>> cache)
        {
            _cache = cache;
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
                    IList<double> values;
                    if (_cache.TryGetValue(name, out values))
                    {
                        values.Add(value);
                    }
                    else
                    {
                        values = new List<double> { value };
                        _cache.Add(name, values);
                    } 
                }
            }
        }
    }
}
