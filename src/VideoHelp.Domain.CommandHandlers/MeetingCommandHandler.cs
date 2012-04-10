using System;
using CommonDomain.Persistence;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.CommandHandlers
{
    public class MeetingCommandHandler : ICommandHandler<AttachMediaContent>
    {
        private readonly IRepository _repository;

        public MeetingCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AttachMediaContent command)
        {
            var meeting = _repository.GetById<Meeting>(command.AggregateId);
           // meeting.AttachMediaContent(command.Content);
            _repository.Save(meeting, Guid.NewGuid());
        }
    }
}