using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WellEmulatorServiceClient.ServiceReference;

namespace WellEmulatorMvc.Controllers
{
    public class ServiceController : ApiController
    {
        private readonly WellEmulatorClient _client = new WellEmulatorClient();

        [ActionName("start")]
        public string StartEmulator()
        {
            try
            {
                _client.Start();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        [ActionName("stop")]
        public string StopEmulator()
        {
            try
            {
                _client.Stop();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        [ActionName("apply")]
        public string SetSettings(Dictionary<string, double> dict)
        {
            try
            {
                _client.SetSettings(new Settings()
                {
                    ReplicationPeriod = TimeSpan.FromMilliseconds(dict["ReplicationPeriod"]),
                    ReportAutoSavePeriod = TimeSpan.FromMilliseconds(dict["ReportAutoSavePeriod"]),
                    SamplingRate = TimeSpan.FromMilliseconds(dict["SamplingRate"]),
                    ValuesDelay = TimeSpan.FromMilliseconds(dict["ValuesDelay"])
                });
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        [ActionName("repon")]
        public string StartReplication()
        {
            try
            {
                _client.EnableReplication();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        [ActionName("repoff")]
        public string StopReplication()
        {
            try
            {
                _client.DisableReplication();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        [ActionName("getobjs")]
        public IEnumerable<string> GetWitsmlObjects(string groupName)
        {
            return _client.GetWitsmlObjects(groupName);
        }

        [ActionName("getels")]
        public IEnumerable<string> GetWitsmlElements(string groupName, string objectName)
        {
            return _client.GetWitsmlElements(groupName, objectName);
        }

        [ActionName("newtag")]
        public string CreateNewTag(Tag tag)
        {
            try
            {
                _client.AddTag(tag);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ActionName("gettag")]
        public Tag GetTag(int tagId)
        {
            return _client.GetTag(tagId);
        }

        [ActionName("gettags")]
        public IEnumerable<Tag> GetTags()
        {
            return _client.GetSettingsTags();
        }

        [ActionName("removetag"), HttpGet]
        public string RemoveTag(int tagId)
        {
            try
            {
                _client.RemoveTag(tagId);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + "\n" + ex.StackTrace;
            }
        }

        //[ActionName("getpdgtmwells")]
        //public IEnumerable<string> GetPdgtmWells()
        //{
        //    return _client.GetPdgtmWells().Select(w => w.Name);
        //}

        //[ActionName("gethistwells")]
        //public IEnumerable<string> GetHistorianWells()
        //{
        //    return _client.GetHistWells();
        //}

        [ActionName("gethisttags")]
        public IEnumerable<string> GetHistTags(string wellName)
        {
            return _client.GetNotMappedHistTags(wellName);
        }

        [ActionName("getpdgtmtags")]
        public IEnumerable<string> GetPdgtmTags(string wellName)
        {
            return _client.GetNotMappedPdgtmTags(wellName);
        }

        [ActionName("addmap")]
        public string AddMapItem(MapItem mapItem)
        {
            try
            {
                _client.AddMapItem(mapItem);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ActionName("removemaps")]
        public string RemoveMapItems(List<int> ids)
        {
            try
            {
                _client.RemoveMapItems(ids.ToArray());
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [ActionName("getmappings")]
        public IEnumerable<MapItem> GetMappings()
        {
            return _client.GetMappings();
        }
    }
}
