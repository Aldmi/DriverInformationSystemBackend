﻿using Application.Common;
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
    public TrainDto Train { get; set; }
}


/// <summary>
/// 1.Проверить IpCamera - regex IP
/// 2.Проверить UniqCarrigeNumber - 4 символа
/// 3.Проверить UniqCarrigeNumber - 4 символа
/// 4.Carriges кол-во элементов от 4-6
/// 4.Проверить уникальность IpCamera и UniqCarrigeNumber в пределах TrainDto
/// 5.Провреить уникальность IpCamera и UniqCarrigeNumber переданного TrainDto с сохраненными в БД Trains
/// </summary>
public class CreateTodoListCommandValidator : AbstractValidator<CreateTrainCommand>
{
    private readonly ITrainRepository _trainRepository;
    
    public CreateTodoListCommandValidator(ITrainRepository trainRepository)
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


    internal sealed class CreateTodoListCommandHandler : IRequestHandler<CreateTrainCommand, Guid>
    {
        private readonly ITrainRepository _trainRepository;

        public CreateTodoListCommandHandler(ITrainRepository trainRepository)
        {
            _trainRepository = trainRepository;
        }

        
        public async Task<Guid> Handle(CreateTrainCommand request, CancellationToken cancellationToken)
        {
            var trainDto = request.Train;
            
            var locomotiveOne = Locomotive.Create(
                new CarrigeNumber(trainDto.LocomotiveOne.UniqCarrigeNumber),
                new IpCamera(trainDto.LocomotiveOne.CameraFirstIpAddress),
                new IpCamera(trainDto.LocomotiveOne.CameraSecondIpAddress)).Value;
            
            var locomotiveTwo = Locomotive.Create(
                new CarrigeNumber(trainDto.LocomotiveTwo.UniqCarrigeNumber),
                new IpCamera(trainDto.LocomotiveTwo.CameraFirstIpAddress),
                new IpCamera(trainDto.LocomotiveTwo.CameraSecondIpAddress)).Value;

            var carriges = trainDto.Carriges.Select(carrigeDto => Carrige.Create(
                new CarrigeNumber(carrigeDto.UniqCarrigeNumber),
                1,
                new IpCamera(carrigeDto.CameraFirstIpAddress),
                new IpCamera(carrigeDto.CameraSecondIpAddress)).Value
            ).ToArray();

            var train = Train.Create(trainDto.Name, locomotiveOne, locomotiveTwo, carriges).Value;

            var id= await _trainRepository.AddOrReplace(train);
            return id!.Value;
        }
    }
}