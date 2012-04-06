using System;

namespace VideoHelp.ReadModel.Contracts
{
    public interface INotificationBus
    {
        T WaitNotification<T>(Guid viewId, int timeoutInSec = 30) where T : Notification;
        void PublishNotification<T>(T notification) where T : Notification;
    }
}