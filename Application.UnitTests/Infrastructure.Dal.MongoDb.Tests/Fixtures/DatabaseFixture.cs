using Application.Domain;
using Application.Infrastructure.Persistence.MongoDb;
using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;
using MongoDB.Driver;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.Fixtures;

public class DatabaseFixture
{
    const string ConnectionString = "mongodb://root:pass12345@localhost:27017";
    const string DbName = "DIS_Test";
    
    public readonly IMongoCollection<MetroTrain> MetroTrains;
    public readonly ConnectionThrottlingPipeline ConnectionThrottlingPipeline;

    public DatabaseFixture()
    {
        RegisterClassMap();
        var client= new MongoClient(ConnectionString);
        client.DropDatabase(DbName);
        var database = client.GetDatabase(DbName);
        
        MetroTrains = database.GetCollection<MetroTrain>(MongoMetroTrainRepository.CollectionName);
        
        ConnectionThrottlingPipeline = new ConnectionThrottlingPipeline(client.Settings.MaxConnectionPoolSize);
    }


    public void RegisterClassMap()
    {
        DependencyInjection.BsonMapperConfigurate();
    }
    
    
    public void Cleanup()
    {
        //Remove all datas
        MetroTrains.DeleteMany(_ => true);
        
        //Seed
        MetroTrains.InsertMany(MetroTrainsFactory.SeedList);
    }
}


[CollectionDefinition("SharedDbCollection")]
public class SharedDbCollection : ICollectionFixture<DatabaseFixture>
{
}