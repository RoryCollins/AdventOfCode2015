namespace Day10;

using System.Text;

public class Solution
{
    private readonly string input;

    public Solution(string input)
    {
        this.input = input;
    }

    public object PartOne()
    {
        var foo = Enumerable.Range(1, 40)
            .Aggregate(input, (s, _) => GetDiscription(s));
        return foo.Length;
    }

    public object PartTwo()
    {
        var foo = Enumerable.Range(1, 50)
            .Aggregate(input, (s, _) => GetDiscription(s));
        return foo.Length;
    }

    private string GetDiscription(string s)
    {
        var counter = 1;
        var sb = new StringBuilder();
        for (var i = 1; i < s.Length; i++)
        {
            if (s[i] != s[i - 1])
            {
                sb.Append($"{counter}{s[i - 1]}");
                counter = 1;
                continue;
            }

            counter++;
        }

        sb.Append($"{counter}{s.Last()}");

        return sb.ToString();
    }
}