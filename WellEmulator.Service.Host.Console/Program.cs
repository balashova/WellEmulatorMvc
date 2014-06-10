using System;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceModel;
using WellEmulator.Core;
using NLog;
using WellEmulator.Models;

namespace WellEmulator.Service.Host.Console
{
    class Program
    {
        private ServiceHost _serviceHost;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private void OnFault(object sender, EventArgs eventArgs)
        {
            _logger.Trace("Service faulted");
        }

        private void Run()
        {
            string historianConnection;
            string pdgtmConnectionString;
            string settingsConnectionString;

            try
            {
                historianConnection = ConfigurationManager.ConnectionStrings["HistorianConnection"].ConnectionString;
                pdgtmConnectionString = ConfigurationManager.ConnectionStrings["TeamworkConnection"].ConnectionString;
                settingsConnectionString = ConfigurationManager.ConnectionStrings["SettingsDb"].ConnectionString;
            }
            catch (Exception ex)
            {
                _logger.Fatal("Can not load connection strings", ex);
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
                
                _serviceHost = new ServiceHost(wellEmulator);
                var behavior = _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                behavior.InstanceContextMode = InstanceContextMode.Single;
                
                _serviceHost.Open();
                _serviceHost.Faulted += OnFault;
                _logger.Trace("Service started!");

                System.Console.WriteLine("The service is ready.");
                System.Console.WriteLine("Press the Enter key to terminate service.");
                System.Console.ReadLine();
                
                _logger.Trace("Service stopped.");
            }
            catch (CommunicationObjectFaultedException ex)
            {
                _logger.Fatal("Restart with administrator rights.", ex);

                System.Console.WriteLine(ex.ToString());
                System.Console.WriteLine();
                System.Console.WriteLine("Restart with administrator rights.");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                _logger.Fatal("Service loading failed.", ex);
                throw;
            }      
        }

        private static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }
    }
}
