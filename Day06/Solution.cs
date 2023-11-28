namespace Day06;

using System.Text.RegularExpressions;
using Shared;

public partial class Solution : Solver
{
    private readonly IEnumerable<Command> input;
    private readonly HashSet<Coordinate2D> activeLights = new();
    private readonly Dictionary<Coordinate2D, int> activeLightsWithBrightness = new();
    public Solution(IEnumerable<string> input)
    {
        this.input = input.Select(CreateCommand);
    }

    private static Command CreateCommand(string line)
    {
        var match = MyRegex().Match(line);
        var groups = match.Groups;

        var start = new Coordinate2D(int.Parse(groups["startX"].Value), int.Parse(groups["startY"].Value));
        var stop = new Coordinate2D(int.Parse(groups["stopX"].Value), int.Parse(groups["stopY"].Value));

        return groups["command"]
                .Value.Trim() switch
            {
                "turn on" => new TurnOnCommand(start, stop),
                "turn off" => new TurnOffCommand(start, stop),
                "toggle" => new ToggleCommand(start, stop),
                _ => throw new ArgumentOutOfRangeException()
            };
    }

    public object PartOne()
    {
        foreach (var command in input)
        {
            command.ExecutePartOne(activeLights);
        }
        return activeLights.Count;
    }

    public object PartTwo()
    {
        foreach (var command in input)
        {
            command.ExecutePartTwo(activeLightsWithBrightness);
        }
        return activeLightsWithBrightness.Sum(kvp => kvp.Value);
    }

    [GeneratedRegex("^(?<command>\\D+)(?<startX>\\d+),(?<startY>\\d+) through (?<stopX>\\d+),(?<stopY>\\d+)$")]
    private static partial Regex MyRegex();
}