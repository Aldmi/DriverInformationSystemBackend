using Application.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.Persones.GetListQuery;

public class GetPersoneListController : ApiControllerBase
{
    [HttpGet("/persones")]
    public async Task<ActionResult<List<PersoneVm>>> Get()
    {
        return await Mediator.Send(new GetPersoneListQuery());
    }
}

public class GetPersoneListQuery: IRequest<List<PersoneVm>>
{
}


public class PersoneVm
{
    public Guid Id { get; init; }
    public string Name { get;  init;}
    public string Password { get;  init; }
    public string RoleName { get;  init; }
}


internal sealed class GetTrainListHandler : IRequestHandler<GetPersoneListQuery, List<PersoneVm>>
{
    private readonly IPersoneRepository _personeRepository;

    public GetTrainListHandler(IPersoneRepository personeRepository)
    {
        _personeRepository = personeRepository;
    }
    
    
    public async Task<List<PersoneVm>> Handle(GetPersoneListQuery request, CancellationToken cancellationToken)
    {
        var personeList= await _personeRepository.ListAsync();
        var personeListVm = personeList.Select(person => new PersoneVm()
        {
            Id = person.Id,
            Name = person.Name,
            Password = person.Password,
            RoleName = person.Role.Name
        }).ToList();

        return personeListVm;
    }
}
