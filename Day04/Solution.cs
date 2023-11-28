namespace Day04;

using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using Shared;

public class Solution : Solver
{
    private readonly string secretKey;

    public Solution(string secretKey)
    {
        this.secretKey = secretKey;
    }

    public object PartOne()
    {
        return FindSuffix("00000");
    }

    public object PartTwo()
    {
        return FindSuffix("000000");
    }

    private object FindSuffix(string prefix)
    {
        var q = new ConcurrentQueue<int>();
        Parallel.ForEach(
            Numbers(),
            MD5.Create,
            (i, state, md5) =>
            {
                var hashBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(secretKey + i));
                var hash = string.Join("", hashBytes.Select(b => b.ToString("x2")));
                if (hash.StartsWith(prefix))
                {
                    q.Enqueue(i);
                    state.Stop();
                }

                return md5;
            },
            _ => { });
        return q.Min();
    }

    IEnumerable<int> Numbers() {
        for (int i=0; ;i++) {
            yield return i;
        }
    }

}