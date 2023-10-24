using Application.Common;
using Application.Domain.TrainAgregat;
using Application.Features.TrainLists.CreateCommand;
using Application.Interfaces;
using Application.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.UpdateCommand;


public class UpdateTrainController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPut("/api/trains")]
    public async Task<ActionResult<string>> Update([FromBody]UpdateTrainCommand command)
    {
        return await Mediator.Send(command);
    }
}


public class UpdateTrainCommand : IRequest<string>
{
    public Guid IdTrain { get; init; }
    public string? Name { get; init; }
    public LocomotiveDto LocomotiveOne { get; init; }
    public LocomotiveDto LocomotiveTwo { get; init; }
    public CarrigeDto[] Carriges { get; init; }
}


internal sealed class UpdateTrainCommandHandler : IRequestHandler<UpdateTrainCommand, string>
{
    private readonly ITrainRepository _trainRepository;
    public UpdateTrainCommandHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

        
    public async Task<string> Handle(UpdateTrainCommand request, CancellationToken cancellationToken)
    {
        var locomotiveOne = Locomotive.Create(
            new CarrigeNumber(request.LocomotiveOne.UniqCarrigeNumber),
            request.LocomotiveOne.CameraIpAddress.Select(ip => new IpCamera(ip)).ToArray()).Value;
            
        var locomotiveTwo = Locomotive.Create(
            new CarrigeNumber(request.LocomotiveTwo.UniqCarrigeNumber),
            request.LocomotiveTwo.CameraIpAddress.Select(ip => new IpCamera(ip)).ToArray()).Value;

        var carriges = request.Carriges.Select(carrigeDto => Carrige.Create(
            new CarrigeNumber(carrigeDto.UniqCarrigeNumber),
            1,
            new IpCamera(carrigeDto.CameraFirstIpAddress),
            new IpCamera(carrigeDto.CameraSecondIpAddress)).Value
        ).ToArray();

        var train = Train.CreateWithId(request.IdTrain, request.Name, locomotiveOne, locomotiveTwo, carriges).Value;

        var id= await _trainRepository.AddOrReplace(train);
        return (id == request.IdTrain) ? "Update Ok" : "Update Error. поезд был добавлен, а не обновлен";
    }
}