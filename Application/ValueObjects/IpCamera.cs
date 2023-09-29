using CSharpFunctionalExtensions;

namespace Application.ValueObjects;

/// <summary>
/// Объект Ip камеры
/// </summary>
public class IpCamera : ValueObject<IpCamera>
{
    public string IpAddress { get; }
    
    public IpCamera(string ipAddress)
    {
        IpAddress = ipAddress;
    }
    
    
    protected override bool EqualsCore(IpCamera other)=>IpAddress == other.IpAddress;
    
    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = IpAddress.GetHashCode();
            hashCode = (hashCode * 397);
            return hashCode;
        }
    }
}