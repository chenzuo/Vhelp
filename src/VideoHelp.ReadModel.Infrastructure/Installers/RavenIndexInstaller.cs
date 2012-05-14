using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Indexes;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class RavenIndexInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            IndexCreation.CreateIndexes(typeof(ViewRepository).Assembly, container.Resolve<IDocumentStore>());   
        }
    }
}