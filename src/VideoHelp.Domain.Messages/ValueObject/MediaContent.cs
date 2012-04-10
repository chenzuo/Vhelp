using System;
using System.Runtime.Serialization;

namespace VideoHelp.Domain.Messages.ValueObject
{
    [DataContract]
    [KnownType(typeof(CameraStream))] 
    public abstract class MediaContent
    {
        protected MediaContent(Guid id, Guid ownerUser)
        {
            Id = id;
            OwnerUser = ownerUser;
        }

        [DataMember]
        public Guid Id { get; protected set; }

        [DataMember]
        public Guid OwnerUser { get; protected set; }
    }
}