using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using WellEmulator.Mvc.ServiceListeners;

[assembly: OwinStartup(typeof(WellEmulator.Mvc.Startup))]

namespace WellEmulator.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
