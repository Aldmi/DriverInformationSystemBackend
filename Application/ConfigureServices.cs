using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }

    // public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    // {
    //     if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    //     {
    //         services.AddDbContext<ApplicationDbContext>(options =>
    //             options.UseInMemoryDatabase("VerticalSliceDb"));
    //     }
    //     else
    //     {
    //         services.AddDbContext<ApplicationDbContext>(options =>
    //             options.UseSqlServer(
    //                 configuration.GetConnectionString("DefaultConnection"),
    //                 b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
    //     }
    //
    //     services.AddScoped<IDomainEventService, DomainEventService>();
    //
    //     services.AddTransient<IDateTime, DateTimeService>();
    //     services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
    //
    //     services.AddSingleton<ICurrentUserService, CurrentUserService>();
    //
    //     return services;
    // }


    // public static IServiceCollection AddRabitMqConsumers(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.Configure<RabbitMqConfiguration>(configuration);
    //     services.AddSingleton<IRabbitMqService, RabbitMqService>();
    //     services.AddSingleton<IConsumerService, CreateSoundFileConsumerService>();
    //     services.AddHostedService<ConsumerHostedService>();
    //     
    //     return services;
    // }
}
