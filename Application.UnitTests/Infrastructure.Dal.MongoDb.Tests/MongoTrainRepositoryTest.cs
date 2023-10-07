using Application.Domain;
using Application.Domain.TrainAgregat;
using Application.Infrastructure.Persistence.MongoDb.Repositories;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.Fixtures;
using Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;
using Application.ValueObjects;
using FluentAssertions;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests;

[Collection("SharedDbCollection")]
public class MongoTrainRepositoryTest
{
    public MongoTrainRepositoryTest(DatabaseFixture fixture)
    {
        Fixture = fixture;
        Repository = new MongoTrainRepository(Fixture.Trains, Fixture.ConnectionThrottlingPipeline);
        Fixture.Cleanup();
    }
    
    private DatabaseFixture Fixture { get; }
    private MongoTrainRepository Repository { get; }
    
    
    [Fact]
    public async Task ListAsync_Ok()
    {
        var all = await Repository.ListAsync();
        
         all.Should().SatisfyRespectively(
             one =>
             {
                 one.Should().BeEquivalentTo(TrainsFactory.SeedList[0],options =>
                 {
                     options.ComparingByMembers<Train>();
                     return options;
                 });
             },
             two =>
             {
                 two.Should().BeEquivalentTo(TrainsFactory.SeedList[1],options =>
                 {
                     options.ComparingByMembers<Train>();
                     return options;
                 });
             },
             three =>
             {
                 three.Should().BeEquivalentTo(TrainsFactory.SeedList[2],options =>
                 {
                     options.ComparingByMembers<Train>();
                     return options;
                 });
             }
        );
    }
    

    [Fact]
    public async Task GetSingleAsync_Ok()
    {
        var expectedId = TrainsFactory.SeedList[2].Id;
        var single = await Repository.GetSingleAsync(e=> e.Id == expectedId);
        var expected = TrainsFactory.SeedList.First(o => o.Id == expectedId);
        
        single.Should().BeEquivalentTo(expected);
    }

    
    [Fact]
    public async Task GetById_NotFound_ReturnNull()
    {
        var single = await Repository.GetByIdAsync(Guid.NewGuid());
        
        single.Should().BeNull();
    }

    
 
    [Fact]
    public async Task AddOrReplace_Add_Ok()
    {
        //arrange
        var entity = Train.Create(
            "Train 10",
            Locomotive.Create(
                new CarrigeNumber("11111"),
                new IpCamera("192.168.1.1"),
                new IpCamera("192.168.1.2")).Value,
            Locomotive.Create(
                new CarrigeNumber("22222"),
                new IpCamera("192.168.1.10"),
                new IpCamera("192.168.1.20")).Value,
            new[]
            {
                Carrige.Create(
                    new CarrigeNumber("33333"),
                    1,
                    new IpCamera("192.168.1.1"),
                    new IpCamera("192.168.1.1")).Value,
                Carrige.Create(
                    new CarrigeNumber("44444"),
                    2,
                    new IpCamera("192.168.1.100"),
                    new IpCamera("192.168.1.121")).Value,
                Carrige.Create(
                    new CarrigeNumber("58612"),
                    2,
                    new IpCamera("192.168.1.100"),
                    new IpCamera("192.168.1.121")).Value,
                Carrige.Create(
                    new CarrigeNumber("98563"),
                    2,
                    new IpCamera("192.168.1.100"),
                    new IpCamera("192.168.1.121")).Value
            }
        ).Value;
        
        //act
        var id = await Repository.AddOrReplace(entity);
        
        //assert
        var allExpected = await Repository.ListAsync();
        var expected= allExpected.First(c => c.Id == entity.Id);
        
        id.Should().NotBeNull();
        allExpected.Count.Should().Be(TrainsFactory.SeedList.Count + 1);
        expected.Should().BeEquivalentTo(entity);
    }
    
    

    [Fact]
    public async Task AddOrReplace_Replace_Ok()
    {
        //Arrange
        var first= (await Repository.ListAsync())[0];
        first.Carriges[0].SetNewSerialCarrigeNumber(4);
        first.Carriges[1].SetNewSerialCarrigeNumber(2);
        
        //Act
        var updatedId= await Repository.AddOrReplace(first);
        
        //Assert
        var allExpected= await Repository.ListAsync();
        var expectedFirst= allExpected.First(c => c.Id == first.Id);
    
        updatedId.Should().Be(first.Id);
        allExpected.Should().HaveCount(3);
        expectedFirst.Should().BeEquivalentTo(first);
    }

    
    [Fact]
    public async Task Delete_By_Id()
    {
        var first= TrainsFactory.SeedList[0];
    
        var res=await Repository.DeleteAsync(first.Id);
        var countAfterDelete = (await Repository.ListAsync()).Count;
            
        res.Should().BeTrue();
        countAfterDelete.Should().Be(TrainsFactory.SeedList.Count - 1);
    }
    
    
    [Fact]
    public async Task Delete_By_Predicate()
    {
        var delete1= TrainsFactory.SeedList[1];
        var delete2= TrainsFactory.SeedList[2];
        
        var deletedCount= await Repository.DeleteAsync(e =>
            e.Id == delete1.Id ||
            e.Id == delete2.Id);
        var countAfterDelete = (await Repository.ListAsync()).Count;
        
        deletedCount.Should().Be(2);
        countAfterDelete.Should().Be(1);
    }
}