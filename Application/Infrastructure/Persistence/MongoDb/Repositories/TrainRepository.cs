using Application.Domain;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class TrainRepository : MongoAbstractRepository<Train>, ITrainRepository
{
    public const string CollectionName = "Trains";

    public TrainRepository(IMongoCollection<Train> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }
}