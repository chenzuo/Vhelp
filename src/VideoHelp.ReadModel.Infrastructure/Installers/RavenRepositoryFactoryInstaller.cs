using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class RavenRepositoryFactoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var factory = new RavenRepositoryFactory(container.Resolve<IDocumentStore>());
            container.Register(Component.For<IRepositoryFactory>().Instance(factory).LifeStyle.Singleton);
        }
    }
}