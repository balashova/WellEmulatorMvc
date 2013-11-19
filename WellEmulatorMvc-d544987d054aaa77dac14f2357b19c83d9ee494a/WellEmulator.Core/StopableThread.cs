using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WellEmulator.Core
{
    public class StopableThread
    {
        public bool Stopped { get; private set; }
        private readonly Thread _thread;
        private readonly TimeSpan _delay;
        private bool _isInitially = true;
        private bool _allowStop = true;

        public StopableThread(TimeSpan delay, ThreadStart threadStart)
        {
            _delay = delay;
            _thread = new Thread(() =>
                {
                    while (!Stopped)
                    {
                        if (!_isInitially)
                        {
                            _allowStop = false;
                            threadStart();
                            _allowStop = true;
                        }
                        else _isInitially = false;
                        Thread.Sleep(_delay);
                    }
                }) { IsBackground = true };
        }

        public void Stop()
        {
            if (_allowStop)
            {
                _thread.Abort();
            }
            Stopped = true;
        }

        public void Start()
        {
            _thread.Start();
        }
    }
}
