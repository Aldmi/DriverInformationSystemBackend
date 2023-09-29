using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain;

/// <summary>
/// Локомотив
/// </summary>
public class Locomotive : Entity<Guid>
{
    /// <summary>
    /// Уникальный номер вагона
    /// </summary>
    public CarrigeNumber CarrigeNumber { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public IpCamera IpCameraFirst { get; private set;}
    
    /// <summary>
    /// 
    /// </summary>
    public IpCamera IpCameraSecond { get; private set;}
    
    
    private Locomotive(
        CarrigeNumber carrigeNumber,
        IpCamera ipCameraFirst,
        IpCamera ipCameraSecond)
    {
        CarrigeNumber = carrigeNumber;
        IpCameraFirst = ipCameraFirst;
        IpCameraSecond = ipCameraSecond;
    }


    public static Result<Locomotive> Create(CarrigeNumber carrigeNumber, IpCamera ipCameraFirst, IpCamera ipCameraSecond)
    {
        var obj = new Locomotive(carrigeNumber, ipCameraFirst, ipCameraSecond)
        {
            Id = Guid.NewGuid()
        };
        return obj;
    }
}