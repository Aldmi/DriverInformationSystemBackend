using Application.Common;
using Application.Common.Exceptions;
using Application.Domain;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.DeleteCommand;


public class DeleteTrainCommandController : ApiControllerBase
{
    [HttpDelete("/api/trains/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteTrainCommand { Id = id });
        return NoContent();
    }
}


public class DeleteTrainCommand : IRequest
{
    public Guid Id { get; set; }
}


internal sealed class DeleteTrainCommandHandler : IRequestHandler<DeleteTrainCommand>
{
    private readonly ITrainRepository _trainRepository;

    public DeleteTrainCommandHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public async Task Handle(DeleteTrainCommand request, CancellationToken cancellationToken) //TODO: передавать cancellationToken из Handle
    {
        var res= await _trainRepository.DeleteAsync(request.Id);
        if (!res)
        {
            throw new DeleteCommandException(nameof(Train), request.Id); //TODO: Возврат ошибок из Handle через Exception заменить на Result<T>, с обработкой Fail статуса в Middleware
        }
    }
}