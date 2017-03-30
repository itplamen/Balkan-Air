namespace BalkanAir.Web
{
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Optimization;
    using System.Web.Routing;

    using App_Start;
    using Common;

    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.BALKAN_AIR_API));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}