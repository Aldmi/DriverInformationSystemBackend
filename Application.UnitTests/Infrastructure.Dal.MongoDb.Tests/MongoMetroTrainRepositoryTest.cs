using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.Fixtures;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;
using FluentAssertions;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests;

[Collection("SharedDbCollection")]
public class MongoMetroTrainRepositoryTest
{
    public MongoMetroTrainRepositoryTest(DatabaseFixture fixture)
    {
        Fixture = fixture;
        Repository = new MongoMetroTrainRepository(Fixture.MetroTrains, Fixture.ConnectionThrottlingPipeline);
        Fixture.Cleanup();
    }
    
    private DatabaseFixture Fixture { get; }
    private MongoMetroTrainRepository Repository { get; }
    
    
    [Fact]
    public async Task ListAsync_Ok()
    {
        var all = await Repository.ListAsync();
        
         all.Should().SatisfyRespectively(
             one =>
             {
                 one.Should().BeEquivalentTo(MetroTrainsFactory.SeedList[0]);
             },
             two =>
             {
                 two.Should().BeEquivalentTo(MetroTrainsFactory.SeedList[1]);
             },
             three =>
             {
                 three.Should().BeEquivalentTo(MetroTrainsFactory.SeedList[2]);
             },
             four =>
             {
                 four.Should().BeEquivalentTo(MetroTrainsFactory.SeedList[3]);
             },
             five =>
             {
                 five.Should().BeEquivalentTo(MetroTrainsFactory.SeedList[4]);
             }
        );
    }
    
    
    // [Fact]
    // public async Task GetSingleAsync_Ok()
    // {
    //     var expectedKey = DeviceOptionsFactory.SeedList[3].Key;
    //     var single = await Repository.GetSingleAsync(e=> e.Key == expectedKey);
    //     var expected = DeviceOptionsFactory.SeedList.First(o => o.Key == expectedKey);
    //     
    //     single.Should().BeEquivalentTo(expected);
    // }
    //
    //
    // [Fact]
    // public async Task GetById_NotFound_ReturnNull()
    // {
    //     var single = await Repository.GetByIdAsync("Error_key");
    //     
    //     single.Should().BeNull();
    // }
    //
    //
    // [Fact]
    // public async Task GetSingleAsync_Exception_not_one_element()
    // {
    //     Func<Task<DeviceOption>> act = () => Repository.GetSingleAsync(e=>e.Key != "Device 5");
    //
    //     await act.Should()
    //         .ThrowAsync<InvalidOperationException>()
    //         .WithMessage("Sequence contains more than one element");;
    // }
    //
    //
    // [Fact]
    // public async Task AddOrReplace_Add_Ok()
    // {
    //     //arrange
    //     var entity = new DeviceOption
    //     {
    //         Key = "Device 100",
    //         Description = "Description 100",
    //         Paging = null,
    //         AutoBuild = true,
    //         ProduserUnionKey = null,
    //         ExchangeKeys = new List<string>() {"Exchange 1.100", "Exchange 1.110"},
    //         MiddleWareMediator = null,
    //     };
    //     
    //     //act
    //     var id = await Repository.AddOrReplace(entity);
    //     
    //     //assert
    //     var allExpected = await Repository.ListAsync();
    //     var expected= allExpected.First(c => c.Key == entity.Key);
    //     
    //     id.Should().BeNull();
    //     allExpected.Count.Should().Be(6);
    //     expected.Should().BeEquivalentTo(entity);
    // }
    //
    //
    // [Fact]
    // public async Task AddOrReplace_Replace_Ok()
    // {
    //     //Arrange
    //     var first= (await Repository.ListAsync())[0];
    //     first.Description = "New Description";
    //     first.ExchangeKeys = new List<string>() {"New key 999", "New key 666"};
    //     first.MiddleWareMediator = new MiddleWareMediatorOption
    //     {
    //         Description = "Преобразование Note",
    //         StringHandlers = new List<StringHandlerMiddleWare4InDataOption>()
    //         {
    //             new StringHandlerMiddleWare4InDataOption
    //             {
    //                 PropName = "Note.NameRu",
    //                 Converters = new List<UnitStringConverterOption>
    //                 {
    //                     new UnitStringConverterOption
    //                     {
    //                         LimitStringConverterOption = new LimitStringConverterOption()
    //                         {
    //                             Limit = 666
    //                         }
    //                     }
    //                 }
    //             }
    //         },
    //         InvokerOutput = new InvokerOutput
    //         {
    //             Mode = InvokerOutputMode.ByTimer
    //         }
    //     };
    //     
    //     //Act
    //     var updatedId= await Repository.AddOrReplace(first);
    //     
    //     //Assert
    //     var allExpected= await Repository.ListAsync();
    //     var expectedFirst= allExpected.First(c => c.Key == first.Key);
    //
    //     updatedId.Should().Be(first.Key);
    //     allExpected.Should().HaveCount(5);
    //     expectedFirst.Should().BeEquivalentTo(first);
    // }
    //
    //
    // [Fact]
    // public async Task Delete_By_Key()
    // {
    //     var first= DeviceOptionsFactory.SeedList[0];
    //
    //     var res=await Repository.DeleteAsync(first.Key);
    //     var countAfterDelete = (await Repository.ListAsync()).Count;
    //         
    //     res.Should().BeTrue();
    //     countAfterDelete.Should().Be(4);
    // }
    //
    //
    // [Fact]
    // public async Task Delete_By_Predicate()
    // {
    //     var delete1= DeviceOptionsFactory.SeedList[3];
    //     var delete2= DeviceOptionsFactory.SeedList[4];
    //     
    //     var deletedCount= await Repository.DeleteAsync(e =>
    //         e.Key == delete1.Key ||
    //         e.Key == delete2.Key);
    //     var countAfterDelete = (await Repository.ListAsync()).Count;
    //     
    //     deletedCount.Should().Be(2);
    //     countAfterDelete.Should().Be(3);
    // }
}