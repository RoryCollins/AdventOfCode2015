namespace Day08;

using System.Text.RegularExpressions;
using Shared;

public class Solution : Solver
{
    private readonly IEnumerable<string> input;

    public Solution(IEnumerable<string> input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        return input.Sum(line => line.Length-CharacterCount(line));
    }

    public object PartTwo()
    {
        var regex = new Regex(@"(\\|"")");
        return input.Sum(line => 2 + regex.Matches(line).Count);
    }

    private static int CharacterCount(string arg) => Regex.Match(arg, @"^""(\\x..|\\.|.)*""$").Groups[1].Captures.Count;
}