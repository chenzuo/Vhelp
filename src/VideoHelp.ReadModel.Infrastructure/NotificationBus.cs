using System;
using System.Threading;
using MassTransit;
using VideoHelp.ReadModel.Contracts;
using VideoHelp.ReadModel.Notification;

namespace VideoHelp.ReadModel.Infrastructure
{
    public class NotificationBus : INotificationBus 
    {
        private readonly IServiceBus _serviceBus;
        private readonly ManualResetEventSlim _resetEvent;

        public NotificationBus(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
            _resetEvent = new ManualResetEventSlim(false);
        }

        bool INotificationBus.WaitNotification<TView>(Guid guid, int timeoutInSec)
        {
            bool result = false;
            var cancelSubscription = _serviceBus.SubscribeHandler<ViewUpdated<TView>>(notification => 
                                    { 
                                        result = true; 
                                       _resetEvent.Set();
                                    }, obj => obj.ViewId == guid);

            _resetEvent.Wait(timeoutInSec * 1000);
            if( !result)
            {
                cancelSubscription();
            }
            _resetEvent.Reset();
            return result;
        }

        public void SubscribeNotification<TView>(Action<Guid> updateAction) where TView : IView
        {
            _serviceBus.SubscribeHandler<ViewUpdated<TView>>(notification => updateAction(notification.ViewId));
        }

        public void PublishNotification<TView>(ViewUpdated<TView> notification) where TView : IView
        {
            _serviceBus.Publish(notification);
        }
    }
}