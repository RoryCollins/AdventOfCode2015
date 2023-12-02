namespace Day17;

public class Solution
{
    private readonly List<int> buckets;
    private readonly List<List<int>> combinations;

    public Solution(IEnumerable<string> buckets)
    {
        this.buckets = buckets.Select(int.Parse)
            .ToList();
        combinations = GetCombinations();
    }

    public object PartOne()
    {
        return combinations.Count;
    }

    public object PartTwo()
    {
        var minimumContainers = combinations.Min(it => it.Count);
        return combinations.Count(it => it.Count == minimumContainers);
    }

    private List<List<int>> GetCombinations()
    {
        var matrix = new List<List<int>>[buckets.Count][];

        matrix[0] = new List<List<int>>[151];
        for (var j = 0; j <= 150; j++)
        {
            matrix[0][j] = new List<List<int>>();
        }
        matrix[0][buckets[0]].Add(new List<int> { buckets[0] });

        for (var i = 1; i < buckets.Count; i++)
        {
            matrix[i] = new List<List<int>>[151];

            for (var j = 0; j <= 150; j++)
            {
                matrix[i][j] = new List<List<int>>(matrix[i - 1][j]);
                var remainder = j - buckets[i];
                switch (remainder)
                {
                    case 0:
                        matrix[i][j]
                            .Add(new List<int>() { buckets[i] });
                        break;
                    case > 0:
                    {
                        foreach (var bs in matrix[i - 1][remainder])
                        {
                            matrix[i][j]
                                .Add(bs.Concat(new[] { buckets[i] })
                                    .ToList());
                        }

                        break;
                    }
                }
            }
        }

        return matrix[^1][^1];
    }
}