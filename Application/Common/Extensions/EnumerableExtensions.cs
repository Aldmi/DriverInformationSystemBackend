namespace Application.Common.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> FindDublicate<T>(this IEnumerable<T> array)
    {
        var set = new HashSet<T>();
        var dublicates = array.Where(item => !set.Add(item)).ToList();
        return dublicates;
    }
}