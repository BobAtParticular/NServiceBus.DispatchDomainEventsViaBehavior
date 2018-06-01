using System.Collections.Generic;

namespace ExampleEndpoint
{
    class DomainCommandService : IDomainCommandService
    {
        public Queue<object> Commands { get; } = new Queue<object>();
    }
}
