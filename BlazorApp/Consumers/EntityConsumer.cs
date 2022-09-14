using ClassLibrary;
using MassTransit;

namespace BlazorApp.Consumers
{
    public class EntityConsumerOnBlazor : IConsumer<AddEntityEvent>
    {
        readonly ILogger<EntityConsumerOnBlazor> logger;

        public EntityConsumerOnBlazor(ILogger<EntityConsumerOnBlazor> logger)
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