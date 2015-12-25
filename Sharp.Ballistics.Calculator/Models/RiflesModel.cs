using GNUBallisticsLibrary;
using Raven.Client;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Calculator.Models
{
    public class RiflesModel : StoredItemModel<Rifle>
    {
        public RiflesModel(IDocumentStore store) : base(store)
        {
        }
    }
}
