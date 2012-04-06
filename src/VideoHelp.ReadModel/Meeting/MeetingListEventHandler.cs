using VideoHelp.Domain.Messages.Events.Meeting;
using VideoHelp.ReadModel.Users;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingListEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;

        public MeetingListEventHandler(IWriteRepository writeRepository, IReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public void Handle(MeetingCreated @event)
        {
            _writeRepository.Add(new MeetingListView(@event.AggregateId, @event.OwnerId, _readRepository.GetById<UserView>(@event.OwnerId).Nick, @event.Name, @event.CreationDate));
            _writeRepository.SaveChanges();
        }
    }
}