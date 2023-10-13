using Application.Domain.TrainAgregat;
using Application.ValueObjects;

namespace Application.UnitTests.Infrastructure.Dal.MongoDb.Tests.SeedDatas;

public static class TrainsFactory
{
    public static List<Train> SeedList { get; } = new()
    {
        Train.Create(
            "Train 1",
             Locomotive.Create(
                new CarrigeNumber("11111"),
                new []
                {
                    new IpCamera("192.168.1.1"),
                    new IpCamera("192.168.1.2"),
                    new IpCamera("192.168.1.3"),
                    new IpCamera("192.168.1.4")
                }).Value,
             Locomotive.Create(
                new CarrigeNumber("22222"),
                new []
                {
                    new IpCamera("192.168.1.10"),
                    new IpCamera("192.168.1.20"),
                    new IpCamera("192.168.1.30"),
                    new IpCamera("192.168.1.40")
                }).Value,
             new []
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
            ).Value,
        
        Train.Create(
            "Train 2",
            Locomotive.Create(
                new CarrigeNumber("333"),
                new []
                {
                    new IpCamera("192.168.1.11"),
                    new IpCamera("192.168.1.21"),
                    new IpCamera("192.168.1.31"),
                    new IpCamera("192.168.1.41")
                }).Value,
            Locomotive.Create(
                new CarrigeNumber("444"),
                new []
                {
                    new IpCamera("192.168.1.101"),
                    new IpCamera("192.168.1.201"),
                    new IpCamera("192.168.1.310"),
                    new IpCamera("192.168.1.401")
                }).Value,
             
            new []
            {
                Carrige.Create(
                    new CarrigeNumber("77777"),
                    1,
                    new IpCamera("192.168.1.1"),
                    new IpCamera("192.168.1.1")).Value,
                 
                Carrige.Create(
                    new CarrigeNumber("88888"),
                    2,
                    new IpCamera("192.168.1.100"),
                    new IpCamera("192.168.1.121")).Value
            }
        ).Value,
        
        Train.Create(
            "Train 3",
            Locomotive.Create(
                new CarrigeNumber("5698"),
                new []
                {
                    new IpCamera("192.168.1.11"),
                    new IpCamera("192.168.1.21"),
                    new IpCamera("192.168.1.31"),
                    new IpCamera("192.168.1.41")
                }).Value,
            Locomotive.Create(
                new CarrigeNumber("8562"),
                new []
                {
                    new IpCamera("192.168.1.101"),
                    new IpCamera("192.168.1.201"),
                    new IpCamera("192.168.1.310"),
                    new IpCamera("192.168.1.401")
                }).Value,
             
            new []
            {
                Carrige.Create(
                    new CarrigeNumber("985641"),
                    1,
                    new IpCamera("192.168.1.1"),
                    new IpCamera("192.168.1.1")).Value,
                 
                Carrige.Create(
                    new CarrigeNumber("965842"),
                    2,
                    new IpCamera("192.168.1.100"),
                    new IpCamera("192.168.1.121")).Value
            }
        ).Value
    };
}