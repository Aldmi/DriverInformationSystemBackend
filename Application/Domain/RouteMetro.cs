using CSharpFunctionalExtensions;

namespace Application.Domain;

/// <summary>
/// Маршрут метро
/// </summary>
public class RouteMetro : Entity<Guid>
{
    public string Name { get; private set;}
    
    public RouteMetro(string name)
    {
        Name = name;
    }
}