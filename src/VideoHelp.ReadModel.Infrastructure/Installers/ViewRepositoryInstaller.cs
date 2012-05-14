using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace VideoHelp.ReadModel.Infrastructure.Installers
{
    public class ViewRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyContaining<ViewRepository>()
                                   .BasedOn(typeof (IViewFactory<,>)).WithService.AllInterfaces());

            container.Register(AllTypes.FromAssemblyContaining<ViewRepository>()
                                   .BasedOn(typeof (IViewFactory<>)).WithService.AllInterfaces());

            container.Register(Component.For<IViewRepository>().Instance(new ViewRepository(container.Resolve)));
        }
    }
}