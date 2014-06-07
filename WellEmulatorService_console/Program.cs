using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WellEmulatorService;

namespace WellEmulatorService_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            try
            {
                _logger.Debug("starting...");
                using (var serviceHost = new ServiceHost(typeof(WellEmulator)))
                {
                    // Open the host and start listening for incoming messages.
                    var instance = WellEmulatorSingle.Instance;
                    serviceHost.Open();
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press the Enter key to terminate service.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                _logger.FatalException("root fatal", ex);
                throw;
            }
            
        }
    }
}
