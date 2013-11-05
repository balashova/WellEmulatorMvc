using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellEmulatorService
{
    public class WellEmulator : IWellEmulator
    {
        public string MethodForTest(string request)
        {
            return "response: " + request;
        }
    }
}
