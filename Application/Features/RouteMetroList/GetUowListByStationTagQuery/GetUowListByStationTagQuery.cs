using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.RouteMetroAgregat;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.GetUowListByStationTagQuery;

public class GetUowListByStationTagController : ApiControllerBase
{
    [HttpGet("/api/routes/getUowListByStationTag/{routeName}/{stationTag}")]
    public async Task<ActionResult<UowAlertVm[]>> Get(string routeName, string stationTag)
    {
        return await Mediator.Send(new GetUowListByStationTagQuery()
        {
            RouteName = routeName,
            StationTag = stationTag
        });
    }
}


public class GetUowListByStationTagQuery: IRequest<UowAlertVm[]>
{
    public string RouteName { get; init; }
    public string StationTag { get; init; }
}


internal sealed class GetRouteMetroItemHandler : IRequestHandler<GetUowListByStationTagQuery, UowAlertVm[]>
{
    private readonly IRouteMetroRepository _routeMetroRepository;
    public GetRouteMetroItemHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }

    public async Task<UowAlertVm[]> Handle(GetUowListByStationTagQuery request, CancellationToken cancellationToken)
    {
        var route = await _routeMetroRepository.GetSingleAsync(route => route.Name == request.RouteName);
        if (route == null) {
            throw new NotFoundException(nameof(RouteMetro), request.RouteName);
        }

        var uowsArray = route.Uows.Where(alert => alert.StationTag == request.StationTag).ToArray();
        if (!uowsArray.Any())
        {
            throw new NotFoundException($"{request.StationTag} не найденн в массиве Uows для маршрута {request.RouteName}");
        }
        
        var uowsArrayVm= uowsArray.Select(alert =>
            {
                return new UowAlertVm()
                {
                    StationTag = alert.StationTag,
                    Description = alert.Description,
                    Ticker = new TickerVm
                    {
                        Message = alert.Ticker.Message
                    },
                    SoundMessages = alert.SoundMessages.Select(sm=> new SoundMessageVm
                    {
                        Name = sm.Name,
                        Url = sm.Url
                    }).ToArray()
                };
            })
            .ToArray();
        
        return uowsArrayVm;
    }
}

