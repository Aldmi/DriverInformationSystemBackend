using Application.Common;
using Application.Domain;
using Application.Interfaces;
using Application.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.CreateCommand;


public class CreateTrainController : ApiControllerBase
{
    [HttpPost("/api/trains")]
    public async Task<ActionResult<Guid>> Create(CreateTrainCommand command)
    {
        return await Mediator.Send(command);
    }
}


//[Authorize]
public class CreateTrainCommand : IRequest<Guid>
{
    public string? Name { get; set; }
    public LocomotiveDto LocomotiveOne { get; set; }
    public LocomotiveDto LocomotiveTwo { get; set; }
    public CarrigeDto[] Carriges { get; set; }
}


/// <summary>
/// 1.Проверить IpCamera - regex IP
/// 2.Проверить UniqCarrigeNumber - 4 символа
/// 3.Проверить UniqCarrigeNumber - 4 символа
/// 4.Carriges кол-во элементов от 4-6
/// 4.Проверить уникальность IpCamera и UniqCarrigeNumber в пределах TrainDto
/// 5.Провреить уникальность IpCamera и UniqCarrigeNumber переданного TrainDto с сохраненными в БД Trains
/// </summary>
public class CreateTrainValidator : AbstractValidator<CreateTrainCommand>
{
    private readonly ITrainRepository _trainRepository;
    
    public CreateTrainValidator(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;

        // RuleFor(v => v.Title)
        //     .NotEmpty().WithMessage("Title is required.")
        //     .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
        //     .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
    }

    private Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        // return _context.TodoLists
        //     .AllAsync(l => l.Title != title, cancellationToken);
        return Task.FromResult(true);
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
}