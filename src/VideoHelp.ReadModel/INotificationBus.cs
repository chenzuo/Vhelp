using System;
using VideoHelp.ReadModel.Documents;

namespace VideoHelp.ReadModel
{
    public interface INotificationBus
    {
        bool WaitNotification<TDoc>(Guid documentId, int timeoutInSec = 30) where TDoc : IDocument;

        Action SubscribeNotification<TDoc>(Action<Guid> updateAction) where TDoc : IDocument;

        void PublishNotification<TDoc>(TDoc document) where  TDoc : IDocument;
    }
}