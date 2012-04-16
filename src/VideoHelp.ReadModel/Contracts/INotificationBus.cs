using System;
using VideoHelp.ReadModel.Notification;

namespace VideoHelp.ReadModel.Contracts
{
    public interface INotificationBus
    {
        bool WaitNotification<TView>(Guid viewId, int timeoutInSec = 30) where TView : IView;

        Action SubscribeNotification<TView>(Action<Guid> updateAction) where TView : IView;

        void PublishNotification<TView>(ViewUpdated<TView> notification) where TView : IView;
    }
}