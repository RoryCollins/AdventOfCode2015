namespace Day03;

using Shared;

public class Solution : Solver
{
    private readonly string instructions;

    public Solution(string instructions)
    {
        this.instructions = instructions;
    }

    public object PartOne()
    {
        var coordinate = Coordinate2D.Origin;
        var locations = new Dictionary<Coordinate2D, int> { { coordinate, 1 } };
        foreach (var direction in instructions.Select(ParseInstruction))
        {
            coordinate = coordinate.Add(direction);
            locations.TryGetValue(coordinate, out var times);
            locations[coordinate] = times + 1;
        }

        return locations.Count();
    }

    public object PartTwo()
    {
        var santa = Coordinate2D.Origin;
        var roboSanta = Coordinate2D.Origin;
        var listenerCoordinate = new Dictionary<int, Coordinate2D>
        {
            { 0, santa },
            { 1, roboSanta }
        };

        var santaLocations = new Dictionary<Coordinate2D, int> { { santa, 1 } };
        var roboSantaLocations = new Dictionary<Coordinate2D, int> { { roboSanta, 1 } };
        var allLocations = new Dictionary<int, Dictionary<Coordinate2D, int>>
        {
            { 0, santaLocations },
            { 1, roboSantaLocations },
        };

        for (var i = 0; i < instructions.Length; i++)
        {
            var direction = ParseInstruction(instructions[i]);
            var current = i % 2;
            var coordinate = listenerCoordinate[current];
            coordinate = coordinate.Add(direction);
            listenerCoordinate[current] = coordinate;

            allLocations[current]
                .TryGetValue(coordinate, out var times);
            allLocations[current][coordinate] = times + 1;
        }

        var foo = allLocations[0]
            .Keys.ToList();
        var bar = allLocations[1]
            .Keys.ToList();
        return foo.Concat(bar)
            .ToHashSet()
            .Count;
    }

    private Coordinate2D ParseInstruction(char c) => c switch
    {
        'v' => new Coordinate2D(0, -1),
        '^' => new Coordinate2D(0, 1),
        '>' => new Coordinate2D(1, 0),
        _ => new Coordinate2D(-1, 0),
    };
}