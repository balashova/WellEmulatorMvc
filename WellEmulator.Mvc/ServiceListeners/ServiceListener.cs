using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using Microsoft.Owin.Logging;
using NLog;
using WellEmulator.Mvc.Hubs;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.ServiceListeners
{
    public class ServiceClient : WellEmulatorClient
    {
        public ServiceClient()
            : base(new InstanceContext(new ServiceListener()))
        {
        }
    }

    public class ServiceObservableClient : WellEmulatorClient, IDisposable
    {
        public ServiceObservableClient()
            : base(new InstanceContext(new ServiceListener()))
        {
            Connect();
        }

        public void Dispose()
        {
            Disconnect();
            Close();
        }
    }

    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class ServiceListener : IWellEmulatorCallback
    {
        public void OnPdgtmDataChanged(PdgtmValue[] pdgtmValues)
        {
            ChartHub.PdgtmDataChanged(pdgtmValues);
        }

        public void OnHistorianDataChanged(HistorianValue[] historianValues)
        {
            ChartHub.HistorianDataChanged(historianValues);
        }
    }
}