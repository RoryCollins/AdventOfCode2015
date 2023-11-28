namespace Day01;

public class Solution
{
    private readonly string chars = File
        .ReadAllText("input.txt");

    public string PartOne()
    {
        return Levels(chars).Last().level.ToString();
    }

    public string PartTwo()
    {
        return Levels(chars)
            .First(i => i.level == -1)
            .idx.ToString();

    }

    private static IEnumerable<(int idx, int level)> Levels(string input)
    {
        var idx = 0;
        var level = 0;
        foreach (var c in input)
        {
            yield return (idx++, level += c == '(' ? 1 : -1);
        }
    }
}