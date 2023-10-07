using Application.Common.Models;
using Application.ValueObjects;
using CSharpFunctionalExtensions;

namespace Application.Domain.RouteMetroAgregat;


/// <summary>
/// Единица работы оповещения
/// </summary>
public class UowAlert
{
    public IReadOnlyList<SoundMessage> SoundMessages { get; private set;}
    public Ticker Ticker { get; private set;}
    
    
    public UowAlert(IReadOnlyList<SoundMessage> soundMessages, Ticker ticker)
    {
        SoundMessages = soundMessages;
        Ticker = ticker;
    }
}



