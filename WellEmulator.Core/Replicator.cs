using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private TimeSpan _replicationPeriod = new TimeSpan(0, 0, 15, 0);
        private StopableThread _replicationThread;
        private bool _isRunning;
        private PdgtmDbAdapter _pdgtmDbAdapter = new PdgtmDbAdapter();
        private HistorianAdapter _historianAdapter = new HistorianAdapter();

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

        }

    }
}
