using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class ReadRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var repository = new RavenReadRepository(container.Resolve<IDocumentStore>());
            container.Register(Component.For<IReadRepository>().Instance(repository));
        }
    }
}