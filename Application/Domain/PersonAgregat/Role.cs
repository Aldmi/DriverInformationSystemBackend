namespace Application.Domain.PersonAgregat;

public class Role
{
    public string Name { get; private set; }
    
    public static readonly Role Admin = new() {Name = "admin"};
    public static readonly Role Engineer = new() {Name = "engineer"};
    public static readonly Role MetroDriver = new() {Name = "metro_driver"};
}