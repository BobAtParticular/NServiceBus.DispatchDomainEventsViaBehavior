using System;
using System.Threading.Tasks;
using ExampleEndpoint;
using NServiceBus;

static class Program
{
    static async Task Main()
    {
        Console.Title = "ExampleEndpoint";
        var endpointConfiguration = new EndpointConfiguration("ExampleEndpoint");
        endpointConfiguration.UsePersistence<LearningPersistence>();
        var transport = endpointConfiguration.UseTransport<LearningTransport>();

        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();

        transport.Routing().RouteToEndpoint(typeof(MyCommand), "ExampleEndpoint");
        endpointConfiguration.PurgeOnStartup(true);

        //IOC
        endpointConfiguration.RegisterComponents(c =>
            c.ConfigureComponent<IDomainCommandService>(b => new DomainCommandService(),
                DependencyLifecycle.InstancePerUnitOfWork));
        endpointConfiguration.RegisterComponents(c =>
            c.ConfigureComponent<IDomainEventService>(b => new DomainEventService(),
                DependencyLifecycle.InstancePerUnitOfWork));
        endpointConfiguration.RegisterComponents(c =>
            c.ConfigureComponent<SomeBusinessService>(DependencyLifecycle.InstancePerUnitOfWork));

        //Register the behavior
        var pipeline = endpointConfiguration.Pipeline;
        pipeline.Register(
            behavior: new DispatchDomainEventsBehavior(), 
            description: "Dispatches messages");

        //Enable uniform session
        endpointConfiguration.EnableUniformSession();

        //start the endpoint
        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        await Start(endpointInstance)
            .ConfigureAwait(false);
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }

    static async Task Start(IEndpointInstance endpointInstance)
    {
        Console.WriteLine("Press '1' to send MyCommand");
        Console.WriteLine("Press any other key to exit");

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.D1)
            {
                var command = new MyCommand {SentFromUserAction = true};

                await endpointInstance.Send(command)
                    .ConfigureAwait(false);
                Console.WriteLine($"Sent MyCommand {command.Id}.");
            }
            else
            {
                return;
            }
        }
    }
}