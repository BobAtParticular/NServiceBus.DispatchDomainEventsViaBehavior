namespace ExampleEndpoint
{
    class SomeBusinessService
    {
        public SomeBusinessService(IDomainCommandService commandService, IDomainEventService eventService)
        {
            this.commandService = commandService;
            this.eventService = eventService;
        }

        public void DoSomething()
        {
            commandService.Commands.Enqueue(new MyCommand {SentFromBehavior = true});
            eventService.Events.Enqueue(new MyEvent());
        }

        IDomainCommandService commandService;
        IDomainEventService eventService;
    }
}
