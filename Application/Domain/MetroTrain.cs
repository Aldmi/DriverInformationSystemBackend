using CSharpFunctionalExtensions;

namespace Application.Domain;

public class MetroTrain : Entity
{
    public string Name { get; set; }
    private List<MetroLocomotive> Locomotives { get; } = new();
    public List<MetroCarrige> Carriges { get; } = new();


    public Result AddCarige(MetroCarrige carrige)
    {
        if (Locomotives.Count >= 5)
            return Result.Failure("Вагонов не может быть больше 5");
        
        Carriges.Add(carrige);
        return Result.Success();
    }
    
    public Result AddLocomotive(MetroLocomotive locomotive)
    {
        if (Locomotives.Count >= 2)
            return Result.Failure("Локомотивов не может быть больше 2");
        
        Locomotives.Add(locomotive);
        return Result.Success();
    }
}