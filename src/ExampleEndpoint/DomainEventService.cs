using System.Collections.Generic;

namespace ExampleEndpoint
{
    class DomainEventService : IDomainEventService
    {
        public Queue<object> Events { get; } = new Queue<object>();
    }
}