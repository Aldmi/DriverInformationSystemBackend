using CSharpFunctionalExtensions;

namespace Application.ValueObjects;

/// <summary>
/// Звуковое сообщение (один файл)
/// </summary>
public class SoundMessage : ValueObject<SoundMessage>
{
    public string Name { get; }
    public string Url { get; }

    
    public SoundMessage(string name, string url)
    {
        Name = name;
        Url = url;
    }


    /// <summary>
    /// Сравнение по Url
    /// </summary>
    protected override bool EqualsCore(SoundMessage other) =>  Url == other.Url;


    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Url.GetHashCode();
            hashCode = (hashCode * 397);
            return hashCode;
        }
    }
}