using System.Collections.Generic;

namespace ExampleEndpoint
{
    interface IDomainCommandService
    {
        Queue<object> Commands { get; }
    }
}