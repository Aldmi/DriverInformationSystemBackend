using System.Linq.Expressions;
using Application.Domain;
using Application.Domain.TrainAgregat;
using Application.Interfaces;
using MongoDB.Driver;

namespace Application.Infrastructure.Persistence.MongoDb.Repositories;

public class MongoTrainRepository : MongoAbstractRepository<Train>, ITrainRepository
{
    public const string CollectionName = "Trains";

    public MongoTrainRepository(IMongoCollection<Train> collections, ConnectionThrottlingPipeline connectionThrottlingPipeline) : base(collections, connectionThrottlingPipeline)
    {
    }

    public async Task<bool> CheckUniqCarrigeNumberListAsync(IEnumerable<string> uniqCarrigeNumberList)
    {
    
        // var builder = Builders<Train>.Filter;
        // var filter = builder.All("Languages", new string[] { "english", "spanish" });
        //
        // var users = await collection.Find(filter).ToListAsync();
        //
        //
        // await Collections.CountDocumentsAsync(filter);
        //


        Func<Train, bool> pre = train => uniqCarrigeNumberList.Contains(train.LocomotiveOne.CarrigeNumber.UniqNumber);
        //var locomotiveOneCarrigeNumberIsExist= await _trainRepository.IsExistAsync(input => pre(input));
        
        
        var count= Collections.Find(train => pre(train)); 
        
        return true;
    }
}