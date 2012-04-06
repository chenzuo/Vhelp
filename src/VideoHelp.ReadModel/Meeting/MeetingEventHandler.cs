using VideoHelp.Domain.Messages.Events.Meeting;

namespace VideoHelp.ReadModel.Meeting
{
    public class MeetingEventHandler
    {
        private readonly IWriteRepository _writeRepository;
        private readonly IReadRepository _readRepository;

        public MeetingEventHandler(IWriteRepository writeRepository, IReadRepository readRepository)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
        }

        public void Handle(MeetingCreated @event)
        {
            _writeRepository.Add(new MeetingView(@event.AggregateId, @event.OwnerId, @event.Name, @event.CreationDate));
            _writeRepository.SaveChanges();
        }

        public void Handle(VideoStreamAdded @event)
        {
            var meeting =_readRepository.GetById<MeetingView>(@event.AggregateId);
            if(@event.UserId == meeting.Owner)
            {
                meeting.MainVideoStream = @event.StreamId;
            }
            else
            {
                meeting.VideoStreams.Add(@event.UserId, @event.StreamId);
            }
            _readRepository.SaveChanges();
        } 
    }
}