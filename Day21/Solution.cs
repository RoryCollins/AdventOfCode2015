namespace Day21;

using System.Text.RegularExpressions;
using Shared;

public record Item(string Name, int Cost, int Damage, int Armour)
{
    public static Item NoItem => new Item("None", 0, 0, 0);
};

public class Solution
{
    private readonly Fighter boss;
    private readonly IEnumerable<Item> weapons;
    private readonly IEnumerable<Item> armours;
    private readonly IEnumerable<Item> rings;

    public Solution(IEnumerable<string> input)
    {
        var stats = input
            .Select(it => it.Split(":", StringSplitOptions.TrimEntries))
            .Select(it => int.Parse(it[1]))
            .ToList();

        boss = new Fighter(stats[0], stats[1], stats[2]);

        var bazaar = File.ReadAllText("./bazaar.txt")
            .Split("\r\n\r\n")
            .Select(it => it.Split("\r\n"))
            .Select(it => it.Skip(1))
            .Select(shop => shop
                .Select(it => Regex.Match(it, @"(?<name>[+\w\s\d]*)(?<cost>\s+\d+)(?<damage>\s+\d+)(?<armour>\s+\d+)")
                    .Groups)
                .Select(i => new Item(i["name"]
                    .Value.Trim(), int.Parse(i["cost"].Value), int.Parse(i["damage"].Value), int.Parse(i["armour"].Value))))
            .ToList();
        weapons = bazaar[0];
        armours = bazaar[1];
        rings = bazaar[2];
    }

    public object PartOne()
    {
        Fighter player;
        var minimumCost = int.MaxValue;
        var possibleArmours = armours.Concat(new[] { Item.NoItem })
            .ToList();
        var possibleRings = rings
            .ChooseTwo()
            .Concat(rings.Select(r => (Item.NoItem, r)))
            .Concat(new[] { (Item.NoItem, Item.NoItem) })
            .ToList();

        foreach (var weapon in weapons)
        {
            foreach (var armour in possibleArmours)
            {
                foreach (var ringPair in possibleRings)
                {
                    var totalCost = weapon.Cost + armour.Cost + ringPair.Item1.Cost + ringPair.Item2.Cost;
                    var totalDamage = weapon.Damage + ringPair.Item1.Damage + ringPair.Item2.Damage;
                    var totalArmour = armour.Armour + ringPair.Item1.Armour + ringPair.Item2.Armour;
                    if (totalCost > minimumCost) continue;
                    player = new Fighter(100, totalDamage, totalArmour);
                    if (Fight(player, boss))
                    {
                        minimumCost = totalCost;
                    }
                }
            }
        }

        return minimumCost;
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    private static bool Fight(Fighter player, Fighter boss)
    {
        int damageReceived = Math.Max((boss.Damage - player.Armour), 1);
        int damageDealt = Math.Max((player.Damage - boss.Armour), 1);
        return player.Hp / damageReceived >= boss.Hp / damageDealt;
    }
}

internal class Fighter
{
    public int Hp { get; }
    public int Damage { get; }
    public int Armour { get; }

    public Fighter(int hp, int damage, int armour)
    {
        Hp = hp;
        Damage = damage;
        Armour = armour;
    }
}