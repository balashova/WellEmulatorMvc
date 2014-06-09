using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using NLog;

namespace WellEmulator.Service
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Trace("Starting...");
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new WellEmulatorService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                logger.FatalException("Service was not started.", ex);
                throw;
            }
        }
    }
}
