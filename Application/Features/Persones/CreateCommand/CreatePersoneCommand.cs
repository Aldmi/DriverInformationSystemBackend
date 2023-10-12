using Application.Common;
using Application.Domain.PersonAgregat;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Persones.CreateCommand;

public class CreatePersoneController: ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPost("/persones")]
    public async Task<ActionResult<Guid>> Create([FromBody]CreatePersoneCommand command)
    {
        return await Mediator.Send(command);
    }
}

public class CreatePersoneCommand : IRequest<Guid>
{
    public string Name { get; init;}
    public string Password { get; init; }
    public string RoleName { get; init; }
}


internal sealed class CreatePersoneCommandHandler : IRequestHandler<CreatePersoneCommand, Guid>
{
    private readonly IPersoneRepository _personeRepository;

    public CreatePersoneCommandHandler(IPersoneRepository personeRepository)
    {
        _personeRepository = personeRepository;
    }
    
    
    public async Task<Guid> Handle(CreatePersoneCommand request, CancellationToken cancellationToken)
    {
        var persone = Person.Create(request.Name, request.Password, new Role(request.RoleName)).Value;

        var id = await _personeRepository.AddOrReplace(persone);
        return id!.Value;
    }
}