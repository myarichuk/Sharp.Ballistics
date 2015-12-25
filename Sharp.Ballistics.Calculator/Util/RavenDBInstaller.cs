using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using UnitsNet.Serialization.JsonNet;
using System;
using System.Windows;

namespace Sharp.Ballistics.Calculator.Bootstrap
{

    public class RavenDBInstaller : IWindsorInstaller
    {       
        void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IDocumentStore>().UsingFactoryMethod(() =>
            {
                EmbeddableDocumentStore documentStore = null;
                try {
                    documentStore = new EmbeddableDocumentStore
                    {
                        UseEmbeddedHttpServer = true,
                        DefaultDatabase = Constants.DatabaseName
                    };

                    documentStore.Initialize();
                    documentStore.DatabaseCommands
                                 .GlobalAdmin.EnsureDatabaseExists(Constants.DatabaseName);

                    documentStore.Conventions.CustomizeJsonSerializer = serializer =>
                    {
                        serializer.Converters.Add(new UnitsNetJsonConverter());
                    };
                }
                catch(Exception e)
                {
                    //ravendb failed to initialize - cannot continue
                    MessageBox.Show(e.ToString());
                    Application.Current.Shutdown(-1);
                }

                return documentStore;
            }).LifestyleSingleton());
        }
    }
}
