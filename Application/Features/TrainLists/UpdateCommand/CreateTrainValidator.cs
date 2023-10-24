using Application.Common.Extensions;
using Application.Common.Regex;
using Application.Features.TrainLists.CreateCommand;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Features.TrainLists.UpdateCommand;


/// <summary>
/// 1.Проверить IpCamera - regex IP
/// 2.Проверить UniqCarrigeNumber - 4 символа
/// 3.Carriges кол-во элементов от 4-6
/// 4.Проверить уникальность IpCamera и UniqCarrigeNumber в пределах TrainDto
/// 5.Провреить уникальность IpCamera и UniqCarrigeNumber переданного TrainDto с сохраненными в БД Trains
/// </summary>
public class UpdateTrainValidator : AbstractValidator<UpdateTrainCommand>
{
    public UpdateTrainValidator(ITrainRepository trainRepository)
    {
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
        RuleFor(v => v).Custom((obj, context) =>
        {
            var carrigeNumberList = obj.Carriges.Select(c => c.UniqCarrigeNumber).ToList();
            carrigeNumberList.Add(obj.LocomotiveOne.UniqCarrigeNumber);
            carrigeNumberList.Add(obj.LocomotiveTwo.UniqCarrigeNumber);
            var dublicate = carrigeNumberList.FindDublicate();
            if (dublicate.Any())
            {
                context.AddFailure(new ValidationFailure("Номера вагонов uniqCarrigeNumber", $"{string.Join(", ", dublicate)} повторяются"));
            }
        });

        //cameraIpAddress - повторы внутри переданной command
        RuleFor(v => v).Custom((obj, context) =>
        {
            var ipAddressList = obj.LocomotiveOne.CameraIpAddress.ToList();
            ipAddressList.AddRange(obj.LocomotiveTwo.CameraIpAddress);
            ipAddressList.AddRange(obj.Carriges.Select(c=>c.CameraFirstIpAddress));
            ipAddressList.AddRange(obj.Carriges.Select(c=>c.CameraSecondIpAddress));
            var dublicate= ipAddressList.FindDublicate();
            if (dublicate.Any())
            {
                context.AddFailure(new ValidationFailure("Ip адресса камер", $"{string.Join(", ", dublicate)} повторяются"));
            }
        });
        
        //ПРОВЕРКА В БД
        RuleFor(v => v).CustomAsync(async (obj, context, token) =>
        {
            var isExist= await trainRepository.IsExistAsync(train =>train.Id == obj.IdTrain);
            if (!isExist)
            {
                context.AddFailure(new ValidationFailure("IdTrain отсутствует в БД", $"{obj.IdTrain}"));
            }
        });
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


