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


    public Result ChangeUowsList(UowAlert[] uowsNew)
    {
        if (!uowsNew.Any()) {
            return Result.Failure($"Список {nameof(uowsNew)} не может быть пуст");
        }

        var stationTag= uowsNew.First().StationTag;
        
        var index = Array.FindIndex(Uows, uowAlert => uowAlert.StationTag == stationTag);
        if (index == -1) {
            return Result.Failure($"{stationTag} не найден по этому маршруту {Name}");
        }
        
        var countUows = Uows.Count(uow => uow.StationTag == stationTag);
        var countUowsNew = uowsNew.Length;
        
        if (countUows == countUowsNew)
        {
            //РАВНО. Заменяем все элементы
            foreach (var uowNew in uowsNew)
            {
                Uows[index] = uowNew;
                index++;
            }
        }
        else if (countUows > countUowsNew)
        {
            //ПЕРЕДАЛИ МЕНЬШЕ. Заменяем элементы, лишние удаляем
        }
        else
        {
            //ПЕРЕДАЛИ БОЛЬШЛЕ. Заменяем элементы, новые добавляем
        }
        
        
        return Result.Success();
    }
    
    
}