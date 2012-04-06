using System;
using CommonDomain.Persistence;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.CommandHandlers
{
    public class MeetingCommandHandler : ICommandHandler<AddVideoStream>
    {
        private readonly IRepository _repository;

        public MeetingCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AddVideoStream command)
        {
            var meeting = _repository.GetById<Meeting>(command.AggregateId);
            meeting.AddVideoStream(command.UserId, command.StreamId);
            _repository.Save(meeting, Guid.NewGuid());
        }
    }
}