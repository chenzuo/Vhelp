using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Raven.Client;
using Raven.Client.Document;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class RavenInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var documentStore = new DocumentStore { ConnectionStringName = "Raven" };
            documentStore.Initialize();

            container.Register(Component.For<IDocumentStore>().Instance(documentStore).LifeStyle.Singleton);
        }
    }
}