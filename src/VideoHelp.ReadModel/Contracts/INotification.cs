using System;

namespace VideoHelp.ReadModel.Contracts
{
    public interface INotification<T> where T : IView
    {
        Guid ViewId { get; }
    }
}