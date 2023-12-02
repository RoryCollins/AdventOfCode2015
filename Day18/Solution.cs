namespace Day18;

using Shared;

public class Solution
{
    private readonly Dictionary<Coordinate2D, bool> lights = new();
    private static readonly int[] TwoOrThree = { 2, 3 };
    private bool cornersStuckOn = false;
    private const int XSize = 100;
    private const int YSize = 100;
    private const int Steps = 100;

    public Solution(List<string> input)
    {
        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                lights.Add(new Coordinate2D(x, y), input[x][y] == '#');
            }
        }
    }

    public Dictionary<Coordinate2D, bool> Next(Dictionary<Coordinate2D, bool> current)
    {
        var next = new Dictionary<Coordinate2D, bool>();
        foreach (var light in current)
        {
            var litNeighbours = GetNeighbours(light.Key)
                .Count(it => current[it]);
            if (current[light.Key] && TwoOrThree.Contains(litNeighbours))
            {
                next.Add(light.Key, true);
                continue;
            }

            if (!current[light.Key] && litNeighbours == 3)
            {
                next.Add(light.Key, true);
                continue;
            }

            next.Add(light.Key, false);
        }

        if (cornersStuckOn)
        {
            TurnOnCorners(next);
        }

        return next;
    }

    private static void TurnOnCorners(Dictionary<Coordinate2D, bool> next)
    {
        var corners = new[]
        {
            new Coordinate2D(0, 0),
            new Coordinate2D(XSize - 1, 0),
            new Coordinate2D(XSize - 1, YSize - 1),
            new Coordinate2D(0, YSize - 1),
        };
        foreach (var corner in corners)
        {
            next[corner] = true;
        }
    }

    private static IEnumerable<Coordinate2D> GetNeighbours(Coordinate2D centre)
    {
        var directions = new[]
        {
            new Coordinate2D(-1, 0),
            new Coordinate2D(-1, 1),
            new Coordinate2D(0, 1),
            new Coordinate2D(1, 1),
            new Coordinate2D(1, 0),
            new Coordinate2D(1, -1),
            new Coordinate2D(0, -1),
            new Coordinate2D(-1, -1),
        };
        return directions.Select(centre.Add)
            .Where(it => it is { X: >= 0 and < XSize, Y: >= 0 and < YSize });
    }

    public object PartOne()
    {
        return Enumerable.Range(1, Steps)
            .Aggregate(lights, (x, _) => Next(x))
            .Count(it => it.Value);
    }

    public object PartTwo()
    {
        cornersStuckOn = true;
        TurnOnCorners(lights);
        return PartOne();
    }
}