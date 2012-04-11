using System;

namespace VideoHelp.Domain
{
    public interface IMediaContent
    {
        Guid Id { get; }
        Guid OwnerUser { get; }
    }
}