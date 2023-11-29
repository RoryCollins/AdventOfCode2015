namespace Day11;

using Shared;

public class StraightLetterRequirement : Requirement
{
    private char increment(char c)
    {
        return c == 'z' ? 'a' : (char)(c + 1);
    }
    public bool IsValid(string password)
    {
        var straights = "abcdefghijklmnopqrstuvwxyz".Windowed(3);
        return straights.Any(s => password.Contains(string.Join("", s)));
    }

    public string IncrementToValidPassword(string password)
    {
        var nextPassword = password.ToArray();
        for (var i = nextPassword.Length - 1; i >= 0; i--)
        {
            if (nextPassword[i - 1] <= increment(nextPassword[i-2])
                && nextPassword[i] <= increment(nextPassword[i-1]))
            {
                nextPassword[i - 1] = increment(nextPassword[i-2]);
                nextPassword[i] = increment(nextPassword[i-1]);
                break;
            }

            if (nextPassword[i - 2] < 'x')
            {
                nextPassword[i - 2] = increment(nextPassword[i-2]);
                nextPassword[i - 1] = increment(nextPassword[i-2]);
                nextPassword[i] = increment(nextPassword[i-1]);
                break;
            }
        }

        return string.Join("", nextPassword);
    }
}