using ClassLibrary;
using MassTransit;

namespace ApiApp.Consumers
{
    public class EntityConsumerOnApi : IConsumer<AddEntityEvent>
    {
        readonly ILogger<EntityConsumerOnApi> logger;

        public EntityConsumerOnApi(ILogger<EntityConsumerOnApi> logger)
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