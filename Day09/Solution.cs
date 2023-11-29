namespace Day09;

using Shared;

public class Solution
{
    private readonly Dictionary<string, Dictionary<string, int>> graph = new();
    private readonly HashSet<string> locations = new();

    public Solution(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var deconstructed = line.Split(new[] { "to", "=" }, StringSplitOptions.TrimEntries);
            var node1 = deconstructed[0];
            var node2 = deconstructed[1];
            var distance = int.Parse(deconstructed[2]);
            locations.Add(node1);
            locations.Add(node2);
            graph.TryAdd(node1, new());
            graph[node1].Add(node2, distance);
            graph.TryAdd(node2, new());
            graph[node2].Add(node1, distance);
        }
    }

    public object PartOne()
    {
        return GetBestRoute((x, y) => new[] { x, y }.Min());
    }

    public object PartTwo()
    {
        return GetBestRoute((x, y) => new[] { x, y }.Max());
    }

    private int GetBestRoute(Func<int, int, int> comparison)
    {
        var routes = locations.Permute();
        var currentBest = 0;
        foreach (var route in routes)
        {
            var current = 0;
            for (var i = 0; i < route.Length - 1; i++)
            {
                current += graph[route[i]][route[i + 1]];
            }

            currentBest = currentBest == 0 ? current : currentBest;
            currentBest = comparison(current, currentBest);
        }

        return currentBest;
    }
}