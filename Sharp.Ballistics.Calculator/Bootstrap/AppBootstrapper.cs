using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Sharp.Ballistics.Calculator.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Windows;
using Castle.Windsor.Installer;
using Sharp.Ballistics.Calculator.Models;

namespace Sharp.Ballistics.Calculator.Bootstrap
{
    
    public class AppBootstrapper : BootstrapperBase
    {
		private WindsorContainer container;

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
		{
            container = new WindsorContainer();

		    container.AddFacility<EventRegistrationFacility>();
            
            container.Register(
		        Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(),
		        Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                                          .WithServiceDefaultInterfaces()
                                          .WithServiceSelf()
                                          .LifestyleTransient(),
                Classes.FromThisAssembly().InNamespace("Sharp.Ballistics.Calculator.Models")
                                          .WithServiceDefaultInterfaces()
                                          .WithServiceSelf()
                                          .LifestyleTransient()
                );

            container.Install(FromAssembly.This());
		}

        internal void Dispose()
        {
            container.Dispose();
        }

        protected override void BuildUp(object instance)
        {
        }

        protected override object GetInstance(Type serviceType, string key)
		{
            if (string.IsNullOrEmpty(key))
            {
                return container.Resolve(serviceType);
            }
		    return container.Resolve(key, serviceType);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return container.ResolveAll(serviceType).Cast<object>();
		}
	}
}