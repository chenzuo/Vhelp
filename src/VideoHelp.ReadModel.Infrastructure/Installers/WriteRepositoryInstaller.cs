using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class WriteRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var repository = new RavenWriteRepository(container.Resolve<IDocumentStore>());
            container.Register(Component.For<IWriteRepository>().Instance(repository));
        }
    }
}