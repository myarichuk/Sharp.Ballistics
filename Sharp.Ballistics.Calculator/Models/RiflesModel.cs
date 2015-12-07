using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.Models
{
    public class RiflesModel
    {
        private readonly IDocumentStore documentStore;

        public RiflesModel(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }
    }
}
