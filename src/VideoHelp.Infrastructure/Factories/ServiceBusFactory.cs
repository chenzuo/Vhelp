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
                    sbc.UseRabbitMq();
                    sbc.UseRabbitMqRouting();
                    sbc.UseNLog();
                    sbc.ReceiveFrom(endpoint);
                });
        }
    }
}