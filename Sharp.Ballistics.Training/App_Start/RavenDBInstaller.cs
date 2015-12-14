using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sharp.Ballistics.Training.App_Start
{
    public class RavenDBInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod(() =>
            {
                var documentStore = new EmbeddableDocumentStore
                {
                    DefaultDatabase = Constants.DatabaseName,
                    DataDirectory = Constants.DataFolder,                    
                    UseEmbeddedHttpServer = true,   
                };

                documentStore.Configuration.Port = 9090;
                documentStore.Initialize();
                return documentStore;
            }).LifestyleSingleton(),
            Component.For<IDocumentSession>().UsingFactoryMethod(kernel =>
            {
                var documentStore = kernel.Resolve<IDocumentStore>();
                return documentStore.OpenSession();
            }).LifestylePerWebRequest(),
            Component.For<IAsyncDocumentSession>().UsingFactoryMethod(kernel =>
            {
                var documentStore = kernel.Resolve<IDocumentStore>();
                return documentStore.OpenAsyncSession();
            }).LifestylePerWebRequest());
        }
    }
}