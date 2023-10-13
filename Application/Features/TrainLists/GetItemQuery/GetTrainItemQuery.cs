using Application.Common;
using Application.Common.Exceptions;
using Application.Domain;
using Application.Domain.TrainAgregat;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.GetItemQuery;

public class GetTrainItemController : ApiControllerBase
{
    [HttpGet("/api/trains/{id:guid}")]
    public async Task<ActionResult<TrainItemVm>> Get(Guid id)
    {
        return await Mediator.Send(new GetTrainItemQuery()
        {
            Id = id
        });
    }
}


public class GetTrainItemQuery: IRequest<TrainItemVm>
{
    public Guid Id { get; init; }
}


internal sealed class GetTrainItemHandler : IRequestHandler<GetTrainItemQuery, TrainItemVm>
{
    private readonly ITrainRepository _trainRepository;

    public GetTrainItemHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public async Task<TrainItemVm> Handle(GetTrainItemQuery request, CancellationToken cancellationToken)
    {
        var train = await _trainRepository.GetByIdAsync(request.Id);
        if (train == null)
        {
            throw new NotFoundException(nameof(Train), request.Id);
        }
        
        var trainVm= new TrainItemVm
        {
            Id = train.Id,
            Name = train.Name,
            LocomotiveOne = new LocomotiveItemVm()
            {
                UniqCarrigeNumber = train.LocomotiveOne.CarrigeNumber.UniqNumber,
                CameraIpAddress = train.LocomotiveOne.IpCameraArray.Select(ip=>ip.IpAddress).ToArray()
            },
            LocomotiveTwo = new LocomotiveItemVm()
            {
                UniqCarrigeNumber = train.LocomotiveTwo.CarrigeNumber.UniqNumber,
                CameraIpAddress = train.LocomotiveTwo.IpCameraArray.Select(ip=>ip.IpAddress).ToArray()
            },
            Carriges = train.Carriges.Select(carrige => new CarrigeItemVm()
            {
                UniqCarrigeNumber = carrige.CarrigeNumber.UniqNumber,
                CameraFirstIpAddress = carrige.IpCameraFirst.IpAddress,
                CameraSecondIpAddress = carrige.IpCameraSecond.IpAddress
            }).ToArray()
        };

        return trainVm;
    }
}

