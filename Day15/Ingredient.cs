namespace Day15;

using System.Text.RegularExpressions;

partial class Ingredient
{
    public string Name { get; }
    public int Capacity { get; }
    public int Durability { get; }
    public int Flavour { get; }
    public int Texture { get; }
    public int Calories { get; }

    public IngredientAmount Times(int amount) => new IngredientAmount(
        Capacity*amount,
        Durability*amount,
        Flavour*amount,
        Texture*amount,
        Calories*amount);

    private Ingredient(string name, int capacity, int durability, int flavour, int texture, int calories)
    {
        Name = name;
        Capacity = capacity;
        Durability = durability;
        Flavour = flavour;
        Texture = texture;
        Calories = calories;
    }

    public static Ingredient From(string description)
    {
        var groups = MyRegex()
            .Match(description)
            .Groups;

        return new Ingredient(groups["name"].Value,
            int.Parse(groups["capacity"].Value),
            int.Parse(groups["durability"].Value),
            int.Parse(groups["flavour"].Value),
            int.Parse(groups["texture"].Value),
            int.Parse(groups["calories"].Value));
    }

    [GeneratedRegex(@"(?<name>\w+): capacity (?<capacity>-?\d+), durability (?<durability>-?\d+), flavor (?<flavour>-?\d+), texture (?<texture>-?\d+), calories (?<calories>-?\d+)")]
    private static partial Regex MyRegex();
}

public record IngredientAmount(int Capacity, int Durability, int Flavour, int Texture, int Calories)
{
    public static IngredientAmount Empty = new IngredientAmount(0,0,0,0,0);

    public static IngredientAmount operator +(IngredientAmount a, IngredientAmount b) =>
        new IngredientAmount(a.Capacity + b.Capacity,
            a.Durability + b.Durability,
            a.Flavour + b.Flavour,
            a.Texture + b.Texture,
            a.Calories + b.Calories);

    public int Score => new[] { Capacity, Durability, Flavour, Texture }.Any(i => i <= 0)
        ? 0
        : Capacity * Durability * Flavour * Texture;
};