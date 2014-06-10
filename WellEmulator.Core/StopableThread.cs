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
        private readonly Thread _thread;
        private readonly TimeSpan _delay;
        private bool _isFirstIteration = true;
        private bool _allowStop = true;

        public StopableThread(TimeSpan delay, ThreadStart threadStart)
        {
            _delay = delay;
            _thread = new Thread(() =>
            {
                IsRunning = true;
                try
                {
                    while (IsRunning)
                    {
                        if (!_isFirstIteration)
                        {
                            _allowStop = false;
                            threadStart();
                            _allowStop = true;
                        }
                        else _isFirstIteration = false;
                        Thread.Sleep(_delay);
                    }
                }
                catch (Exception)
                {
                    IsRunning = false;
                    throw;
                }
            }) { IsBackground = true };
        }

        public bool IsRunning { get; private set; }

        public void Stop()
        {
            if (_allowStop)
            {
                _thread.Abort();
            }
            IsRunning = false;
        }

        public void Start()
        {
            _thread.Start();
        }
    }
}
