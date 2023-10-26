using Application.ValueObjects;

namespace Application.Domain.RouteMetroAgregat;


/// <summary>
/// Единица работы оповещения
/// </summary>
public class UowAlert
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
}



