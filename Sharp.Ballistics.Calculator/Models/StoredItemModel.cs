using Raven.Client;
using System;
using System.Collections.Generic;
using Sharp.Ballistics.Abstractions;
using Raven.Abstractions.Commands;

namespace Sharp.Ballistics.Calculator.Models
{
    public abstract class StoredItemModel<T> where T : class, IHaveId
    {
        private readonly IDocumentStore documentStore;

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

        public IEnumerable<T> All()
        {
            using (var session = documentStore.OpenSession())
            using (var stream = session.Advanced.Stream(session.Query<T>()))
            {
                do
                {
                    yield return stream.Current.Document;
                } while (stream.MoveNext());
            }
        }
               
    }
}
