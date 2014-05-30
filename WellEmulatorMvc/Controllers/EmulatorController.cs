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
            var wells = new List<Well>() //_client.GetWellList(); TODO change on deploy!
                {
                    new Well() { Name = "a", Id = 1 }
                };
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
            var tags = new List<Tag> // _client.GetTags();  TODO change on deploy!
                {
                    new Tag
                        {
                            Id = 1,
                            Delta = 2,
                            Name = "t1",
                            MaxValue = 99,
                            MinValue = 12,
                            Group = "g1",
                            WellName = "w1",
                            Object = "o1",
                            Value = 15
                        },
                    new Tag
                        { 
                            Id = 2,
                            Delta = 33,
                            Name = "t2",
                            MaxValue = 66,
                            MinValue = 1,
                            Group = "g2",
                            WellName = "w2",
                            Object = "o2",
                            Value = 44
                        }
                }.ToArray();
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
