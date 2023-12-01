namespace Day14;

using System.Text.RegularExpressions;

public class Solution
{

    private IEnumerable<Reindeer> reindeers;
    public Solution(IEnumerable<string> input)
    {
        reindeers = input.Select(Reindeer.From);
    }

    public object PartOne()
    {
        return reindeers.Max(it => it.DistanceAfter(2503));
    }

    public object PartTwo()
    {
        var scores = reindeers.ToDictionary(reindeer => reindeer.Name, _ => 0);
        for (int i = 1; i <= 2503; i++)
        {
            List<(string name, int distance)> reindeerDistances = reindeers.Select(r => (r.Name, r.DistanceAfter(i))).ToList();
            var maxDistance = reindeerDistances.Max(it => it.distance);
            var leaders = reindeerDistances.Where(it => it.distance == maxDistance);
            foreach (var (leader, _) in leaders)
            {
                scores[leader]++;
            }
        }

        return scores.Max(it => it.Value);
    }
}

partial class Reindeer
{
    public string Name { get; }
    public int Speed { get; }
    public int ActivePeriod { get; }
    public int RestPeriod { get; }

    public int DistanceAfter(int seconds)
    {
        var loopTime = (ActivePeriod + RestPeriod);
        var completeLoops = seconds / loopTime;
        var leftover = seconds % loopTime;
        return Speed * ((completeLoops * ActivePeriod) + new[] { leftover, ActivePeriod }.Min());
    }

    private Reindeer(string name, int speed, int activePeriod, int restPeriod)
    {
        Name = name;
        Speed = speed;
        ActivePeriod = activePeriod;
        RestPeriod = restPeriod;
    }

    public static Reindeer From(string description)
    {
        var groups = MyRegex()
            .Match(description)
            .Groups;

        return new Reindeer(groups["name"].Value,
            int.Parse(groups["speed"].Value),
            int.Parse(groups["activePeriod"].Value),
            int.Parse(groups["restPeriod"].Value));
    }

    [GeneratedRegex(@"(?<name>\w+) can fly (?<speed>\d+) km/s for (?<activePeriod>\d+) seconds, but then must rest for (?<restPeriod>\d+) seconds.")]
    private static partial Regex MyRegex();
}