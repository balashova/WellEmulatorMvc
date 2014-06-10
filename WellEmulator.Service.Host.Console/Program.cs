using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceModel;
using NLog;

namespace WellEmulator.Service.Host.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                var serviceHost = new ServiceHost(typeof(WellEmulator));
                serviceHost.Open();

                System.Console.WriteLine("The service is ready.");
                System.Console.WriteLine("Press the Enter key to terminate service.");
                System.Console.ReadLine();
            }
            catch (CommunicationObjectFaultedException ex)
            {
                logger.Fatal("Restart with administrator rights.", ex);

                System.Console.WriteLine(ex.ToString());
                System.Console.WriteLine();
                System.Console.WriteLine("Restart with administrator rights.");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                logger.Fatal("Service loading failed.", ex);
                throw;
            }            
        }
    }
}
