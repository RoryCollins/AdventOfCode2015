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
        var locations = new HashSet<Coordinate2D>{ coordinate };
        foreach (var direction in instructions.Select(ParseInstruction))
        {
            coordinate = coordinate.Add(direction);
            locations.Add(coordinate);
        }

        return locations.Count;
    }

    public object PartTwo()
    {
        var listenerCoordinate = new Dictionary<int, Coordinate2D>
        {
            { 0, Coordinate2D.Origin },
            { 1, Coordinate2D.Origin }
        };

        var santaLocations = new HashSet<Coordinate2D> { Coordinate2D.Origin };
        var roboSantaLocations = new HashSet<Coordinate2D> { Coordinate2D.Origin };
        var allLocations = new Dictionary<int, HashSet<Coordinate2D>>
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

            allLocations[current].Add(coordinate);
        }

        return allLocations[0].Concat(allLocations[1])
            .ToHashSet()
            .Count;
    }

    private static Coordinate2D ParseInstruction(char c) => c switch
    {
        'v' => new Coordinate2D(0, -1),
        '^' => new Coordinate2D(0, 1),
        '>' => new Coordinate2D(1, 0),
        _ => new Coordinate2D(-1, 0),
    };
}