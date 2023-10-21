using FluentValidation;

namespace Application.Features.RouteMetroList.CreateCommand;


/// <summary>
/// 1.Проверить ....
/// </summary>
public class CreateRouteMetroValidator : AbstractValidator<CreateRouteMetroCommand>
{
    public CreateRouteMetroValidator()
    {
        RuleFor(v => v.Name)
            .NotNull().WithMessage("не может быть null")
            .NotEmpty().WithMessage("не может быть пуст");

        RuleFor(v => v.Uows)
            .Must(items => items != null && items.Any())
            .WithMessage("Uows не может быть пуст");
        
        RuleForEach(v => v.Uows)
            .ChildRules(uowAlert =>
            {
                uowAlert.RuleFor(v => v.Ticker).NotNull().WithMessage("Ticker не может быть null");
                uowAlert.RuleFor(v => v.Ticker.Message)
                    .NotNull().WithMessage("Ticker.Message не может быть null")
                    .NotEmpty().WithMessage("Ticker.Message не может быть пуст");
                
                uowAlert.RuleFor(v => v.SoundMessages)
                    .Must(items => items != null && items.Any())
                    .WithMessage("SoundMessages не может быть пуст");
                uowAlert.RuleForEach(v => v.SoundMessages)
                    .ChildRules(soundMessage =>
                    {
                        soundMessage.RuleFor(v=>v.Name)
                            .NotNull().WithMessage("soundMessage.Name не может быть null")
                            .NotEmpty().WithMessage("soundMessage.Name не может быть пуст");
                        
                        soundMessage.RuleFor(v=>v.Url)
                            .NotNull().WithMessage("soundMessage.Url не может быть null")
                            .NotEmpty().WithMessage("soundMessage.Url не может быть пуст");
                    });
            });
    }
}


