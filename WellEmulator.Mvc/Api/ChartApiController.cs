using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WellEmulator.Mvc.ServiceListeners;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Api
{
    public class ChartApiController : ApiController
    {
        private readonly WellEmulatorClient _client = new ServiceClient();


        [ActionName("getNumberDbValues")]
        public int GetNumberDbValues()
        {
            return _client.GetNumberDbValues();
        }
        
        [ActionName("setNumberDbValues")]
        public void SetNumberDbValues(int number)
        {
            _client.SetNumberDbValues(number);
        }
    }
}
