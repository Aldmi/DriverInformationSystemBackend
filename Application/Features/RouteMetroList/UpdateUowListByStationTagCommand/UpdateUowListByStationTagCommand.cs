using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.RouteMetroAgregat;
using Application.Interfaces;
using Application.ValueObjects;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.UpdateUowListByStationTagCommand;


public class UpdateUowListByStationTagController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPut("/api/routes/updateUowListByStationTag/{routeName}/{stationTag}")]
    public async Task<ActionResult<Guid?>> Update(
        [FromRoute]string routeName,
        [FromRoute]string stationTag,
        [FromBody]UowAlertDto[] uows)
    {
        var result=await  Mediator.Send(new UpdateUowListByStationTagCommand {
            RouteName = routeName,
            StationTag = stationTag,
            Uows = uows
        });
        if (result.IsSuccess) {
            return result.Value;
        }
        return result.Error switch {
            _ => Problem(title:"ошибка updateUowListByStationTag", detail:result.Error)
        };
    }
}


public class UpdateUowListByStationTagCommand : IRequest<Result<Guid>>
{
    public string RouteName { get; init; }
    public string StationTag { get; init; }
    public UowAlertDto[] Uows { get; init; }
}




internal sealed class CreateRouteMetroCommandHandler : IRequestHandler<UpdateUowListByStationTagCommand, Result<Guid>>
{
    private readonly IRouteMetroRepository _routeMetroRepository;
    public CreateRouteMetroCommandHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }


    public async Task<Result<Guid>> Handle(UpdateUowListByStationTagCommand request, CancellationToken cancellationToken)
    {
        var route = await _routeMetroRepository.GetSingleAsync(route => route.Name == request.RouteName);
        if (route == null) {
            throw new NotFoundException(nameof(RouteMetro), request.RouteName);
        }
        
        var uowsNew = request.Uows.Select(uowDto =>
            new UowAlert(
                uowDto.StationTag,
                uowDto.Description,
                uowDto.SoundMessages.Select(sm => new SoundMessage(sm.Name, sm.Url)).ToList(),
                new Ticker(uowDto.Ticker.Message))
        ).ToArray();

        var result = route.ChangeUowsList(uowsNew);
        if (result.IsFailure) {
            return result.ConvertFailure<Guid>();
        }

        var res = await _routeMetroRepository.AddOrReplace(route);
        return res == null ? Result.Failure<Guid>("Обновление не выполнено") : Result.Success(res.Value);
    }
}