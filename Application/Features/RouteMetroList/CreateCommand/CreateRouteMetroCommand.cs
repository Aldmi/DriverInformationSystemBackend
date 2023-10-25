using Application.Common;
using Application.Common.Models;
using Application.Domain.RouteMetroAgregat;
using Application.Interfaces;
using Application.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.CreateCommand;


public class CreateRouteMetroController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPost("/api/routes")]
    public async Task<ActionResult<Guid>> Create([FromBody]CreateRouteMetroCommand command)
    {
        return await Mediator.Send(command);
    }
}


public class CreateRouteMetroCommand : IRequest<Guid>
{
    public string Name { get; init; }
    public Gender Gender{ get; init; }
    public UowAlertDto[] Uows { get; init; }
}




internal sealed class CreateRouteMetroCommandHandler : IRequestHandler<CreateRouteMetroCommand, Guid>
{
    private readonly IRouteMetroRepository _routeMetroRepository;

    public CreateRouteMetroCommandHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }


    public async Task<Guid> Handle(CreateRouteMetroCommand request, CancellationToken cancellationToken)
    {
        var uowAlerts = request.Uows.Select(uowDto =>
            new UowAlert(
                uowDto.Description,
                uowDto.SoundMessages.Select(sm => new SoundMessage(sm.Name, sm.Url)).ToList(),
                new Ticker(uowDto.Ticker.Message))
        ).ToArray();

        var routeMetro = RouteMetro.Create(request.Name, request.Gender, uowAlerts).Value;
        var id= await _routeMetroRepository.AddOrReplace(routeMetro);
        return id!.Value;
    }
}