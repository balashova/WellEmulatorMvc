using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WellEmulatorMvc.Models;
using WellEmulatorMvc.Parsers;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorMvc.Controllers
{
    public class EmulatorController : Controller
    {
        private readonly WellEmulatorClient _client = new WellEmulatorClient();

        public ActionResult Control()
        {
            var settings = _client.GetSettings();
            return View(settings);
        }

        public ActionResult NewTag()
        {
            var model = new NewTagViewModel()
                {
                    Wells = _client.GetWellList()
                };
            return View(model);
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
