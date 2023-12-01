namespace Day13;

using System.Text.RegularExpressions;
using Shared;

public class Solution
{
    private Dictionary<string, Dictionary<string, int>> dict = new();

    public Solution(IEnumerable<string> input)
    {
        foreach (var (source, target, change) in input.Select(Parse))
        {
            dict.TryAdd(source, new Dictionary<string, int>());
            dict[source].Add(target, change);
        }

    }

    public object PartOne()
    {
        var ps = dict.Keys.ToList().Permute();
        return ps.Max(GetHappiness);
    }

    public object PartTwo()
    {
        var people = dict.Keys.ToList();
        const string myName = "Me";
        dict.TryAdd(myName, new Dictionary<string, int>());

        foreach (var p in people)
        {
            dict[p].Add(myName, 0);
            dict[myName].Add(p, 0);
        }
        var ps = dict.Keys.ToList().Permute();
        return ps.Max(GetHappiness);
    }


    private int GetHappiness(string[] ps)
    {
        int sum = 0;
        for(int i = 0; i<=ps.Length-1; i++)
        {
            var me = dict[ps[i]];
            if (i == 0)
            {
                sum += me[ps[^1]] + me[ps[1]];
                continue;
            }

            if (i == ps.Length - 1)
            {
                sum += me[ps[i - 1]] + me[ps[0]];
                continue;
            }

            sum += me[ps[i - 1]] + me[ps[i + 1]];

        }

        return sum;
    }

    private (string source, string target, int change) Parse(string s)
    {
        var groups = new Regex(@"(?<source>\w+) would (?<impact>gain|lose) (?<amount>\d+) happiness units by sitting next to (?<target>\w+).")
            .Match(s)
            .Groups;

        var change = int.Parse(groups["amount"].Value) * ((groups["impact"].Value == "gain") ? 1 : -1);
        return (groups["source"].Value, groups["target"].Value, change);
    }
}