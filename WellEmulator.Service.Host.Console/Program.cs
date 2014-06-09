using System;
using System.ServiceModel;
using NLog;

namespace WellEmulator.Service.Host.Console
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
                    var wellEmulator = new WellEmulator();
                    serviceHost.Open();
                    System.Console.WriteLine("The service is ready.");
                    System.Console.WriteLine("Press the Enter key to terminate service.");
                    System.Console.ReadLine();
                }
            }
            catch (CommunicationObjectFaultedException ex)
            {
                System.Console.WriteLine(ex.ToString());
                System.Console.WriteLine();
                System.Console.WriteLine("Please restart with administrator rights");
                System.Console.ReadLine();

                logger.FatalException("Please restart with administrator rights. Continue work.", ex);
            }
            catch (Exception ex)
            {
                logger.FatalException("root fatal", ex);
                throw;
            }            
        }
    }
}
