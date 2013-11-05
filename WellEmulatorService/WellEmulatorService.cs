using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using WellEmulator.Core;
using WellEmulator.Models;

namespace WellEmulatorService
{
    public partial class WellEmulatorService : ServiceBase
    {
        private ServiceHost _serviceHost;

        public WellEmulatorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }

            _serviceHost = new ServiceHost(typeof(Emulator));
        }

        protected override void OnStop()
        {
        }
    }
}
