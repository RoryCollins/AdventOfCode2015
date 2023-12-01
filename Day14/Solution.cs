namespace Day14;

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