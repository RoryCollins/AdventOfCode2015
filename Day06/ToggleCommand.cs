namespace Day06;

using Shared;

internal class ToggleCommand : Command
{
    public ToggleCommand(Coordinate2D start, Coordinate2D stop) : base(start, stop)
    {
    }

    internal override void ExecutePartOne(HashSet<Coordinate2D> coordinates)
    {
        for (int x = Start.X; x <= Stop.X; x++)
        {
            for (int y = Start.Y; y <= Stop.Y; y++)
            {
                var coordinate = new Coordinate2D(x, y);
                if (coordinates.Contains(coordinate)) coordinates.Remove(coordinate);
                else coordinates.Add(coordinate);
            }
        }
    }

    internal override void ExecutePartTwo(Dictionary<Coordinate2D, int> coordinates)
    {
        for (int x = Start.X; x <= Stop.X; x++)
        {
            for (int y = Start.Y; y <= Stop.Y; y++)
            {
                var coordinate = new Coordinate2D(x, y);
                coordinates.TryGetValue(coordinate, out var value);
                coordinates[coordinate] = value + 2;
            }
        }
    }
}