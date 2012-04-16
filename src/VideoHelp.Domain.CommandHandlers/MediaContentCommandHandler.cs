using System;
using CommonDomain.Persistence;
using VideoHelp.Domain.Messages.Commands;

namespace VideoHelp.Domain.CommandHandlers
{
    public class MediaContentCommandHandler : ICommandHandler<AttachCameraStream>
    {
        private readonly IRepository _repository;

        public MediaContentCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AttachCameraStream command)
        {
            var mediaContent = CameraStream.Create(command.AggregateId, command.MeetingId, command.UserId, command.StreamLink);
            _repository.Save(mediaContent, Guid.NewGuid());
        }
    }
}