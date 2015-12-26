using Raven.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using Sharp.Ballistics.Abstractions;
using Raven.Abstractions.Commands;
using RavenConstants = Raven.Abstractions.Data;

namespace Sharp.Ballistics.Calculator.Models
{
    public abstract class StoredItemModel<T> where T : class, IHaveId
    {
        protected readonly IDocumentStore documentStore;

        protected StoredItemModel(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public void InsertOrUpdate(params T[] items)
        {
            using (var session = documentStore.OpenSession())
            {
                foreach (var item in items)
                    session.Store(item);
                session.SaveChanges();
            }
        }

        public void Delete(params T[] items)
        {
            using (var session = documentStore.OpenSession())
            {
                foreach (var item in items)
                {
                    if (String.IsNullOrWhiteSpace(item.Id))
                        throw new InvalidOperationException("To delete an item, it must have a non-empty id");

                    session.Advanced.Defer(new DeleteCommandData
                    {
                        Key = item.Id
                    });
                }

                session.SaveChanges();
            }
        }

        public IEnumerable<T> All(int start = 0, int take = 1024)
        {
            using (var session = documentStore.OpenSession())
            {
                var indexName = RavenConstants.Constants.DocumentsByEntityNameIndex;

                var tag = documentStore.Conventions.FindTypeTagName(typeof(T));
                var query = session.Advanced.DocumentQuery<T>(indexName)
                                            .WaitForNonStaleResultsAsOfNow()
                                            .Where($"Tag:{tag}")
                                            .Skip(start)
                                            .Take(take);

                return query.ToList();
            }
        }
               
    }
}
