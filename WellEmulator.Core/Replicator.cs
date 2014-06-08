using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class Replicator
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        public TimeSpan ReplicationPeriod
        {
            get { return _replicationPeriod; }
            set
            {
                if (value.Equals(default(TimeSpan))) throw new InvalidTimeSpanException();
                _replicationPeriod = value;
                if (_isRunning) ReplicationThreadRestart();
            }
        }

        public List<MapItem> Mappings
        {
            get { return _mappings; }
            set
            {
                if (value == null) return;
                _mappings = value;
            }
        }

        private TimeSpan _replicationPeriod = new TimeSpan(0, 0, 15, 0);
        private readonly PdgtmDbAdapter _pdgtmDbAdapter = new PdgtmDbAdapter();
        private readonly HistorianAdapter _historianAdapter = new HistorianAdapter();
        private List<MapItem> _mappings = new List<MapItem>();
        private StopableThread _replicationThread;
        private bool _isRunning;

        public void AddMapping(MapItem mapItem)
        {
            lock (_mappings)
            {
                _mappings.Add(mapItem);
            }
        }

        public void RemoveMapping(List<int> mapItems)
        {
            lock (_mappings)
            {
                _mappings.RemoveAll(m => mapItems.Contains(m.Id));
            }
        }

        public void Start()
        {
            if (_isRunning) return;
            ReplicationThreadStart();
            _isRunning = true;
        }

        public void Stop()
        {
            ReplicationThreadStop();
            _isRunning = false;
        }

        private void ReplicationThreadStop()
        {
            if (_replicationThread != null) _replicationThread.Stop();
        }

        private void ReplicationThreadStart()
        {
            _replicationThread = new StopableThread(_replicationPeriod, Replicate);
            _replicationThread.Start();
        }

        private void ReplicationThreadRestart()
        {
            ReplicationThreadStop();
            ReplicationThreadStart();
        }

        private void Replicate()
        {
            try
            {
                if (!_mappings.Any()) return;

                lock (_mappings)
                {
                    var mappings = _mappings;
                    var tags =
                        _historianAdapter.GetTagValues(
                            mappings.Select(m => string.Format("{0}.{1}", m.HistorianWellName, m.HistorianTag)).ToList());

                    foreach (var well in mappings.GroupBy(m => m.PdgtmWellName))
                    {
                        var mapOilRate = well.SingleOrDefault(m => m.PdgtmTag.Equals("oilRate"));
                        var mapGasRate = well.SingleOrDefault(m => m.PdgtmTag.Equals("gasRate"));
                        var mapWaterRate = well.SingleOrDefault(m => m.PdgtmTag.Equals("waterRate"));

                        double oilRate = 0;
                        if (mapOilRate != null)
                        {
                            tags.TryGetValue(
                                string.Format("{0}.{1}", mapOilRate.HistorianWellName, mapOilRate.HistorianTag),
                                out oilRate);
                        }

                        double gasRate = 0;
                        if (mapGasRate != null)
                        {
                            tags.TryGetValue(
                                string.Format("{0}.{1}", mapGasRate.HistorianWellName, mapGasRate.HistorianTag),
                                out gasRate);
                        }

                        double waterRate = 0;
                        if (mapWaterRate != null)
                        {
                            tags.TryGetValue(
                                string.Format("{0}.{1}", mapWaterRate.HistorianWellName, mapWaterRate.HistorianTag),
                                out waterRate);
                        }

                        _pdgtmDbAdapter.InsertValues(_pdgtmDbAdapter.GetWellId(well.Key), oilRate, gasRate, waterRate);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("replication failed", ex);
                throw;
            }
        }
    }
}
