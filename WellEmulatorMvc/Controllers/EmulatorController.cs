using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using NUnit.Framework;
using WellEmulatorMvc.Models;
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
            var wells = _client.GetWellList();
            var objects = _client.GetWitsmlObjects("WITSML");
            var tags = _client.GetWitsmlElements("WITSML", objects.First());

            var model = new NewTagViewModel
                {
                    Wells = wells,
                    WitsmlObjects = objects,
                    WitsmlElements = tags
                };
            return View(model);
        }

        public ActionResult Tags()
        {
            var tags = _client.GetTags();
            return View(tags);
        }

        //public ActionResult Wells()
        //{
        //    return View();
        //}

        public ActionResult Mapping()
        {
            var maps = _client.GetMappings();
            return View(maps);
        }
    }
}
