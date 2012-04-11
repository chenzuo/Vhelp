using System;
using CommonDomain.Core;
using VideoHelp.Domain.Messages.Events.MediaContent;

namespace VideoHelp.Domain
{
    public class CameraStream : AggregateBase, IMediaContent
    {
        public static CameraStream Create(Guid id, Guid meetingId, Guid ownerUser, string streamLink)
        {
            return new CameraStream(id, meetingId, ownerUser, streamLink);
        }

        public CameraStream(Guid id, Guid meetingId, Guid ownerUser, string streamLink)
        {
            RaiseEvent(new CameraStreamCreated(id, meetingId, ownerUser, streamLink));
        }

        public CameraStream(){}

        public String StreamLink { get; private set; }
        public Guid OwnerUser { get; private set; }
        public Guid MeetingId { get; private set; }

        private void Apply(CameraStreamCreated @event)
        {
            Id = @event.AggregateId;
            OwnerUser = @event.OwnerUser;
            MeetingId = @event.MeetingId;
            StreamLink = @event.StreamLink;
        }
    }
}