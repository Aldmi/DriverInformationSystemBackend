using CSharpFunctionalExtensions;

namespace Application.ValueObjects;

/// <summary>
/// Уникальный номер вагона (присваивается вагону на заводе)
/// </summary>
public class IdCarrigeNumber : ValueObject
{
    public string CarrigeNumber { get;}

    public IdCarrigeNumber(string carrigeNumber)
    {
        CarrigeNumber = carrigeNumber;
    }
    

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return CarrigeNumber;
    }
}