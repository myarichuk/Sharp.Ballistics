using GNUBallisticsLibrary;
using Raven.Client;
using Sharp.Ballistics.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharp.Ballistics.Calculator.Models
{
    public class RiflesModel : StoredItemModel<Rifle>
    {
        public RiflesModel(IDocumentStore store) : base(store)
        {
        }

        public IEnumerable<Rifle> RiflesByScopeName(string name)
        {
            using (var session = documentStore.OpenSession())
            {
                var rifles = session.Query<Rifle>()
                                    .Where(r => r.Scope.Name.Equals(name,
                                            StringComparison.InvariantCultureIgnoreCase))
                                    .ToList();

                return rifles;
            }
        }

        public IEnumerable<Rifle> RiflesByCartridgeName(string name)
        {
            using (var session = documentStore.OpenSession())
            {
                var rifles = session.Query<Rifle>()
                                    .Where(r => r.Cartridge.Name.Equals(name, 
                                            StringComparison.InvariantCultureIgnoreCase))
                                    .ToList();

                return rifles;
            }
        }
    }
}
