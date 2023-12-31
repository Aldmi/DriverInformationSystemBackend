﻿using Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Features.RouteMetroList.CreateCommand;


/// <summary>
/// 1.Проверить ....
/// </summary>
public class CreateRouteMetroValidator : AbstractValidator<CreateRouteMetroCommand>
{
    public CreateRouteMetroValidator(IRouteMetroRepository routeMetroRepository)
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
        
        
        //ПРОВЕРКА В БД
        RuleFor(v => v).CustomAsync(async (obj, context, token) =>
        {
            var isExistRouteByName= await routeMetroRepository.IsExistAsync(rm => rm.Name == obj.Name);
            if (isExistRouteByName)
            {
                context.AddFailure(new ValidationFailure("Route.Name", $"{obj.Name} Уже присутсвтует в БД"));
            }
        });
    }
}


