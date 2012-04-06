using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using NUnit.Framework;

namespace VideoHelp.Infrastructure.Tests
{
    [TestFixture]
    public class MassTransitSample
    {
        [Test]
        public void FirstLook()
        {
          
            Bus.Initialize(sbc =>
                               {
                                   sbc.UseMsmq();
                                   sbc.VerifyMsmqConfiguration();
                                   sbc.UseMulticastSubscriptionClient();
                                   sbc.ReceiveFrom("msmq://localhost/test_queue");
                                   sbc.Subscribe(
                                       subs => subs.Handler<YourMessage>(msg =>
                                                                             {
                                                                                 Console.WriteLine("Message" + msg.Text);
                                                                                 Bus.Instance.MessageContext<YourMessage>().Respond(new CommandResult(){Text = "resp " + msg.Text});
  
                                                                             }
                                                   ));
                               });
            

            var t = new Task(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Bus.Instance.Publish(new YourMessage { Text = i.ToString(CultureInfo.InvariantCulture) });

                    Thread.Sleep(100);
                }
            });

            t.Start();

            Bus.Instance.Publish(new YourMessage {Text = "Test"});
            Bus.Instance.Publish(new YourMessage {Text = "Test3"});
            Bus.Instance.Publish(new YourMessage {Text = "Test4"});

            
            Bus.Instance.PublishRequest(new YourMessage { Text = "18" },
                                        x => x.Handle<CommandResult>(result => Console.WriteLine(result.Text)));
            Bus.Instance.PublishRequest(new YourMessage { Text = "SS" },
                                        x => x.Handle<CommandResult>(result => Console.WriteLine(result.Text)));


            t.Wait();
            //Thread.Sleep(300);
            Bus.Shutdown();
        }
    }


    public class CommandResult : CorrelatedBy<Guid>
    {
        public string Text;

        public Guid CorrelationId
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class YourMessage
    {
        public string Text;
    }
}