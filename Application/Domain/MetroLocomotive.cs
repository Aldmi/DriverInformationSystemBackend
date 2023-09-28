using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain;

/// <summary>
/// Вагон метро
/// </summary>
public class MetroLocomotive : Entity<IdCarrigeNumber>
{
    public MetroLocomotive(IdCarrigeNumber id, IpCamera ipCameraFirst, IpCamera ipCameraSecond)
        :base(id)
    {
        IpCameraFirst = ipCameraFirst;
        IpCameraSecond = ipCameraSecond;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public IpCamera IpCameraFirst { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public IpCamera IpCameraSecond { get; init; }
}