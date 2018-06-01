using System;
using System.Threading.Tasks;
using NServiceBus;

namespace ExampleEndpoint
{
    class MyCommandHandler : IHandleMessages<MyCommand>
    {
        public MyCommandHandler(SomeBusinessService service)
        {
            this.service = service;
        }

        public async Task Handle(MyCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Handling MyCommand {message.Id}, Sent via User Action: {message.SentFromUserAction}, Sent via Context: {message.SentFromContext}, Sent via Behavior: {message.SentFromBehavior}");

            if (message.SentFromUserAction)
            {
                //This simulates a business service being called that will eventually raise domain messages to be placed on the bus
                service.DoSomething();

                //Represents sending using the context
                await context.Send(new MyCommand {SentFromContext = true}).ConfigureAwait(false);
            }
        }

        SomeBusinessService service;
    }
}
