using Castle.Windsor;
using Castle.Windsor.Installer;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharp.Ballistics.Training.App_Start
{
    public class NancyBootstrapper : WindsorNancyBootstrapper
    {
        protected override void ApplicationStartup(IWindsorContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override IWindsorContainer GetApplicationContainer()
        {
            return MvcApplication.Container;
        }

        protected override void RequestStartup(IWindsorContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
        }
    }
}