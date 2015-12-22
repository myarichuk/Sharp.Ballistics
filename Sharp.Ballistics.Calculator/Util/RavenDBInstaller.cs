using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System.Reflection;
using UnitsNet.Serialization.JsonNet;
using System;
using Raven.Imports.Newtonsoft.Json;

namespace Sharp.Ballistics.Calculator.Bootstrap
{   
    
    public class RavenDBInstaller : IWindsorInstaller
    {       
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod(() =>
            {
                var documentStore = new EmbeddableDocumentStore
                {
                    DefaultDatabase = Constants.DatabaseName,
                    UseEmbeddedHttpServer = true
                };

                documentStore.Initialize();
                documentStore.Conventions.CustomizeJsonSerializer = serializer =>
                {
                    serializer.Converters.Add(new UnitsNetJsonConverter());
                };

                IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStore);
                return documentStore;
            }).LifestyleSingleton());
        }
    }
}
