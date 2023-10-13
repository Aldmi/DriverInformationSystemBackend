using Application.Common.Extensions;
using Application.Common.Regex;
using Application.Interfaces;
using FluentValidation;

namespace Application.Features.TrainLists.CreateCommand;


/// <summary>
/// 1.Проверить IpCamera - regex IP
/// 2.Проверить UniqCarrigeNumber - 4 символа
/// 3.Carriges кол-во элементов от 4-6
/// 4.Проверить уникальность IpCamera и UniqCarrigeNumber в пределах TrainDto
/// 5.Провреить уникальность IpCamera и UniqCarrigeNumber переданного TrainDto с сохраненными в БД Trains
/// </summary>
public class CreateTrainValidator : AbstractValidator<CreateTrainCommand>
{
    private readonly ITrainRepository _trainRepository;
    
    public CreateTrainValidator(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
        
        RuleFor(v => v.LocomotiveOne).SetValidator(new LocomotiveDtoValidator());
        RuleFor(v => v.LocomotiveTwo).SetValidator(new LocomotiveDtoValidator());

        RuleFor(x => x.Carriges)
            .Must(list => list.Length is >= 4 and <= 6).WithMessage("Кол-во вагонов должно быть от 4 до 6");
        
        RuleForEach(x => x.Carriges)
            .ChildRules(carrige =>
            {
                carrige.RuleFor(v => v.UniqCarrigeNumber)
                    .NotNull().WithMessage("не может быть null")
                    .Length(4).WithMessage("lenght должно быть 4");

                carrige.RuleFor(v => v.CameraFirstIpAddress)
                    .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
        
                carrige.RuleFor(v => v.CameraSecondIpAddress)
                    .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
            });
        
        //UniqCarrigeNumber - повторы внутри переданной command
        RuleFor(v => v).Custom((command, context) =>
        {
            var carrigeNumbers = command.Carriges.Select(c => c.UniqCarrigeNumber).ToList();
            carrigeNumbers.Add(command.LocomotiveOne.UniqCarrigeNumber);
            carrigeNumbers.Add(command.LocomotiveTwo.UniqCarrigeNumber);
            var dublicate= carrigeNumbers.FindDublicate();
            if (dublicate.Any())
            {
                context.AddFailure($"UniqCarrigeNumber {string.Join(", ", dublicate)} повторяются");
            }
        });


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

    

    
    
    private class LocomotiveDtoValidator: AbstractValidator<LocomotiveDto>
    {
        public LocomotiveDtoValidator()
        {
            RuleFor(v => v.UniqCarrigeNumber)
                .NotNull().WithMessage("не может быть null")
                .Length(4).WithMessage("lenght должно быть 4");

            RuleForEach(x => x.CameraIpAddress)
                .ChildRules(ip =>
                {
                    ip.RuleFor(v => v)
                        .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
                });
        }
    }
}





internal class CarrigeDtoValidator: AbstractValidator<CarrigeDto>
{
    public CarrigeDtoValidator()
    {
        RuleFor(v => v.UniqCarrigeNumber)
            .NotNull().WithMessage("не может быть null")
            .Length(4).WithMessage("lenght должно быть 4");

        RuleFor(v => v.CameraFirstIpAddress)
            .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
        
        RuleFor(v => v.CameraSecondIpAddress)
            .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
    }
}