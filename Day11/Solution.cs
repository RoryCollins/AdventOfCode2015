namespace Day11;

public class Solution
{
    private readonly string currentPassword;
    private string secondPassword;

    public Solution(string currentPassword)
    {
        this.currentPassword = currentPassword;
    }

    public object PartOne()
    {
        // secondPassword = GetNextPassword(currentPassword);

        return secondPassword;
    }


    public object PartTwo()
    {
        return new DoubleLetterRequirement().IncrementToValidPassword("dezbb"); //should be dzzbb
        // var thirdPassword = GetNextPassword(secondPassword);

        // return thirdPassword;
    }

    private string GetNextPassword(string password)
    {
        var nextPassword = IncrementPassword(password);
        var requirements = new Requirement[]
        {
            new DoubleLetterRequirement(),
            new StraightLetterRequirement(),
            new BannedLetterRequirement(),
        };
        bool changed;
        do
        {
            changed = false;
            foreach (var requirement in requirements)
            {
                if (!requirement.IsValid(nextPassword))
                {
                    nextPassword = requirement.IncrementToValidPassword(nextPassword);
                    changed = true;
                    break;
                }
            }
        } while (changed);

        return nextPassword;
    }

    private string IncrementPassword(string password)
    {
        var nextPassword = password.ToArray();
        for (var i = nextPassword.Length - 1; i >= 0; i--)
        {
            if (nextPassword[i] == 'z')
            {
                nextPassword[i] = 'a';
                continue;
            }

            nextPassword[i] = (char)(nextPassword[i] + 1);
            break;
        }

        return string.Join("", nextPassword);
    }
}