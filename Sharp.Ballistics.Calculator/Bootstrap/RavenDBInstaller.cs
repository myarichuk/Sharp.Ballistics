using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System.Reflection;

namespace Sharp.Ballistics.Calculator.Bootstrap
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
                    UseEmbeddedHttpServer = true
                };

                documentStore.Initialize();

                IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), documentStore);

                return documentStore;
            }).LifestyleSingleton());            
        }
    }
}
