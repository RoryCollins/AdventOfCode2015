namespace Day06;

using Shared;

internal class TurnOffCommand : Command
{
    internal TurnOffCommand(Coordinate2D start, Coordinate2D stop) : base(start, stop)
    {
    }

    internal override void ExecutePartOne(HashSet<Coordinate2D> coordinates)
    {
        for (int x = Start.X; x <= Stop.X; x++)
        {
            for (int y = Start.Y; y <= Stop.Y; y++)
            {
                coordinates.Remove(new Coordinate2D(x, y));
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
                if (coordinates.TryGetValue(coordinate, out var value) && value > 0)
                {
                    coordinates[coordinate] = value - 1;
                }
            }
        }
    }
}