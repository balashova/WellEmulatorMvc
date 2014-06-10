using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.ServiceListeners
{
    public class ServiceListenerClient : WellEmulatorClient, IDisposable
    {
        public ServiceListenerClient()
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
            throw new NotImplementedException();
        }

        public void OnHistorianDataChanged(HistorianValue[] historianValues)
        {
            throw new NotImplementedException();
        }
    }
}