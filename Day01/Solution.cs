namespace Day01;

public class Solution
{
    private readonly List<char> chars = File
        .ReadAllText("input.txt")
        .ToCharArray()
        .ToList();

    public string PartOne()
    {
        var ascensions = chars.Count(x => x == '(');
        var descents = chars.Count(x => x == ')');
        return $"{ascensions - descents}!";
    }

    public string PartTwo()
    {
        var level = 0;
        for (var i = 0; i < chars.Count; i++)
        {
            if (chars[i] == '(')
            {
                level++;
            }
            else
            {
                level--;
            }

            if (level == -1)
            {
                return i+1.ToString();
            }
        }

        return "NOT FOUND";
    }
}