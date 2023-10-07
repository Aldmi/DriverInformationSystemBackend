using CSharpFunctionalExtensions;

namespace Application.ValueObjects;

public class Ticker : ValueObject<Ticker>
{
    public string Message { get; }

    
    public Ticker(string message)
    {
        Message = message;
    }

    
    protected override bool EqualsCore(Ticker other) => Message == other.Message;
    
    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Message.GetHashCode();
            hashCode = (hashCode * 397);
            return hashCode;
        }
    }
}