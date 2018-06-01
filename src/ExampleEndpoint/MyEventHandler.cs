using System;
using System.Threading.Tasks;
using NServiceBus;

namespace ExampleEndpoint
{
    class MyEventHandler : IHandleMessages<MyEvent>
    {
        public Task Handle(MyEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Handling MyEvent {message.Id}");
            return Task.CompletedTask;
        }
    }
}