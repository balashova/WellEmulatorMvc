using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WellEmulator.Mvc.ServiceListeners;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Controllers
{
    public class ChartController : Controller
    {
        private readonly WellEmulatorClient _client = new ServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public int GetQueryRange()
        {
            return _client.GetQueryRange().Minutes;
        }
    }
}
