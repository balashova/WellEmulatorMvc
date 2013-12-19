﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Models;

namespace WellEmulator.Core
{
    public class Replicator
    {
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

        public void RemoveMapping(MapItem mapItem)
        {
            lock (_mappings)
            {
                _mappings.Add(_mappings.Single(m => m.HistorianTag.Equals(mapItem.HistorianTag) &&
                                                    m.PdgtmTag.Equals(mapItem.PdgtmTag) &&
                                                    m.PdgtmWellName.Equals(mapItem.PdgtmWellName)));
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
            lock (_mappings)
            {
                var mappings = _mappings;
                var tags = _historianAdapter.GetTagValues(mappings.Select(m => string.Format("{0}.{1}", m.PdgtmWellName, m.HistorianTag)).ToList());

                foreach (var well in mappings.GroupBy(m => m.PdgtmWellName))
                {
                    var oilRate = tags[well.Single(m => m.PdgtmTag.Equals("oilRate")).HistorianTag];
                    var gasRate = tags[well.Single(m => m.PdgtmTag.Equals("gasRate")).HistorianTag];
                    var waterRate = tags[well.Single(m => m.PdgtmTag.Equals("waterRate")).HistorianTag];

                    _pdgtmDbAdapter.InsertValues(_pdgtmDbAdapter.GetWellId(well.Key), oilRate, gasRate, waterRate);
                }
            }
        }
    }
}
