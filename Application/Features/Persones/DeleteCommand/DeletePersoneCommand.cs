using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.PersonAgregat;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Persones.DeleteCommand;

public class DeletePersoneController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpDelete("/persones/{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeletePersoneCommand { Id = id });
        return NoContent();
    }
}


public class DeletePersoneCommand : IRequest
{
    public Guid Id { get; init; }
}


internal sealed class DeletePersoneCommandHandler : IRequestHandler<DeletePersoneCommand>
{
    private readonly IPersoneRepository _personeRepository;

    public DeletePersoneCommandHandler(IPersoneRepository personeRepository)
    {
        _personeRepository = personeRepository;
    }
    
    public async Task Handle(DeletePersoneCommand request, CancellationToken cancellationToken)
    {
        var res= await _personeRepository.DeleteAsync(request.Id);
        if (!res)
        {
            throw new DeleteCommandException(nameof(Person), request.Id); //TODO: Возврат ошибок из Handle через Exception заменить на Result<T>, с обработкой Fail статуса в Middleware
        }
    }
}