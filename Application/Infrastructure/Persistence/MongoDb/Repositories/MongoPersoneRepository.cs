using System.Linq.Expressions;
using Application.Domain.PersonAgregat;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoPersoneRepository : MongoAbstractRepository<Person>, IPersoneRepository
{
    public const string CollectionName = "Persones";
    
    public MongoPersoneRepository(IMongoCollection<Person> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }
    
    
    public async Task<Person?> GetOrDefaultAsync(Expression<Func<Person, bool>> predicate)
    {
        var cursor = await Collections.FindAsync(predicate);
        return await cursor.FirstOrDefaultAsync();
    }
}