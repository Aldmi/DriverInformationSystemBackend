
namespace Application.Features.RouteMetroList.GetUowListByStationTagQuery;

public class UowAlertVm
{
    public string StationTag { get; init; }
    public string Description { get; init; }
    public TickerVm Ticker { get; init; }
    public SoundMessageVm[] SoundMessages { get; init; }
}

public class SoundMessageVm
{
    public string Name{ get; init; }
    public string Url { get; init; }
}

public class TickerVm
{
    public string Message { get; init; }
}
