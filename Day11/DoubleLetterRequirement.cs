namespace Day11;

public class DoubleLetterRequirement : Requirement
{
    public bool IsValid(string password)
    {
        var counter = 0;
        for (var i = 1; i < password.Length; i++)
        {
            if (password[i] == password[i - 1])
            {
                counter++;
                i++;
            }
        }

        return counter>=2;
    }

    public string IncrementToValidPassword(string password)
    {
        var nextPassword = password.ToArray();
        for (var i = password.Length-1; i >= 0; i--)
        {
            //something janky here
            if (nextPassword[i] == 'z')
            {
                nextPassword[i] = 'a';
                continue;
            }
            if (nextPassword[i - 1] == nextPassword[i])
            {
                i--;
                continue;
            }
            if (nextPassword[i - 1] > nextPassword[i])
            {
                nextPassword[i] = nextPassword[i - 1];
                break;
            }

            nextPassword[i - 1] = (char)(nextPassword[i - 1] + 1);
            nextPassword[i] = nextPassword[i - 1];
            break;
        }
        return string.Join("", nextPassword);
    }
}