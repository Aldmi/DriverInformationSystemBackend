﻿namespace Application.Features.RouteMetroList.CreateCommand;

public class UowAlertDto
{ 
    public string StationTag { get; init; }
   public string Description { get; init;  }
   public TickerDto Ticker { get; init; }
   public SoundMessageDto[] SoundMessages { get; init; }
}

public class SoundMessageDto
{
    public string Name{ get; init; }
    public string Url { get; init; }
}

public class TickerDto
{
    public string Message { get; init; }
}

