using Application.Common.Extensions;
using FluentValidation;
namespace Application.Features.TrainLists.UpdateCarrigesListCommand;


/// <summary>
/// 1.
/// </summary>
public class UpdateCarrigesListValidator : AbstractValidator<UpdateCarrigesListCommand>
{
    public UpdateCarrigesListValidator()
    {
        RuleFor(v => v.CarrigesNumberSeq).Must(seqNum =>
        {
            var isDublicated= seqNum.FindDublicate().Any();
            return !isDublicated;
        }).WithMessage("Дубликаты номеров вагонов");
        
        
        RuleForEach(x => x.CarrigesNumberSeq)
            .ChildRules(carrigeNumber =>
            {
                carrigeNumber.RuleFor(v => v)
                    .NotNull().WithMessage("не может быть null")
                    .MinimumLength(3).WithMessage("lenght должно быть не менее 3 цифр");
            });
    }
    
}
