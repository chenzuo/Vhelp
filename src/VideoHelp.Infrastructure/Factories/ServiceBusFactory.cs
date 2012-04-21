using System;
using Magnum.Extensions;
using MassTransit;
using MassTransit.NLogIntegration;

namespace VideoHelp.Infrastructure.Factories
{
    public class ServiceBusFactory
    {
        public IServiceBus Create(Uri endpoint)
        {
            return global::MassTransit.ServiceBusFactory.New(sbc =>
                {
                    sbc.UseMsmq();
                    sbc.UseNLog();
                    sbc.VerifyMsmqConfiguration();
                    sbc.UseMulticastSubscriptionClient();
                    sbc.ReceiveFrom(endpoint);
                    sbc.UseControlBus();
                    sbc.UseJsonSerializer();

                    sbc.SetConcurrentConsumerLimit(5);
                    sbc.SetDefaultTransactionTimeout(1.Minutes());

                });
        }
    }
}