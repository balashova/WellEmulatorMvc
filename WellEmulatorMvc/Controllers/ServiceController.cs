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
    }
}
