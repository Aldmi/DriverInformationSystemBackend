using Application.Domain;
using Application.Domain.RouteMetroAgregat;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoRouteMetroRepository : MongoAbstractRepository<RouteMetro>, IRouteMetroRepository
{
    public const string CollectionName = "Routes";

    public MongoRouteMetroRepository(IMongoCollection<RouteMetro> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }
}