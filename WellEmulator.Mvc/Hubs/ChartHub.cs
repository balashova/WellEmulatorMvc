using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Hubs
{
    [HubName("chart")]
    public class ChartHub : Hub
    {
        public static void HistorianDataChanged(HistorianValue[] historianValues)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            context.Clients.All.HistorianDataChanged(historianValues);
        }

        public static void PdgtmDataChanged(PdgtmValue[] pdgtmValues)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            context.Clients.All.PdgtmDataChanged(pdgtmValues);
        }

        private static void SM(string m)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            context.Clients.All.SendMessage(m);
        }

        public void SendMessage(string message)
        {
            SM(message);
        }
    }
}