using System;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure
{
    public class RavenWriteRepository : IWriteRepository, IDisposable 
    {
        private readonly IDocumentSession _session;

        public RavenWriteRepository(IDocumentStore documentStore)
        {
            _session = documentStore.OpenSession();
        }

        public void Add<T>(T value)
        {
            _session.Store(value);
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