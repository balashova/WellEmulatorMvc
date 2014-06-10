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
    public class ChartController : ApiController
    {
        private readonly WellEmulatorClient _client = new ServiceClient();

        [ActionName("getQueryRange")]
        public int GetQueryRange()
        {
            return _client.GetQueryRange().Minutes;
        }

        [ActionName("setQueryRange"), HttpPost, HttpGet]
        public void SetQueryRange(int minutes)
        {
            _client.SetQueryRange(TimeSpan.FromMinutes(minutes));
        }
    }
}
