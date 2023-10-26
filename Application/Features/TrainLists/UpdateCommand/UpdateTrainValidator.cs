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
            .Must(list => list.Length is >= 2 and <= 3).WithMessage("Кол-во вагонов должно быть от 2 до 3");
        
        RuleForEach(x => x.Carriges)
            .ChildRules(carrige =>
            {
                carrige.RuleFor(v => v.UniqCarrigeNumber)
                    .NotNull().WithMessage("не может быть null")
                    .MinimumLength(3).WithMessage("lenght должно быть не менее 3 цифр");

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
                return;
            }
            
            var carrigeNumberList = obj.Carriges.Select(c => c.UniqCarrigeNumber).ToList();
            carrigeNumberList.Add(obj.LocomotiveOne.UniqCarrigeNumber);
            carrigeNumberList.Add(obj.LocomotiveTwo.UniqCarrigeNumber);
            
            var ipAddressList = obj.LocomotiveOne.CameraIpAddress.ToList();
            ipAddressList.AddRange(obj.LocomotiveTwo.CameraIpAddress);
            ipAddressList.AddRange(obj.Carriges.Select(c=>c.CameraFirstIpAddress));
            ipAddressList.AddRange(obj.Carriges.Select(c=>c.CameraSecondIpAddress));
            
            var allTrains = await trainRepository.ListAsync(train =>train.Id != obj.IdTrain); //Все остальные поезда из БД
            foreach (var train in allTrains)
            {
                //Уникальность номера локомотива 1 
                if (carrigeNumberList.Contains(train.LocomotiveOne.CarrigeNumber.UniqNumber))
                {
                    context.AddFailure(new ValidationFailure("uniqCarrigeNumber", $"{train.Id} LocomotiveOne.uniqCarrigeNumber {train.LocomotiveOne.CarrigeNumber.UniqNumber}"));
                }
                //Уникальность номера локомотива 2 
                if (carrigeNumberList.Contains(train.LocomotiveTwo.CarrigeNumber.UniqNumber))
                {
                    context.AddFailure(new ValidationFailure("uniqCarrigeNumber", $"{train.Id} LocomotiveTwo.uniqCarrigeNumber {train.LocomotiveTwo.CarrigeNumber.UniqNumber}"));
                }
                //Уникальность номера вагонов
                var intersectCarrigesUniqNumber = carrigeNumberList.Intersect(train.Carriges.Select(c => c.CarrigeNumber.UniqNumber)).ToList();
                if (intersectCarrigesUniqNumber.Any())
                {
                    context.AddFailure(new ValidationFailure("uniqCarrigeNumber", $"{train.Id} carriges {string.Join(", ", intersectCarrigesUniqNumber)} повторяются"));
                }
                
                //Уникальность locomotiveOne.CameraIpAddress
                var locomotiveOneCameraIpAddress = ipAddressList.Intersect(train.LocomotiveOne.IpCameraArray.Select(ip=>ip.IpAddress)).ToList();
                if (locomotiveOneCameraIpAddress.Any())
                {
                    context.AddFailure(new ValidationFailure("ip_Address", $"{train.Id} LocomotiveOne.CameraIpAddress {string.Join(", ", locomotiveOneCameraIpAddress)} повторяются"));
                }
                
                //Уникальность locomotiveTwo.CameraIpAddress
                var locomotiveTwoCameraIpAddress = ipAddressList.Intersect(train.LocomotiveTwo.IpCameraArray.Select(ip=>ip.IpAddress)).ToList();
                if (locomotiveTwoCameraIpAddress.Any())
                {
                    context.AddFailure(new ValidationFailure("ip_Address", $"{train.Id} locomotiveTwo.CameraIpAddress {string.Join(", ", locomotiveTwoCameraIpAddress)} повторяются"));
                }
                
                //Уникальность carriges.cameraFirstIpAddress
                var carrigesCameraFirstIpAddress= ipAddressList.Intersect(train.Carriges.Select(c=>c.IpCameraFirst.IpAddress)).ToList();
                if (carrigesCameraFirstIpAddress.Any())
                {
                    context.AddFailure(new ValidationFailure("ip_Address", $"{train.Id} carriges.cameraFirstIpAddress {string.Join(", ", carrigesCameraFirstIpAddress)} повторяются"));
                }
                
                //Уникальность carriges.cameraSecondIpAddress
                var carrigesCameraSecondIpAddress= ipAddressList.Intersect(train.Carriges.Select(c=>c.IpCameraSecond.IpAddress)).ToList();
                if (carrigesCameraSecondIpAddress.Any())
                {
                    context.AddFailure(new ValidationFailure("ip_Address", $"{train.Id} carriges.cameraSecondIpAddress {string.Join(", ", carrigesCameraSecondIpAddress)} повторяются"));
                }
            }
        });
    }

    
     private class LocomotiveDtoValidator: AbstractValidator<LocomotiveDto> 
     {
        public LocomotiveDtoValidator()
        {
            RuleFor(v => v.UniqCarrigeNumber)
                .NotNull().WithMessage("не может быть null")
                .MinimumLength(3).WithMessage("lenght должно быть не менее 3 цифр");

            RuleForEach(x => x.CameraIpAddress)
                .ChildRules(ip =>
                {
                    ip.RuleFor(v => v)
                        .Matches(RegexStatic.IpAddress).WithMessage("Ip адресс задан не верно");
                });
        }
     }
}


