namespace Day14;

using System.Text.RegularExpressions;

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