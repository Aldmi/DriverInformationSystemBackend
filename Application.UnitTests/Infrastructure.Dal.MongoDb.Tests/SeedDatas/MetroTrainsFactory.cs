using Application.Domain;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;

public static class MetroTrainsFactory
{
    public static List<MetroTrain> SeedList { get; } = new()
    {
        new MetroTrain()
        {
            Name = "Name_11111"
        },
        // new MetroTrain()
        // {
        //     Name = "Name_222222"
        // }
    };

}