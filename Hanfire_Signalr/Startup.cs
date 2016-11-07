
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Hangfire_Signalr.Startup))]
namespace Hangfire_Signalr
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //允許Cross Domain
            app.Map("/habfireSR", map =>
            {
                var hubConfiguration = new HubConfiguration { EnableJSONP=true};
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
