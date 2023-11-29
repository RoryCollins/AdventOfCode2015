namespace Day02;

using System.Runtime.CompilerServices;

public class Solution
{
    private readonly IEnumerable<Parcel> parcels;

    public Solution(IEnumerable<string> input)
    {
        this.parcels = input.Select(line =>
        {
            var def = line.Split("x").Select(int.Parse).ToArray();
            return new Parcel(def[0], def[1], def[2]);
        });
    }

    public object PartOne()
    {
        return parcels.Sum(p => p.WrappingPaperRequired);
    }

    public object PartTwo()
    {
        return parcels.Sum(p => p.RibbonRequired);
    }
}

public record Parcel(int Height, int Length, int Width)
{
    private int Slack => new List<int>{Height * Length, Length * Width, Height * Width}.Min();
    public int WrappingPaperRequired => (2 * Height * Length) + (2 * Length * Width) + (2 * Height * Width) + Slack;

    public int RibbonRequired => new List<int> { Height, Length, Width }.Order()
        .Take(2)
        .Sum(x => x * 2) + Height*Length*Width;
};