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
            var historianProvider = new HistorianAdapter();
            var a = historianProvider.GetTagList();

            historianProvider.AddTag(new Tag()
            {
                Name = "tp4",
                WellName = "well_1"
            });

            historianProvider.RemoveTag(new Tag()
            {
                Name = "tp4",
                WellName = "well_1"
            });

            //var b = historianProvider.GetTag("tp3");

            Console.WriteLine();
        }
    }
}
