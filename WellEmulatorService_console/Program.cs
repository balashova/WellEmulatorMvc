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
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                logger.Debug("starting...");
                using (var serviceHost = new ServiceHost(typeof (WellEmulator)))
                {
                    // Open the host and start listening for incoming messages.
                    var instance = WellEmulatorSingle.Instance;
                    serviceHost.Open();
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press the Enter key to terminate service.");
                    Console.ReadLine();
                }
            }
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Please restart with administrator rights");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.FatalException("root fatal", ex);
                throw;
            }            
        }
    }
}
