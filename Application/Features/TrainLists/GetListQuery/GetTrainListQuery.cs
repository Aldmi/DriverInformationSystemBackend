using Application.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.GetListQuery;

public class GetTrainListController : ApiControllerBase
{
    [HttpGet("/api/trains")]
    public async Task<ActionResult<List<TrainVm>>> Get()
    {
        return await Mediator.Send(new GetTrainListQuery());
    }
}


public class GetTrainListQuery: IRequest<List<TrainVm>>
{
}


internal sealed class GetTrainListHandler : IRequestHandler<GetTrainListQuery, List<TrainVm>>
{
    private readonly ITrainRepository _trainRepository;

    public GetTrainListHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public async Task<List<TrainVm>> Handle(GetTrainListQuery request, CancellationToken cancellationToken)
    {
        var trainList = await _trainRepository.ListAsync();
        
        var trainListVm= trainList.Select(train =>
        {
            return new TrainVm()
            {
                Id = train.Id,
                Name = train.Name,
                LocomotiveOne = new LocomotiveVm()
                {
                    UniqCarrigeNumber = train.LocomotiveOne.CarrigeNumber.UniqNumber,
                    CameraFirstIpAddress = train.LocomotiveOne.IpCameraFirst.IpAddress,
                    CameraSecondIpAddress = train.LocomotiveOne.IpCameraSecond.IpAddress,
                },
                LocomotiveTwo = new LocomotiveVm()
                {
                    UniqCarrigeNumber = train.LocomotiveTwo.CarrigeNumber.UniqNumber,
                    CameraFirstIpAddress = train.LocomotiveTwo.IpCameraFirst.IpAddress,
                    CameraSecondIpAddress = train.LocomotiveTwo.IpCameraSecond.IpAddress,
                },
                Carriges = train.Carriges.Select(carrige => new CarrigeVm()
                {
                    UniqCarrigeNumber = carrige.CarrigeNumber.UniqNumber,
                    CameraFirstIpAddress = carrige.IpCameraFirst.IpAddress,
                    CameraSecondIpAddress = carrige.IpCameraSecond.IpAddress
                }).ToArray()
            };
        }).ToList();
        
        return trainListVm;
    }
}

