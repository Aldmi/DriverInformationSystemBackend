using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain.RouteMetroAgregat;


/// <summary>
/// Единица работы оповещения
/// </summary>
public class UowAlert : ValueObject<UowAlert>
{
    public string StationTag { get; private set; }
    public string Description { get; private set; }
    public IReadOnlyList<SoundMessage> SoundMessages { get; private set;}
    public Ticker Ticker { get; private set;}
    
    
    public UowAlert(string stationTag, string description, IReadOnlyList<SoundMessage> soundMessages, Ticker ticker)
    {
        StationTag = stationTag;
        Description = description;
        SoundMessages = soundMessages;
        Ticker = ticker;
    }


    public bool EqualsByStationTag(UowAlert other) => StationTag == other.StationTag;
    
    protected override bool EqualsCore(UowAlert other)
    {
        var res = StationTag.Equals(other.StationTag) &&
                  Description.Equals(other.Description) &&
                  Ticker.Equals(other.Ticker) &&
                  SoundMessages.SequenceEqual(other.SoundMessages);

        return res;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            var hashCode = (StationTag.GetHashCode() * 397) +
                           (Description.GetHashCode() * 559) +
                           Ticker.GetHashCode();
            return hashCode;
        }
    }
}



