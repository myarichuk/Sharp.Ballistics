using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Sharp.Ballistics.Calculator.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Windows;

namespace Sharp.Ballistics.Calculator.Bootstrap
{
    
    public class AppBootstrapper : BootstrapperBase
    {
		private WindsorContainer _container;

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override void Configure()
		{
            _container = new WindsorContainer();

		    _container.AddFacility<EventRegistrationFacility>();
            
            _container.Register(
		        Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(),
		        Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),
                Classes.FromThisAssembly().InSameNamespaceAs<ShellViewModel>()
                                          .WithServiceDefaultInterfaces()
                                          .WithServiceSelf()
                                          .LifestyleTransient()
		        );
		}

        internal void Dispose()
        {
            _container.Dispose();
        }

        protected override void BuildUp(object instance)
        {
        }

        protected override object GetInstance(Type serviceType, string key)
		{
            if (string.IsNullOrEmpty(key))
            {
                return _container.Resolve(serviceType);
            }
		    return _container.Resolve(key, serviceType);
		}

		protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return _container.ResolveAll(serviceType).Cast<object>();
		}
	}
}