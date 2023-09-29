using CSharpFunctionalExtensions;

namespace Application.Domain;

public class Train : Entity<Guid>
{
    public string? Name { get; private set;}
    public Locomotive LocomotiveOne { get; private set; }
    public Locomotive LocomotiveTwo { get; private set; }
    public Carrige[] Carriges { get; private set; }

    private Train(
        string? name,
        Locomotive locomotiveOne,
        Locomotive locomotiveTwo,
        Carrige[] carriges)
    {
        Name = name;
        LocomotiveOne = locomotiveOne;
        LocomotiveTwo = locomotiveTwo;
        Carriges = carriges;
    }
    
    public static Result<Train> Create(
        string? name,
        Locomotive locomotiveOne,
        Locomotive locomotiveTwo,
        Carrige[] carriges
        )
    {
        return carriges.Length > 5 ?
            Result.Failure<Train>("Вагонов не может быть больше 5") :
            new Train(name, locomotiveOne, locomotiveTwo, carriges);
    }
}
    
    
    