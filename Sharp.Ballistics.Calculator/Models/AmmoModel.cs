using Raven.Client;
using Sharp.Ballistics.Abstractions;

namespace Sharp.Ballistics.Calculator.Models
{
    public class AmmoModel : StoredItemModel<Cartridge>
    {
        public AmmoModel(IDocumentStore store) : base(store)
        {
        }
    }
}
