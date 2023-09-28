using Application.Domain;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoMetroTrainRepository : MongoAbstractRepository<MetroTrain>, IMetroTrainRepository
{
    public const string CollectionName = "MetroTrains";

    public MongoMetroTrainRepository(IMongoCollection<MetroTrain> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }
}