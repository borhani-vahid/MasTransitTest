using ClassLibrary;
using ConsoleApp.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("MassTransit", LogEventLevel.Debug)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<FennecDbContextTest>(options =>
        {
            options.UseSqlServer(hostContext.Configuration["ConnectionString"]);
        });

        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<FennecDbContextTest>(o =>
            {
                o.UseSqlServer();
            });

            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumer<EntityConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
