using CSharpFunctionalExtensions;

namespace Application.ValueObjects;

/// <summary>
/// Уникальный номер вагона (присваивается вагону на заводе)
/// </summary>
public class CarrigeNumber : ValueObject<CarrigeNumber>
{
    public string UniqNumber { get; }

    public CarrigeNumber(string uniqNumber)
    {
        UniqNumber = uniqNumber;
    }
    
    
    protected override bool EqualsCore(CarrigeNumber other)=>UniqNumber == other.UniqNumber;
    
    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = UniqNumber.GetHashCode();
            hashCode = (hashCode * 397);
            return hashCode;
        }
    }
}