namespace Day19;

using System.Text.RegularExpressions;

public class Solution
{
    private readonly Dictionary<string, List<string>> lookup = new();
    private static string text = string.Empty;

    public Solution(List<string> input)
    {
        var inputDictionary = input
            .TakeWhile(line => line.Length != 0)
            .Select(line => line.Split("=>", StringSplitOptions.TrimEntries));

        foreach (var entry in inputDictionary)
        {
            var list = lookup.TryGetValue(entry[0], out var v)
                ? v
                : new List<string>();
            lookup[entry[0]] = list.Append(entry[1]).ToList();
        }

        text = input[^1];
    }

    public object PartOne()
    {
        return AvailableTranscodes(text)
            .Count;
    }

    public object PartTwo()
    {
        var elementCount = Regex.Count(text, "[A-Z][a-z]?");
        var rnArCount = Regex.Count(text, "Rn|Ar");
        var yCount = Regex.Count(text, "Y");

        return $"{elementCount} - {rnArCount} - 2*{yCount} - 1 = {elementCount-rnArCount-(2*yCount)-1}";
    }

    private HashSet<string> AvailableTranscodes(string current)
    {
        var molecules = new HashSet<string>();
        for (var i = 0; i < current.Length; i++)
        {
            var prefix = current[..i];
            var remainder = current[i..];
            foreach (var (entry, replacements) in lookup)
            {
                if (!remainder.StartsWith(entry, StringComparison.Ordinal)) continue;
                foreach (var it in replacements)
                {
                    var r = entry.Length > remainder.Length
                        ? ""
                        : remainder[entry.Length..];
                    var str = prefix + it + r;
                    molecules.Add(str);
                }
            }
        }

        return molecules;
    }
}