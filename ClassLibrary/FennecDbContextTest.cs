using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClassLibrary
{
    public class FennecDbContextTest : DbContext
    {
        private readonly IConfiguration config;

        public FennecDbContextTest(DbContextOptions<FennecDbContextTest> options, IConfiguration config) : base(options)
        {
            this.config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Entity>()
                .ToTable("Entities")
                .HasKey(e => e.Id);

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured && config != null)
            {
                optionsBuilder.UseSqlServer(config["ConnectionString"]);
            }
        }
    }
}