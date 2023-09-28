using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain;

/// <summary>
/// Вагон метро
/// </summary>
public class MetroCarrige : Entity<IdCarrigeNumber>
{
    public MetroCarrige(IdCarrigeNumber id, int seriaCarrigeNumber, IpCamera ipCameraFirst, IpCamera ipCameraSecond)
        :base(id)
    {
        SeriaCarrigeNumber = seriaCarrigeNumber;
        IpCameraFirst = ipCameraFirst;
        IpCameraSecond = ipCameraSecond;
    }

    /// <summary>
    /// Порядковый номер вагона
    /// </summary>
    public int SeriaCarrigeNumber { get; init; }

    /// <summary>
    /// Камера в начале вагона
    /// </summary>
    public IpCamera IpCameraFirst { get; init; }
    
    /// <summary>
    /// Камера в конце вагона
    /// </summary>
    public IpCamera IpCameraSecond { get; init; }
}