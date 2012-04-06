using System;
using MassTransit;

namespace VideoHelp.Infrastructure.Factories
{
    public class ServiceBusFactory
    {
        public IServiceBus Create(Uri endpoint)
        {
            return global::MassTransit.ServiceBusFactory.New(sbc =>
                {
                    sbc.UseMsmq();
                    sbc.VerifyMsmqConfiguration();
                    sbc.UseMulticastSubscriptionClient();
                    sbc.ReceiveFrom(endpoint);
                });
        }
    }
}