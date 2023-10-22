using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.TrainAgregat;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.RouteMetroList.DeleteCommand;


public class DeleteRouteMetroController : ApiControllerBase
{
    [HttpDelete("/api/routes/{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteRouteMetroCommand { Id = id });
        return NoContent();
    }
}


public class DeleteRouteMetroCommand : IRequest
{
    public Guid Id { get; init; }
}


internal sealed class DeleteTrainCommandHandler : IRequestHandler<DeleteRouteMetroCommand>
{
    private readonly IRouteMetroRepository _routeMetroRepository;
    
    public DeleteTrainCommandHandler(IRouteMetroRepository routeMetroRepository)
    {
        _routeMetroRepository = routeMetroRepository;
    }

    public async Task Handle(DeleteRouteMetroCommand request, CancellationToken cancellationToken) //TODO: передавать cancellationToken из Handle
    {
        var res= await _routeMetroRepository.DeleteAsync(request.Id);
        if (!res)
        {
            throw new DeleteCommandException(nameof(Train), request.Id); //TODO: Возврат ошибок из Handle через Exception заменить на Result<T>, с обработкой Fail статуса в Middleware
        }
    }
}