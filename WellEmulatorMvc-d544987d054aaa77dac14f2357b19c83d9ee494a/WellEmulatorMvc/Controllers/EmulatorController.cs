using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WellEmulatorMvc.Parsers;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorMvc.Controllers
{
    public class EmulatorController : Controller
    {
        private readonly WellEmulatorClient _client = new WellEmulatorClient();

        public ActionResult Control()
        {
            string outMessage = null;
            var settings = _client.GetSettings(ref outMessage);
            return View(settings);
        }

        public ActionResult NewTag()
        {
            return View();
        }

        public ActionResult Tags()
        {
            return View();
        }

        public ActionResult Wells()
        {
            return View();
        }
    }
}
