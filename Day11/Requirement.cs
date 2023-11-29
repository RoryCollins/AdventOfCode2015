namespace Day11;

public interface Requirement
{
    bool IsValid(string password);
    string IncrementToValidPassword(string password);
}