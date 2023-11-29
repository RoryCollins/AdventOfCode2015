namespace Shared;

public static class ListExtensions
{
    public static IEnumerable<T[]> Permute<T>(this IEnumerable<T> source)
    {
        return Permute(new List<T>(), source.ToArray());
    }
    private static IEnumerable<T[]> Permute<T>(IEnumerable<T> prefix, T[] remainder)
    {
        if (!remainder.Any()) return new[]{prefix.ToArray()};
        return remainder.SelectMany((s, i) => Permute(
            prefix.Append(s),
            remainder.Take(i).Concat(remainder.Skip(i + 1)).ToArray()));
    }
}