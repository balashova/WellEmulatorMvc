using System;
using System.Collections.Generic;
using System.Web.Http;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Api
{
    public class ServiceController : ApiController
    {
        private readonly WellEmulatorClient _client = new WellEmulatorClient();

        [ActionName("startEmulator")]
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

        [ActionName("stopEmulator")]
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

        [ActionName("applySettings")]
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

        [ActionName("startReplication")]
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

        [ActionName("stopReplication")]
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

        [ActionName("getWitsmlObjects")]
        public IEnumerable<string> GetWitsmlObjects(string groupName)
        {
            return _client.GetWitsmlObjects(groupName);
        }

        [ActionName("getWitsmlElements")]
        public IEnumerable<string> GetWitsmlElements(string groupName, string objectName)
        {
            return _client.GetWitsmlElements(groupName, objectName);
        }

        [ActionName("createTag")]
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

        [ActionName("getTag")]
        public Tag GetTag(int tagId)
        {
            return _client.GetTag(tagId);
        }

        [ActionName("getAllTagsFromSettings")]
        public IEnumerable<Tag> GetTags()
        {
            return _client.GetSettingsTags();
        }

        [ActionName("removeTag"), HttpGet]
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

        [ActionName("getHistorianTags")]
        public IEnumerable<string> GetHistTags(string wellName)
        {
            return _client.GetNotMappedHistTags(wellName);
        }

        [ActionName("getPdgtmTags")]
        public IEnumerable<string> GetPdgtmTags(string wellName)
        {
            return _client.GetNotMappedPdgtmTags(wellName);
        }

        [ActionName("addMappingItem")]
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

        [ActionName("removeMapingItems")]
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

        [ActionName("getMappings")]
        public IEnumerable<MapItem> GetMappings()
        {
            return _client.GetMappings();
        }
    }
}
