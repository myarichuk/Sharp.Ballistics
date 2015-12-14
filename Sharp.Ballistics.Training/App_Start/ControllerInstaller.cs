using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharp.Ballistics.Training.App_Start
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .Pick().If(t => t.Name.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
                    .Configure(configurer => configurer.Named(configurer.Implementation.Name))
                    .WithServiceSelf()
                    .WithServiceBase()
                    .WithServiceAllInterfaces()
                    .LifestylePerWebRequest());
        }
    }
}