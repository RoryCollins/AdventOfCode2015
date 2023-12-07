namespace Day20;

public class Solution
{
    private readonly int input;

    public Solution(int input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        return SieveIt(input, 10);
    }

    public object PartTwo()
    {
        return SieveIt(input, 11, 50);
    }

    private static int SieveIt(int target, int presentsPerElf, int? elfLimit = null)
    {
        const int max = 1_300_000;

        var houses = new int[max];
        for (var i = 1; i < target/presentsPerElf; i++)
        {
            for (var j = 1; j <= (elfLimit ?? target/i); j++)
            {
                var currentHouse = i * j;
                if(currentHouse >= max) continue;
                houses[currentHouse] += i * presentsPerElf;
            }
        }

        return houses.Select((presentCount, index) => (presentCount, index))
            .First(it => it.presentCount >= target)
            .index;
    }
}