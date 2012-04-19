using System;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure
{
    public class RavenRepositoryFactory : IRepositoryFactory
    {
        private readonly IDocumentStore _documentStore;

        public RavenRepositoryFactory(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IRepository Create()
        {
            return new RavenRepository(_documentStore);
        }
    }
}