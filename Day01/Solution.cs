namespace Day01;

using Shared;

public class Solution : Solver
{
    private readonly string chars = File
        .ReadAllText("input.txt");

    public object PartOne()
    {
        return Levels(chars).Last().level;
    }

    public object PartTwo()
    {
        return Levels(chars)
            .First(i => i.level == -1)
            .index + 1;

    }

    private static IEnumerable<(int index, int level)> Levels(string input)
    {
        var index = 0;
        var level = 0;
        foreach (var c in input)
        {
            yield return (index++, level += c == '(' ? 1 : -1);
        }
    }
}