using ClassLibrary;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Consumers
{
    public class EntityConsumer : IConsumer<AddEntityEvent>
    {
        readonly ILogger<EntityConsumer> logger;

        public EntityConsumer(ILogger<EntityConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<AddEntityEvent> context)
        {
            logger.LogWarning("Wooow! A message received => {Text}.", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}