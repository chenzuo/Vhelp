using System;
using System.Linq;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure
{
    public class RavenReadRepository : IReadRepository, IDisposable
    {
        private readonly IDocumentSession _session;

        public RavenReadRepository(IDocumentStore documentStore)
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

        public void SaveChanges()
        {
            _session.SaveChanges();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}