namespace Day16;

using System.Text.RegularExpressions;

public class Solution
{
    private readonly IEnumerable<string> input;
    private Dictionary<string, int> reference;

    public Solution(IEnumerable<string> input)
    {
        this.input = input;
        reference = new Dictionary<string, int>
        {
            { "children", 3 },
            { "cats", 7 },
            { "samoyeds", 2 },
            { "pomeranians", 3 },
            { "akitas", 0 },
            { "vizslas", 0 },
            { "goldfish", 5 },
            { "trees", 3 },
            { "cars", 2 },
            { "perfumes", 1 },
        };
    }

    public object PartOne()
    {
        return input.Select(ProcessSue)
            .Max();
    }

    public object PartTwo()
    {
        return "[part one modified for part two]";
    }

    private int ProcessSue(string sue)
    {
        var sueId = int.Parse(new Regex(@"Sue (\d+)").Match(sue).Groups[1].Value);
        var keyMatches = new Regex(@"(?<key>\w+): (?<number>\d+)").Matches(sue);
        var correct = keyMatches.All(m =>
        {
            var key = m.Groups["key"].Value;
            var value = m.Groups["number"].Value;
            return key switch
            {
                "cats" or "trees" => reference[key] < int.Parse(value),
                "pomeranians" or "goldfish" => reference[key] > int.Parse(value),
                _ => reference[key] == int.Parse(value),
            };
            return reference[key] == int.Parse(value);
        });
        return correct ? sueId : -1;
    }
}