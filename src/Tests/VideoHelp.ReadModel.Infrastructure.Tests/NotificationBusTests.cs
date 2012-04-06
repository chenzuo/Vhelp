using System;
using MassTransit;
using Moq;
using NUnit.Framework;
using VideoHelp.ReadModel.Contracts;

namespace VideoHelp.ReadModel.Infrastructure.Tests
{
    [TestFixture]
    public class NotificationBusTests
    {
         [Test]
         public void StopWaitNotificationWhenNotoficationSend()
         {
             var unsubscrideCount = 0;
             var serviceBus = new Mock<IServiceBus>();
             
             var guid = Guid.NewGuid();
             var notification = new TestNotification(guid);

             var notifyBus = new NotificationBus(serviceBus.Object);

             
             notifyBus.WaitNotification<TestNotification>(guid);
         }
    }

    public class TestNotification : Contracts.Notification
    {
        public TestNotification(Guid id) : base(id)
        {
        }
    }
}