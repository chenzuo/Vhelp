using System;
using System.Linq;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure
{
    public class RavenRepository : IRepository
    {
        private readonly IDocumentSession _session;

        public RavenRepository(IDocumentStore documentStore)
        {
            _session = documentStore.OpenSession();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _session.Query<T>();
        }

        public T GetById<T>(Guid id) where T : class
        {
            return _session.Load<T>(id);
        }

        public void Store<T>(T value) where T : class
        {
            _session.Store(value);
        }

        public void Dispose()
        {
            _session.SaveChanges();
            _session.Dispose();
        }
    }
}