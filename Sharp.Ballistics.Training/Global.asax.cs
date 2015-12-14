using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sharp.Ballistics.Training
{
    public class MvcApplication : System.Web.HttpApplication
    {
#pragma warning disable CC0033 // Dispose Fields Properly -> disposed in Application_End()
        public static readonly IWindsorContainer Container = new WindsorContainer();
#pragma warning restore CC0033 // Dispose Fields Properly

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Container.Install(FromAssembly.This()); //all installers from this assembly
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container));
        }

        protected void Application_End()
        {
            Container.Dispose();
        }
    }
}
