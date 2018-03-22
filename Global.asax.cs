using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShriVivah
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
        {
            this.BeginRequest += MvcApplication_BeginRequest;
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //RouteTable.Routes.MapHubs("~/signalr", new Microsoft.AspNet.SignalR.HubConfiguration());
        }

        void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            var UserName = this.Context.Request.Headers["UserName"];
            var Password = this.Context.Request.Headers["Password"];
        }
    }
}
