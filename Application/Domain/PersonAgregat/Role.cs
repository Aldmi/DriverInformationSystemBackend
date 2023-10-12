namespace Application.Domain.PersonAgregat;

public class Role
{
    public string Name { get; private set; }
    
    public static readonly Role Admin = new("admin");
    public static readonly Role Engineer = new("engineer");
    public static readonly Role MetroDriver = new("metro_driver");

    public Role(string name)
    {
        Name = name;
    }
}