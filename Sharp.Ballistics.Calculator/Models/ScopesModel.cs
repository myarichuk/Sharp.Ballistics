using Raven.Client;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Calculator.Models
{
    public class ScopesModel : StoredItemModel<Scope>
    {
        public ScopesModel(IDocumentStore store) : base(store)
        {
        }
    }
}
