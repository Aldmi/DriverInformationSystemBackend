using CSharpFunctionalExtensions;

namespace Application.Domain;

public class MetroTrain : Entity<Guid>
{
    public MetroTrain()
    {
        
    }
    public string Name { get; set; }
    // private List<MetroLocomotive> Locomotives { get; set; } = new();
    // private List<MetroCarrige> Carriges { get; set;} = new();
    //
    //
    //
    // public Result AddCarige(MetroCarrige carrige)
    // {
    //     if (Locomotives.Count >= 5)
    //         return Result.Failure("Вагонов не может быть больше 5");
    //     
    //     Carriges.Add(carrige);
    //     return Result.Success();
    // }
    //
    // public Result AddLocomotive(MetroLocomotive locomotive)
    // {
    //     if (Locomotives.Count >= 2)
    //         return Result.Failure("Локомотивов не может быть больше 2");
    //     
    //     Locomotives.Add(locomotive);
    //     return Result.Success();
    // }    // private List<MetroLocomotive> Locomotives { get; set; } = new();
    // private List<MetroCarrige> Carriges { get; set;} = new();
    //
    //
    //
    // public Result AddCarige(MetroCarrige carrige)
    // {
    //     if (Locomotives.Count >= 5)
    //         return Result.Failure("Вагонов не может быть больше 5");
    //     
    //     Carriges.Add(carrige);
    //     return Result.Success();
    // }
    //
    // public Result AddLocomotive(MetroLocomotive locomotive)
    // {
    //     if (Locomotives.Count >= 2)
    //         return Result.Failure("Локомотивов не может быть больше 2");
    //     
    //     Locomotives.Add(locomotive);
    //     return Result.Success();
    // }
}