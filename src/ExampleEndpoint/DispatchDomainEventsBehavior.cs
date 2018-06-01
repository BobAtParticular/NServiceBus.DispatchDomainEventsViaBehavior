namespace ExampleEndpoint
{
    using System;
    using System.Threading.Tasks;
    using NServiceBus.Pipeline;
    using NServiceBus.UniformSession;

    class DispatchDomainEventsBehavior : Behavior<IIncomingLogicalMessageContext>
    {
        public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
        {
            await next().ConfigureAwait(false);

            var commandService = context.Builder.Build(typeof(IDomainCommandService)) as IDomainCommandService;
            var eventService = context.Builder.Build(typeof(IDomainEventService)) as IDomainEventService;

            var uniformSession = context.Builder.Build(typeof(IUniformSession)) as IUniformSession;

            while (commandService.Commands.Count > 0)
            {
                var command = commandService.Commands.Dequeue();
                await uniformSession.Send(command).ConfigureAwait(false);
            }

            while (eventService.Events.Count > 0)
            {
                var @event = eventService.Events.Dequeue();
                await uniformSession.Publish(@event).ConfigureAwait(false);
            }
        }
    }
}
