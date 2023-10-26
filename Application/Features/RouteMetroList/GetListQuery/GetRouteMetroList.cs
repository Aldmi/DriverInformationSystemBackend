using Application.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.GetListQuery;

public class GetRouteMetroListController : ApiControllerBase
{
    [HttpGet("/api/routes")]
    public async Task<ActionResult<List<RouteMetroVm>>> Get()
    {
        return await Mediator.Send(new GetRouteMetroListQuery());
    }
}


public class GetRouteMetroListQuery: IRequest<List<RouteMetroVm>>
{
}


internal sealed class GetTrainListHandler : IRequestHandler<GetRouteMetroListQuery, List<RouteMetroVm>>
{
    private readonly IRouteMetroRepository _routeMetroRepository;
    
    public GetTrainListHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }

    public async Task<List<RouteMetroVm>> Handle(GetRouteMetroListQuery request, CancellationToken cancellationToken)
    {
        var routesList = await _routeMetroRepository.ListAsync();
        
        var trainListVm= routesList.Select(route =>
        {
            return new RouteMetroVm()
            {
                Id = route.Id,
                Name = route.Name,
                Gender = route.Gender,
                Uows = route.Uows.Select(uow=>
                {
                    var soundMessagesVm = uow.SoundMessages.Select(sm => new SoundMessageVm {Name = sm.Name, Url = sm.Url}).ToArray();
                    var tickerVm = new TickerVm{Message = uow.Ticker.Message};
                    return new UowAlertVm{StationTag = uow.StationTag, Description = uow.Description, SoundMessages = soundMessagesVm, Ticker = tickerVm};
                }).ToArray()
            };
        }).ToList();
        
        return trainListVm;
    }
}

