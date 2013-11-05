using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new WellEmulatorClient())
            {
                Console.WriteLine(client.MethodForTest("a"));
                Console.ReadLine();
            }
        }
    }
}
