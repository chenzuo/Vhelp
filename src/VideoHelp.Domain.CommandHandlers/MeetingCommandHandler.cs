using System;
using CommonDomain.Persistence;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.CommandHandlers
{
    public class MeetingCommandHandler : ICommandHandler<CreateMeeting>
    {
        private readonly IRepository _repository;

        public MeetingCommandHandler(IRepository repository)
        {
            _repository = repository;
        }


        public void Handle(CreateMeeting command)
        {
            var meeting = Meeting.Create(command.UserId, command.Name);
            _repository.Save(meeting, Guid.NewGuid());
        }
    }
}