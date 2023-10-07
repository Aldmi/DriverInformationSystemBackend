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
}