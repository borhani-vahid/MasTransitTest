using ClassLibrary;
using MassTransit;

namespace BlazorApp.Data
{
    public class MassTestService
    {
        private readonly FennecDbContextTest dbContext;
        readonly IPublishEndpoint publishEndpoint;

        public MassTestService(FennecDbContextTest dbContext, IPublishEndpoint publishEndpoint)
        {
            this.dbContext = dbContext;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Create(string name)
        {
            var entity = new Entity
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await dbContext.Set<Entity>().AddAsync(entity);

            await publishEndpoint.Publish(new AddEntityEvent
            {
                Name = entity.Name
            });

            await dbContext.SaveChangesAsync();
        }
    }
}