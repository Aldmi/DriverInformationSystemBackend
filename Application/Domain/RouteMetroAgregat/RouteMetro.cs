using Application.Common.Models;
using CSharpFunctionalExtensions;

namespace Application.Domain.RouteMetroAgregat;

/// <summary>
/// Маршрут метро
/// </summary>
public class RouteMetro : Entity<Guid>
{
    /// <summary>
    /// Название маршрута
    /// </summary>
    public string Name { get; private set;}
    
    /// <summary>
    /// Пол диктора
    /// </summary>
    public Gender Gender { get; private set;}
    
    /// <summary>
    /// Массив единиц оповещения.
    /// Задает действия при движении по маршруту
    /// </summary>
    public UowAlert[] Uows { get; private set; }
    
    private RouteMetro(string name, Gender gender, UowAlert[] uows)
    {
        Name = name;
        Gender = gender;
        Uows = uows;
    }
    
    
    public static Result<RouteMetro> Create(string name, Gender gender, UowAlert[] uows)
    {
        var obj = new RouteMetro(name, gender, uows);
        return obj; 
    }


    public Result ChangeUowsList(string stationTag, UowAlert[] uowsNew)
    {
        if (!uowsNew.Any()) {
            return Result.Failure($"Список {nameof(uowsNew)} не может быть пуст");
        }

        if (uowsNew.Any(uow => uow.StationTag != stationTag)) {
            return Result.Failure($"StationTag {stationTag} не соответсвует значеням переданным в {nameof(uowsNew)}");
        }
        
        var index = Array.FindIndex(Uows, uowAlert => uowAlert.StationTag == stationTag);
        if (index == -1) {
            return Result.Failure($"{stationTag} не найден по этому маршруту {Name}");
        }
        
        //Заменили часть массива на новый, uowsNew массив
        var countUows = Uows.Count(uow => uow.StationTag == stationTag);
        var listUows = Uows.ToList();
        listUows.RemoveRange(index, countUows);
        listUows.InsertRange(index, uowsNew);
        Uows = listUows.ToArray();
        
        return Result.Success();
    }
}