using CSharpFunctionalExtensions;

namespace Application.Domain.PersonAgregat;

public class Person: Entity<Guid>
{
    private Person(string name, string password, Role role)
    {
        Name = name;
        Password = password;
        Role = role;
    }

    public string Name { get; private set;}
    public string Password { get; private set; }
    public Role Role { get; private set; }
    
    
    public static Result<Person> Create(
        string name,
        string password,
        Role role
    )
    {
        return new Person(name, password, role);
    }
}