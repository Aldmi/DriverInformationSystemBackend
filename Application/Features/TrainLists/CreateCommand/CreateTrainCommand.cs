using Application.Common;
using Application.Domain.TrainAgregat;
using Application.Interfaces;
using Application.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.CreateCommand;


public class CreateTrainController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPost("/api/trains")]
    public async Task<ActionResult<Guid>> Create([FromBody]CreateTrainCommand command)
    {
        return await Mediator.Send(command);
    }
}


public class CreateTrainCommand : IRequest<Guid>
{
    public string? Name { get; init; }
    public LocomotiveDto LocomotiveOne { get; init; }
    public LocomotiveDto LocomotiveTwo { get; init; }
    public CarrigeDto[] Carriges { get; init; }
}




internal sealed class CreateTrainCommandHandler : IRequestHandler<CreateTrainCommand, Guid>
{
    private readonly ITrainRepository _trainRepository;

    public CreateTrainCommandHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

        
    public async Task<Guid> Handle(CreateTrainCommand request, CancellationToken cancellationToken)
    {
        var locomotiveOne = Locomotive.Create(
            new CarrigeNumber(request.LocomotiveOne.UniqCarrigeNumber),
            new IpCamera(request.LocomotiveOne.CameraFirstIpAddress),
            new IpCamera(request.LocomotiveOne.CameraSecondIpAddress)).Value;
            
        var locomotiveTwo = Locomotive.Create(
            new CarrigeNumber(request.LocomotiveTwo.UniqCarrigeNumber),
            new IpCamera(request.LocomotiveTwo.CameraFirstIpAddress),
            new IpCamera(request.LocomotiveTwo.CameraSecondIpAddress)).Value;

        var carriges = request.Carriges.Select(carrigeDto => Carrige.Create(
            new CarrigeNumber(carrigeDto.UniqCarrigeNumber),
            1,
            new IpCamera(carrigeDto.CameraFirstIpAddress),
            new IpCamera(carrigeDto.CameraSecondIpAddress)).Value
        ).ToArray();

        var train = Train.Create(request.Name, locomotiveOne, locomotiveTwo, carriges).Value;

        var id= await _trainRepository.AddOrReplace(train);
        return id!.Value;
    }
}