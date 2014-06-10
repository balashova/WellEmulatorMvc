using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using WellEmulator.Mvc.ServiceListeners;
using WellEmulator.Service.Client.ServiceReference;

namespace WellEmulator.Mvc.Hubs
{
    [HubName("chart")]
    public class ChartHub : Hub
    {
        public static void HistorianDataChanged(HistorianValue[] historianValues)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            context.Clients.Group("historian").dataChanged(historianValues);
        }

        public static void PdgtmDataChanged(PdgtmValue[] pdgtmValues)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ChartHub>();
            context.Clients.Group("pdgtm").dataChanged(pdgtmValues);
        }
        public void SubscribeOn(string data)
        {
            // NOTE: this is not persisted - ....
            Groups.Add(Context.ConnectionId, data);
        }

        public void UnSubscribeFrom(string data)
        {
            // NOTE: this is not persisted - ....
            Groups.Remove(Context.ConnectionId, data);
        }

        public override Task OnConnected()
        {
            Groups.Add(Context.ConnectionId, "historian");
            new ServiceObservableClient();   
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            Groups.Remove(Context.ConnectionId, "historian");
            Groups.Remove(Context.ConnectionId, "pdgtm");
            return base.OnDisconnected();
        }
    }
}