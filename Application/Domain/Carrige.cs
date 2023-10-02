using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain;

/// <summary>
/// Вагон пассажирский
/// </summary>
public class Carrige : Entity<Guid>
{
    /// <summary>
    /// Уникальный номер вагона
    /// </summary>
    public CarrigeNumber CarrigeNumber { get; private set;}
    
    /// <summary>
    /// Порядковый номер вагона
    /// </summary>
    public int SerialCarrigeNumber { get; private set;}

    /// <summary>
    /// Камера в начале вагона
    /// </summary>
    public IpCamera IpCameraFirst { get; private set;}
    
    /// <summary>
    /// Камера в конце вагона
    /// </summary>
    public IpCamera IpCameraSecond { get; private set;}
    
    
    private Carrige(
        CarrigeNumber carrigeNumber,
        int serialCarrigeNumber,
        IpCamera ipCameraFirst,
        IpCamera ipCameraSecond)
    {
        CarrigeNumber = carrigeNumber;
        SerialCarrigeNumber = serialCarrigeNumber;
        IpCameraFirst = ipCameraFirst;
        IpCameraSecond = ipCameraSecond;
    }

    
    public static Result<Carrige> Create(CarrigeNumber carrigeNumber, int seriaCarrigeNumber, IpCamera ipCameraFirst, IpCamera ipCameraSecond)
    {
        var obj = new Carrige(carrigeNumber, seriaCarrigeNumber, ipCameraFirst, ipCameraSecond);
        return obj;
    }
    
    /// <summary>
    /// Создать вагон для поезда на базе уже добавленного вагона в справочник.
    /// Указать Id этого вагона.
    /// </summary>
    /// <param name="existingId"></param>
    /// <param name="carrigeNumber"></param>
    /// <param name="seriaCarrigeNumber"></param>
    /// <param name="ipCameraFirst"></param>
    /// <param name="ipCameraSecond"></param>
    /// <returns></returns>
    public static Result<Carrige> CreateWithexistingGuid(Guid existingId, CarrigeNumber carrigeNumber, int seriaCarrigeNumber, IpCamera ipCameraFirst, IpCamera ipCameraSecond)
    {
        var obj = new Carrige(carrigeNumber, seriaCarrigeNumber, ipCameraFirst, ipCameraSecond)
        {
            Id = existingId
        };
        return obj;
    }


    public Result SetNewSerialCarrigeNumber(int serialCarrigeNumber)
    {
        if (serialCarrigeNumber > 5)
        {
            return Result.Failure("Порядковый номер не может быть больше кол-ва вагонов");
        }
            
        SerialCarrigeNumber = serialCarrigeNumber;
        return Result.Success();
    }
}
