namespace Day06;

using Shared;

internal abstract class Command
{
    protected readonly Coordinate2D Start;
    protected readonly Coordinate2D Stop;

    protected Command(Coordinate2D start, Coordinate2D stop)
    {
        this.Start = start;
        this.Stop = stop;
    }

    internal abstract void ExecutePartOne(HashSet<Coordinate2D> coordinates);

    internal abstract void ExecutePartTwo(Dictionary<Coordinate2D, int> coordinates);
}