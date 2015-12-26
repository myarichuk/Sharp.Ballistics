using Raven.Client;
using Sharp.Ballistics.Abstractions;
using System;
using System.Linq;
namespace Sharp.Ballistics.Calculator.Models
{
    public class AmmoModel : StoredItemModel<Cartridge>
    {
        public AmmoModel(IDocumentStore store) : base(store)
        {
        }

        public Cartridge ByName(string name)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Query<Cartridge>()
                              .FirstOrDefault(c => 
                                    c.Name.Equals(name, 
                                        StringComparison.InvariantCultureIgnoreCase))
;
            }
        }
    }
}
