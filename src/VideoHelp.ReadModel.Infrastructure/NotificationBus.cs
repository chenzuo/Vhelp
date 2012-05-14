using System;
using System.Threading;
using MassTransit;
using VideoHelp.ReadModel.Documents;
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

        public bool WaitNotification<T>(Guid documentId, int timeoutInSec) where T : IDocument
        {
            bool result = false;
            var cancelSubscription = _serviceBus.SubscribeHandler<DocumentUpdated<T>>(notification => 
                                    { 
                                        result = true; 
                                       _resetEvent.Set();
                                    }, obj => obj.DocumentId == documentId);

            _resetEvent.Wait(timeoutInSec * 1000);
            if( !result)
            {
                cancelSubscription();
            }
            _resetEvent.Reset();
            return result;
        }

        public Action SubscribeNotification<TDoc>(Action<Guid> updateAction) where TDoc : IDocument
        {
            var unsubscriber =_serviceBus.SubscribeHandler<DocumentUpdated<TDoc>>(notification => updateAction(notification.DocumentId));

            return () => unsubscriber();
        }

        public void PublishNotification<TDoc>(TDoc document) where TDoc : IDocument
        {
            _serviceBus.Publish(new DocumentUpdated<TDoc>(document));
        }
    }
}