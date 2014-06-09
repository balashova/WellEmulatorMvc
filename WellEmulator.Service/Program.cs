using System.ServiceProcess;

namespace WellEmulator.Service
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new WellEmulatorService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
