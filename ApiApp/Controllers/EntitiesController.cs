using ClassLibrary;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntitiesController : ControllerBase
    {
        readonly IPublishEndpoint publishEndpoint;
        readonly FennecDbContextTest dbContextt;

        private readonly ILogger<EntitiesController> logger;

        public EntitiesController(ILogger<EntitiesController> logger, IPublishEndpoint publishEndpoint, FennecDbContextTest dbContextt)
        {
            this.logger = logger;
            this.publishEndpoint = publishEndpoint;
            this.dbContextt = dbContextt;
        }

        [HttpGet(Name = "GetEntities")]
        public async Task<IEnumerable<Entity>> Get()
        {
            return await dbContextt.Set<Entity>().ToArrayAsync();
        }

        [HttpPost(Name = "AddEntity")]
        public async Task<Entity> Post(string name)
        {
            var entity = new Entity
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await dbContextt.Set<Entity>().AddAsync(entity);

            await publishEndpoint.Publish(new AddEntityEvent
            {
                Name = entity.Name
            });

            await dbContextt.SaveChangesAsync();

            return entity;
        }
    }
}