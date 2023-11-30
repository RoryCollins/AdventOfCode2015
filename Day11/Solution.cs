namespace Day11;

using Shared;

public class Solution
{
    private char[] password;
    private static readonly char[] BannedLetters = { 'i', 'o', 'l' };
    private static readonly IEnumerable<char[]> ThreeLetterStraights = "abcdefghijklmnopqrstuvwxyz".Windowed(3);

    public Solution(string password)
    {
        this.password = password.ToArray();
    }

    public object PartOne()
    {
        return FindNextPassword();
    }

    public object PartTwo()
   {
        return FindNextPassword();
    }

    private string FindNextPassword()
    {
        do
        {
            Increment(ref this.password);
        } while (!IsValid(password));

        return string.Join("", password);
    }

    private static bool BannedLetterCheck(char c) => !(BannedLetters.Contains(c));
    private static bool StraightLetterCheck(char[] cs) => ThreeLetterStraights.Contains(cs);

    private static bool IsValid(char[] password) {
        var straightLetterCheck = false;
        var doubleLetterCheck = false;
        for (int j = 2; j < password.Length; j++) {
            if (!BannedLetterCheck(password[j])) return false;
            if (StraightLetterCheck(password[(j-2)..j])) {
                straightLetterCheck = true;
            }
            if (j <= 2) continue;
            for (var k = 0; k + 2 < j; k++) {
                if (password[j - 3 - k] == password[j - 2 - k]
                    && password[j - 1] == password[j]
                    && password[j - 2 - k] != password[j - 1]) {
                    doubleLetterCheck = true;
                }
            }
        }
        return straightLetterCheck && doubleLetterCheck;
    }

    private static void Increment(ref char[] password, int at = -1) {
        if (at == -1) {
            at = password.Length - 1;
        }
        password[at]++;
        if (password[at] == 'i' || password[at] == 'o' || password[at] == 'l') password[at]++;
        if (password[at] <= 'z') return;
        password[at] = 'a';
        Increment(ref password, at - 1);
    }

}