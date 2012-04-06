using System;
using System.Threading;
using MassTransit;
using VideoHelp.ReadModel.Contracts;

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

        public T WaitNotification<T>(Guid viewId, int timeoutInSec = 30) where T : Contracts.Notification
        {
            T result = null;
            var cancelSubscription = _serviceBus.SubscribeHandler<T>(notification =>
                                                                         {
                                                                             result = notification;
                                                                             _resetEvent.Set();
                                                                         }, obj => obj.NotificationId == viewId);
    
            _resetEvent.Wait(timeoutInSec*1000);
            cancelSubscription();
            _resetEvent.Reset();
            return result;

        }

        public void PublishNotification<T>(T notification) where T : Contracts.Notification
        {
            _serviceBus.Publish(notification);
        }
    }
}