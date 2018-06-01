using System;
using NServiceBus;

namespace ExampleEndpoint
{
    class MyCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool SentFromUserAction { get; set; }
        public bool SentFromContext { get; set; }
        public bool SentFromBehavior { get; set; }
    }
}
