namespace Day05;

using Shared;

public class Solution
{
    private readonly IEnumerable<string> input;

    public Solution(IEnumerable<string> input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        return input.Count(x => ContainsAtLeastThreeVowels(x) &&
                                ContainsDoubleLetter(x) &&
                                !ContainsBannedPart(x));
    }

    public object PartTwo()
    {

        return input.Count(x => ContainsDoublePair(x) &&
                                ContainsMirrorTriple(x));
    }

    private bool ContainsMirrorTriple(string s)
    {
        return s
            .Windowed(3)
            .Any(part => part.First() == part.Last());
    }

    private bool ContainsDoublePair(string s)
    {
        for (int i = 0; i < s.Length - 1; i++)
        {
            var part = s.Substring(i, 2);
            var remainder = s.Substring(i + 2);
            if (remainder.Contains(part)) return true;
        }

        return false;
    }

    private bool ContainsBannedPart(string s)
    {
        var bannedParts = new[] { "ab", "cd", "pq", "xy" };
        return bannedParts.Any(s.Contains);
    }

    private bool ContainsDoubleLetter(string s)
    {
        return s
            .Windowed(2)
            .Any(part => part.First() == part.Last());
    }

    private bool ContainsAtLeastThreeVowels(string s)
    {
        var vowels = new[]{'a', 'e', 'i', 'o', 'u'};
        return s.Count(vowels.Contains) >= 3;
    }
}