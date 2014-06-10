using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceProcess;
using NLog;
using WellEmulator.Core;
using WellEmulator.Models;

namespace WellEmulator.Service
{
    public partial class  WellEmulatorService : ServiceBase
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

            string historianConnection;
            string pdgtmConnectionString;
            string settingsConnectionString;

            try
            {
                historianConnection = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
                pdgtmConnectionString = ConfigurationManager.ConnectionStrings["TeamworkConnection"].ConnectionString;
                settingsConnectionString = ConfigurationManager.ConnectionStrings["SettingsDb"].ConnectionString;
                _logger.Trace("Database connection strings loaded successfully");
            }
            catch (Exception ex)
            {
                _logger.Fatal("Fail to load connection strings", ex);
                throw;
            }

            try
            {
                ISettingsManager settingsManager = new SettingsManager(settingsConnectionString);
                IPdgtmDbAdapter pdgtmDbAdapter = new PdgtmDbAdapter(pdgtmConnectionString);
                IHistorianAdapter historianAdapter = new HistorianAdapter(historianConnection);
                IDatabaseObserver databaseObserver = new DatabaseObserver(historianConnection, pdgtmConnectionString);

                IReporter reporter = new DbReporter(historianAdapter);
                IEmulator emulator = new Emulator(reporter);
                IReplicator replicator = new Replicator(pdgtmDbAdapter, historianAdapter);

                var wellEmulator = new WellEmulator(
                    emulator, replicator,
                    pdgtmDbAdapter, historianAdapter,
                    settingsManager, databaseObserver);

                if (_serviceHost != null)
                {
                    _serviceHost.Close();
                    _serviceHost = null;
                }

                _serviceHost = new ServiceHost(wellEmulator);
                var behavior = _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                behavior.InstanceContextMode = InstanceContextMode.Single;
               
                _serviceHost.Open();
                _logger.Trace("Service started!");
            }
            catch (Exception ex)
            {
                _logger.Fatal("Service failed with fatal error! See stack trace.", ex);
                throw;
            }
        }

        protected override void OnStop()
        {
            if (_serviceHost != null) _serviceHost.Close();
        }
    }
}
