using Application.Domain;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoTrainRepository : MongoAbstractRepository<Train>, ITrainRepository
{
    public const string CollectionName = "Trains";

    public MongoTrainRepository(IMongoCollection<Train> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }
}