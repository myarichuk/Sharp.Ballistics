using Raven.Client;
using Sharp.Ballistics.Abstractions;
using System;
using System.Linq;

namespace Sharp.Ballistics.Calculator.Models
{
    public class ScopesModel : StoredItemModel<Scope>
    {
        public ScopesModel(IDocumentStore store) : base(store)
        {
        }

        public Scope ByName(string name)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Query<Scope>()
                              .FirstOrDefault(c =>
                                    c.Name.Equals(name,
                                        StringComparison.InvariantCultureIgnoreCase))
;
            }
        }
    }
}
