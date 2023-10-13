using Application.Common;
using Application.Common.Exceptions;
using Application.Domain.TrainAgregat;
using Application.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Features.TrainLists.UpdateCarrigesListCommand;

public class UpdateCarrigesListController : ApiControllerBase
{
    //[Authorize(Roles = "admin")]
    [HttpPut("/api/trains/updateCarrigesListSeq")]
    public async Task<ActionResult<Guid>> Update([FromBody]UpdateCarrigesListCommand command)
    {
        var result=await Mediator.Send(command);
        if (result.IsSuccess) {
            return result.Value;
        }
        return result.Error switch {
            _ => Problem(title:"ошибка UpdateCarrigesList", detail:result.Error)
        };
    }
}

/// <summary>
/// находим Train по Id
/// меняем порядок вагонов согласно CarigesSeq.
/// </summary>
public class UpdateCarrigesListCommand : IRequest<Result<Guid>>
{
    public Guid IdTrain { get; init; }
    public string[] CarrigesNumberSeq  { get; init; }
}



internal sealed class UpdateCarrigesListCommandHandler : IRequestHandler<UpdateCarrigesListCommand, Result<Guid>>
{
    private readonly ITrainRepository _trainRepository;

    public UpdateCarrigesListCommandHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }
    
    public async Task<Result<Guid>> Handle(UpdateCarrigesListCommand request, CancellationToken cancellationToken)
    {
        var train= await _trainRepository.GetByIdAsync(request.IdTrain);
        if (train == null)
        {
            throw new NotFoundException(nameof(Train), request.IdTrain);
        }

        if (train.Carriges.Length != request.CarrigesNumberSeq.Length)
            return Result.Failure<Guid>($"Кол-во вагонов не совпало. Надо:{train.Carriges.Length} Указано:{request.CarrigesNumberSeq.Length}");
        
        //Меняем вагоны местами
        var res= train.ChangeCarrigesSequence(request.CarrigesNumberSeq);
        if (res.IsFailure)
            return res;

        await _trainRepository.AddOrReplace(train);
        
        return res;
    }
}