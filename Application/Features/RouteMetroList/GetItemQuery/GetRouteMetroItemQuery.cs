using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.RouteMetroAgregat;
using Application.Domain.TrainAgregat;
using Application.Features.TrainLists.GetItemQuery;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.GetItemQuery;

public class GetRouteMetroItemController : ApiControllerBase
{
    [HttpGet("/api/routes/{id:guid}")]
    public async Task<ActionResult<RouteMetroVm>> Get(Guid id)
    {
        return await Mediator.Send(new GetRouteMetroItemQuery()
        {
            Id = id
        });
    }
}


public class GetRouteMetroItemQuery: IRequest<RouteMetroVm>
{
    public Guid Id { get; init; }
}


internal sealed class GetRouteMetroItemHandler : IRequestHandler<GetRouteMetroItemQuery, RouteMetroVm>
{
    private readonly IRouteMetroRepository _routeMetroRepository;
    public GetRouteMetroItemHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }

    public async Task<RouteMetroVm> Handle(GetRouteMetroItemQuery request, CancellationToken cancellationToken)
    {
        var route = await _routeMetroRepository.GetByIdAsync(request.Id);
        if (route == null)
        {
            throw new NotFoundException(nameof(RouteMetro), request.Id);
        }
        
        var trainVm= new RouteMetroVm
        {
            Id = route.Id,
            Name = route.Name,
            Gender = route.Gender,
            Uows = route.Uows.Select(uow =>
            {
                var soundMessagesVm = uow.SoundMessages.Select(sm => new SoundMessageVm {Name = sm.Name, Url = sm.Url}).ToArray();
                var tickerVm = new TickerVm {Message = uow.Ticker.Message};
                return new UowAlertVm{StationTag = uow.StationTag, Description = uow.Description, SoundMessages = soundMessagesVm, Ticker = tickerVm};
            }).ToArray()
        };

        return trainVm;
    }
}

