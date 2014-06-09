using System;
using System.ServiceModel;
using System.ServiceProcess;
using NLog;

namespace WellEmulator.Service
{
    public partial class WellEmulatorService : ServiceBase
    {
        private ServiceHost _serviceHost;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public WellEmulatorService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                    _serviceHost = null;
                }

                _serviceHost = new ServiceHost(typeof (WellEmulator));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                _logger.FatalException("root fatal", ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_serviceHost != null) _serviceHost.Close();
        }
    }
}
