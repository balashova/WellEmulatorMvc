using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using WellEmulatorMvc.Parsers;

namespace WellEmulatorMvc.Controllers
{
    public class EmulatorController : Controller
    {
        public ActionResult Control()
        {
            return View();
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
