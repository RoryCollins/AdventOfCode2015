namespace Day11;

public class BannedLetterRequirement : Requirement
{
    private static readonly char[] BannedLetters = new[] { 'i', 'o', 'l' };

    public bool IsValid(string password)
    {
        return !BannedLetters.Any(password.Contains);
    }

    public string IncrementToValidPassword(string password)
    {
        var index = password.IndexOfAny(BannedLetters);

        var nextPassword = password.ToArray();
        nextPassword[index] = (char)(nextPassword[index] + 1);
        for (int i = index + 1; i < password.Length; i++)
        {
            nextPassword[i] = 'a';
        }

        return string.Join("", nextPassword);
    }
}