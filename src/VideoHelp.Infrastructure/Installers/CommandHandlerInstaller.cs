using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CommonDomain.Persistence;
using VideoHelp.Domain.CommandHandlers;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Infrastructure.Installers
{
    public class CommandHandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var bus = container.Resolve<ICommandBus>();
            /*
            container.Register(AllTypes.FromAssemblyNamed(_assemblyName)
                                   .Where(x => x.GetInterface(typeof(ICommandHandler<>).Name) != null).WithService.AllInterfaces());



            var handlers = container.ResolveAll(typeof (ICommandHandler<>));
            */

            var userCommandHandler = new UserCommandHandler(container.Resolve<IRepository>());
            var meetingCommandHandler = new MeetingCommandHandler(container.Resolve<IRepository>());

            bus.RegisterCommandHandler<CreateMeeting>(userCommandHandler);
            bus.RegisterCommandHandler<CreateUser>(userCommandHandler);
            bus.RegisterCommandHandler<UpdateUserState>(userCommandHandler);
            bus.RegisterCommandHandler<AddVideoStream>(meetingCommandHandler);
        }
    }
}