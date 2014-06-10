using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using WellEmulator.Mvc.Models;
using WellEmulator.Mvc.ServiceListeners;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Controllers
{
    public class EmulatorController : Controller
    {
        private readonly WellEmulatorClient _client = new ServiceClient();

        public ActionResult Control()
        {
            var settings = _client.GetSettings();
            return View(settings);
        }

        public ActionResult NewTag()
        {
            var wells = _client.GetPdgtmWells();
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
            var tags = _client.GetSettingsTags();
            return View(tags);
        }

        public ActionResult Mapping()
        {
            var histWells = _client.GetHistWells();
            var pdgtmWells = _client.GetPdgtmWells().Select(w => w.Name).ToList();
            var histTags = _client.GetNotMappedHistTags(histWells.FirstOrDefault());
            var pdgtmTags = _client.GetNotMappedPdgtmTags(pdgtmWells.FirstOrDefault());
            var mapItems = _client.GetMappings();

            var model = new MappingViewModel()
                {
                    HistTags = histTags,
                    HistWells = histWells,
                    PdgtmTags = pdgtmTags,
                    PdgtmWells = pdgtmWells,
                    MapItems = mapItems
                };
            return View(model);
        }
    }
}
