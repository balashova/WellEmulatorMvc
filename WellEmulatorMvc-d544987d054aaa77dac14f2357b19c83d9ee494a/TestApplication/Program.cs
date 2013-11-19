using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellEmulator.Core;
using WellEmulator.Models;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var emulator = new Emulator();
            emulator.AddTag(new Tag
            {
                Delta = 10,
                Group = "Witsml",
                MaxValue = 100,
                MinValue = 0,
                Name = "tp3",
                Object = "Log",
                Value = 50
            });
            emulator.Start();

            Console.ReadLine();
        }
    }
}
