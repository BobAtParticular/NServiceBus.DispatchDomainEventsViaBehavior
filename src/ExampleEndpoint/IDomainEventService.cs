using System.Collections.Generic;

namespace ExampleEndpoint
{
    interface IDomainEventService
    {
        Queue<object> Events { get; }
    }
}