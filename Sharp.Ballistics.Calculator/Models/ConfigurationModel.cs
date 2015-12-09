using Raven.Client;

namespace Sharp.Ballistics.Calculator.Models
{

    public class ConfigurationModel
    {
        private readonly IDocumentStore documentStore;

        public ConfigurationModel(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void Load()
        {
            using (var session = documentStore.OpenSession())
            {
                var unitsConfig = session.Load<UnitsConfiguration>(Constants.UnitsConfigurationId);
                Units = unitsConfig ?? UnitsConfiguration.Metric;
            }
        }

        public void Save()
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(Units, Constants.UnitsConfigurationId);
                session.SaveChanges();
            }
        }

        public UnitsConfiguration Units { get; private set; }
    }
}
