using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain.TrainAgregat;

/// <summary>
/// Локомотив
/// </summary>
public class Locomotive : Entity<Guid>
{
    public const int CountIpCamera = 4;
    
    
    /// <summary>
    /// Уникальный номер вагона
    /// </summary>
    public CarrigeNumber CarrigeNumber { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public IpCamera[] IpCameraArray { get; private set;}
    
    
    private Locomotive(
        CarrigeNumber carrigeNumber,
        IpCamera[] ipCameraArray
        )
    {
        CarrigeNumber = carrigeNumber;
        IpCameraArray = ipCameraArray;
    }


    public static Result<Locomotive> Create(CarrigeNumber carrigeNumber, IpCamera[] ipCameraArray)
    {
        if (ipCameraArray.Length != 4)
        {
            return Result.Failure<Locomotive>($"Ip камер в локомотиве должно быть {CountIpCamera}, а переданно {ipCameraArray.Length}");
        }
        var obj = new Locomotive(carrigeNumber, ipCameraArray);
        return obj;
    }


    /// <summary>
    /// Создать локомотив для поезда на базе уже добавленного локомотива в справочник.
    /// Указать Id этого локомотива.
    /// </summary>
    /// <param name="existingId"></param>
    /// <param name="carrigeNumber"></param>
    /// <param name="ipCameraArray"></param>
    /// <returns></returns>
    public static Result<Locomotive> CreateWithExistingGuid(Guid existingId, CarrigeNumber carrigeNumber, IpCamera[] ipCameraArray)
    {
        var obj = new Locomotive(carrigeNumber, ipCameraArray)
        {
            Id = existingId
        };
        return obj;
    }
}