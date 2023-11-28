namespace Day07;

using System.Text.RegularExpressions;
using Shared;

public class Solution : Solver
{
    private readonly Dictionary<string, string> connections = new();
    private Dictionary<string, ushort> signals = new();

    public Solution(IEnumerable<string> input)
    {
        var regex = new Regex(@"(?<command>.*) -> (?<node>.*)");
        foreach (var line in input)
        {
            var match = regex.Match(line);
            connections[match.Groups["node"].Value] = match.Groups["command"].Value;
        }
    }

    public object PartOne()
    {
        foreach (var con in connections)
        {
            signals[con.Key] = GetValue(con.Key);
        }

        return signals["a"];
    }

    public object PartTwo()
    {
        signals = new()
        {
            ["b"] = 16076
        };
        foreach (var con in connections)
        {
            signals[con.Key] = GetValue(con.Key);
        }

        return signals["a"];
    }

    private ushort GetValue(string node)
    {
        if (signals.TryGetValue(node, out var value)) return value;
        signals[node] = ProcessCommand(connections[node]);
        return signals[node];
    }

    private ushort ProcessCommand(string command)
    {
        var andRegex = new Regex(@"(?<node1>\w+) AND (?<node2>\w+)");
        var andMatch = andRegex.Match(command);
        if (andMatch.Success)
        {
            var first = ushort.TryParse(andMatch.Groups["node1"].Value, out var val1) ? val1 : GetValue(andMatch.Groups["node1"].Value);
            var second = ushort.TryParse(andMatch.Groups["node2"].Value, out var val2) ? val2 : GetValue(andMatch.Groups["node2"].Value);
            return (ushort)(first & second);
        }

        var orRegex = new Regex(@"(?<node1>\w+) OR (?<node2>\w+)");
        var orMatch = orRegex.Match(command);
        if (orMatch.Success)
        {
            return (ushort)(GetValue(orMatch.Groups["node1"].Value) | GetValue(orMatch.Groups["node2"].Value));
        }

        var notRegex = new Regex(@"NOT (?<node>\w+)");
        var notMatch = notRegex.Match(command);
        if (notMatch.Success)
        {
            return (ushort)(~GetValue(notMatch.Groups["node"].Value));
        }

        var rShiftRegex = new Regex(@"(?<node>\w+) RSHIFT (?<bits>\d+)");
        var rShiftMatch = rShiftRegex.Match(command);
        if (rShiftMatch.Success)
        {
            return (ushort)(GetValue(rShiftMatch.Groups["node"].Value) >> int.Parse(rShiftMatch.Groups["bits"].Value));
        }

        var lShiftRegex = new Regex(@"(?<node>\w+) LSHIFT (?<bits>\d+)");
        var lShiftMatch = lShiftRegex.Match(command);
        if (lShiftMatch.Success)
        {
            return (ushort)(GetValue(lShiftMatch.Groups["node"].Value) << int.Parse(lShiftMatch.Groups["bits"].Value));
        }

        return ushort.TryParse(command, out var val) ? val : GetValue(command);
    }

    public void CommandFactory(string command)
    {

    }
}