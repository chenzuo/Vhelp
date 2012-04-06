using System;

namespace VideoHelp.Domain
{
    public class VideoStream
    {
        public VideoStream(string streamId, Guid streamOwnerId)
        {
            StreamOwnerId = streamOwnerId;
            StreamId = streamId;
        }

        public String StreamId { get; private set; }
        public Guid StreamOwnerId { get; private set; }
    }
}