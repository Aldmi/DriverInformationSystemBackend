using System.Reflection;
using Application.Common.Behaviours;
using Application.Domain;
using Application.Domain.RouteMetroAgregat;
using Application.Domain.TrainAgregat;
using Application.Infrastructure.Persistence.MongoDb;
using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
         services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        BsonMapperConfigurate();
        
        var maxConnectionPoolSize = 800;
        services.AddSingleton<IMongoDatabase>(_ =>
        {
            var connectionString = configuration.GetConnectionString("Mongodb");
            var url = MongoUrl.Create(connectionString);
            var client = new MongoClient(new MongoClientSettings()
            {
                Server = url.Server,
                Credential = url.GetCredential(), //user, password
                MinConnectionPoolSize = 100,
                MaxConnectionPoolSize = maxConnectionPoolSize,
                // WaitQueueTimeout = TimeSpan.FromSeconds(100),
                ConnectTimeout = url.ConnectTimeout
            });
            var dbName = url.DatabaseName;
            return client.GetDatabase(dbName);
        });
        
        //SERVICES------------------------------------------------------------------------
        services.AddSingleton(_ => new ConnectionThrottlingPipeline(maxConnectionPoolSize));// Общий экземпляр для нескольких репозиториев (кому он нужен.)
        
        //REPOSITORIES-------------------------------------------------------------------
        //реализация интерфейса IMongoCollection по умолчанию потокобезопасна
        services.AddSingleton<IMongoCollection<Train>>(provider => {
            var db= provider.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<Train>(MongoTrainRepository.CollectionName);
        });
        services.AddSingleton<ITrainRepository, MongoTrainRepository>();
        
        services.AddSingleton<IMongoCollection<RouteMetro>>(provider => {
            var db= provider.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<RouteMetro>(MongoTrainRepository.CollectionName);
        });
        services.AddSingleton<IRouteMetroRepository, MongoRouteMetroRepository>();
        
        
        // services.AddScoped<IDomainEventService, DomainEventService>();
        //
        // services.AddTransient<IDateTime, DateTimeService>();
        // services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
        //
        // services.AddSingleton<ICurrentUserService, CurrentUserService>();
    
        return services;
    }


    public static void BsonMapperConfigurate()
    {
        BsonClassMap.RegisterClassMap<Entity<Guid>>(cm => {
            cm.AutoMap();
            cm.MapIdMember(i => i.Id);
            cm.SetIsRootClass(true);
        });
    }
}
