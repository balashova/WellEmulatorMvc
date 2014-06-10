using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
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
            //Version version = Assembly.GetEntryAssembly().GetName().Version;
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
 
            _logger.Trace("File version: {0}", fileVersionInfo.FileVersion);
            _logger.Trace("Assembly version: {0}", assembly.GetName().Version);

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["SettingsDb"].ConnectionString;
                _logger.Trace("Connection: {0}", connectionString);
            }
            catch (Exception ex)
            {
                _logger.Fatal("Fail to load connection string", ex);
            }

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
                _logger.Fatal("root fatal", ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_serviceHost != null) _serviceHost.Close();
        }
    }
}
