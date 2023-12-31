﻿using Application.Domain;
using Application.Domain.RouteMetroAgregat;
using Application.Domain.TrainAgregat;
using Application.Infrastructure.Persistence.MongoDb;
using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;
using MongoDB.Driver;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.Fixtures;

public class DatabaseFixture
{
    const string ConnectionString = "mongodb://root:pass12345@localhost:27017";
    const string DbName = "DIS_Test";
    
    public readonly IMongoCollection<Train> Trains;
    public readonly IMongoCollection<RouteMetro> Routes;
    public readonly ConnectionThrottlingPipeline ConnectionThrottlingPipeline;

    public DatabaseFixture()
    {
        RegisterClassMap();
        var client= new MongoClient(ConnectionString);
        client.DropDatabase(DbName);
        var database = client.GetDatabase(DbName);
        
        Trains = database.GetCollection<Train>(MongoTrainRepository.CollectionName);
        Routes = database.GetCollection<RouteMetro>(MongoRouteMetroRepository.CollectionName);
        
        ConnectionThrottlingPipeline = new ConnectionThrottlingPipeline(client.Settings.MaxConnectionPoolSize);
    }


    public void RegisterClassMap()
    {
        DependencyInjection.BsonMapperConfigurate();
    }
    
    
    public void Cleanup()
    {
        //Remove all datas
        Trains.DeleteMany(_ => true);
        Routes.DeleteMany(_ => true);
        
        //Seed
        Trains.InsertMany(TrainsFactory.SeedList);
        Routes.InsertMany(RoutesMetroFactory.SeedList);
    }
}


[CollectionDefinition("SharedDbCollection")]
public class SharedDbCollection : ICollectionFixture<DatabaseFixture>
{
}