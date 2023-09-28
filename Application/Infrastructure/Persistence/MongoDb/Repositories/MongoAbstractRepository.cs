using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoAbstractRepository<T> where T : Entity<Guid>
{
    protected readonly IMongoCollection<T> Collections;
    protected readonly ConnectionThrottlingPipeline ConnectionThrottlingPipeline;
    
    public MongoAbstractRepository(IMongoCollection<T> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline)
    {
        Collections = collections;
        ConnectionThrottlingPipeline = connectionThrottlingPipeline;
    }
    
    
    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
    {
        var cursor = await Collections.FindAsync(predicate);
        return await cursor.SingleAsync();
    }
    
    public async Task<T> GetByIdAsync(Guid key)
    {
        var cursor = await Collections.FindAsync(c => c.Id == key);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync()
    {
        return await Collections.Find(_=>true).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate)
    {
        return await Collections.Find(predicate).ToListAsync();
    }

    public async Task<Guid?> AddOrReplace(T entity)
    {
        // if (string.IsNullOrEmpty(entity.Id))  {
        //     throw new ArgumentException("entity.Key cant be null or empty");
        // }
        var res= await ConnectionThrottlingPipeline.AddRequest(
            Collections.ReplaceOneAsync(c =>c.Id == entity.Id, entity, new ReplaceOptions{IsUpsert = true}));
        return res.ModifiedCount == 0 ? null : entity.Id;
    }

    public async Task AddRangeAsync(IReadOnlyList<T> entitys)
    {
        await Collections.InsertManyAsync(entitys);
    }

    public async Task<bool> DeleteAsync(Guid key)
    {
        var res = await Collections.DeleteOneAsync(e=>e.Id == key);
        return res.IsAcknowledged && res.DeletedCount == 1;
    }

    public async Task<long> DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var res = await Collections.DeleteManyAsync(predicate);
        return res.DeletedCount;
    }

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
    { 
        return await Collections.CountDocumentsAsync(predicate) > 0;
    }
}