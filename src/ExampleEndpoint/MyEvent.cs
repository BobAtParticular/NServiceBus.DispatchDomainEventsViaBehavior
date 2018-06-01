using System;
using NServiceBus;

namespace ExampleEndpoint
{
    class MyEvent : IEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}