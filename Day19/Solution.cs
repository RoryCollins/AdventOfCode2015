namespace Day19;

using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Solution
{
    private readonly Dictionary<string, List<string>> lookup = new();
    private readonly Dictionary<string, string> reverseLookup = new();
    private static string text;

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
            reverseLookup.Add(entry[1], entry[0]);
        }

        text = input[^1]!;
    }

    public object PartOne()
    {
        return AvailableTranscodes(text).Count;
    }

    public object PartTwo()
    {
        return Resolve("ORnPBPMgArCaCaCa").ToString();
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
                if (remainder.StartsWith(entry, StringComparison.Ordinal))
                {
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
        }

        return molecules;
    }

    private string Resolve(string s)
    {
        var foo = s.LastIndexOf("Ar", StringComparison.Ordinal);
        if (foo == -1) return s;

        var bar = FindLastUnclosed("Rn", "Ar", s[..foo]);
        var compound = s[(bar+2)..foo];
        var internalElements = compound.Split('Y');


        return s.Substring(0, bar+2) + s.Substring(foo);
    }

    private (string, int) Reduce(string s, int count)
    {
        if (!reverseLookup.Keys.Any(s.Contains)) return (s, count);
        foreach (var entry in reverseLookup.Where(e => s.Contains(e.Key)))
        {

        }
    }

    private static int FindLastUnclosed(string open, string closure, string s)
        {
            var lastOpen = s.LastIndexOf(open, StringComparison.Ordinal);
            var lastClose = s.LastIndexOf(closure, StringComparison.Ordinal);
            return lastOpen > lastClose || (lastOpen == -1)
                ? lastOpen
                : FindLastUnclosed(open, closure, s[..lastOpen]);
        }
}