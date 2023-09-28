using System.Reflection;
using Application.Domain;
using Application.Infrastructure.Persistence.MongoDb;
using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.Interfaces;
using CSharpFunctionalExtensions;
using FluentValidation;
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
        
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        BsonMapperConfigurate();
        
        var maxConnectionPoolSize = 800;
        var connectionString = configuration.GetConnectionString("Mongodb");//debug
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
        services.AddSingleton<IMongoCollection<MetroTrain>>(provider => {
            var db= provider.GetRequiredService<IMongoDatabase>();
            return db.GetCollection<MetroTrain>(MongoMetroTrainRepository.CollectionName);
        });
        services.AddSingleton<IMetroTrainRepository, MongoMetroTrainRepository>();
        
    
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
        BsonClassMap.RegisterClassMap<Entity<string>>(cm => {
            cm.AutoMap();
            cm.MapIdMember(i => i.Id);
            cm.SetIsRootClass(true);
        });
        // BsonClassMap.RegisterClassMap<Entity<string>>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
        // BsonClassMap.RegisterClassMap<Entity<string>>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
        // BsonClassMap.RegisterClassMap<Entity<string>>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
        // BsonClassMap.RegisterClassMap<Entity<string>>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
        // BsonClassMap.RegisterClassMap<StringInsertModelExt>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        //     cm.MapCreator(p => new StringInsertModelExt(
        //         p.Key,
        //         p.Format,
        //         p.BorderSubString,
        //         p.StringHandlerMiddleWareOption,
        //         p.DateTimeHandlerMiddleWareOption,
        //         p.MathematicFormula));
        // });
        // BsonClassMap.RegisterClassMap<TcpIpOption>(cm => {
        //     cm.AutoMap();
        //     cm.SetIgnoreExtraElements(true);
        // });
        
    }
}
