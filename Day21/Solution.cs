namespace Day21;

using System.Text.RegularExpressions;
using Shared;

public class Solution
{
    private readonly Fighter boss;

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
        var weapons = bazaar[0];
        var armours = bazaar[1];
        var rings = bazaar[2].ToList();

        var possibleArmours = armours.Concat(new[] { Item.NoItem })
            .ToList();
        var possibleRings = rings
            .ChooseTwo()
            .Concat(rings.Select(r => (Item.NoItem, r)))
            .Concat(new[] { (ring1: Item.NoItem, ring2: Item.NoItem) })
            .ToList();

        orderedCombinations = weapons.SelectMany(w => possibleArmours.SelectMany(a => possibleRings.Select(r =>
            {
                var totalCost = w.Cost + a.Cost + r.Item1.Cost + r.Item2.Cost;
                var totalDamage = w.Damage + r.Item1.Damage + r.Item2.Damage;
                var totalArmour = a.Armour + r.Item1.Armour + r.Item2.Armour;
                return (totalCost, totalArmour, totalDamage);
            })))
            .OrderBy(it => it.totalCost);
    }

    public object PartOne()
    {
        return orderedCombinations
            .First(it => Fight(new Fighter(100, it.totalDamage, it.totalArmour), boss))
            .totalCost;
    }

    public object PartTwo()
    {
        return orderedCombinations
            .Last(it => !Fight(new Fighter(100, it.totalDamage, it.totalArmour), boss))
            .totalCost;
    }

    private static bool Fight(Fighter player, Fighter boss)
    {
        var turnsToKillBoss = (int)Math.Ceiling(boss.Hp / (double)(player.Damage - boss.Armour));
        return player.Hp - (boss.Damage- player.Armour) * (turnsToKillBoss - 1) > 0;
    }

    private readonly IOrderedEnumerable<(int totalCost, int totalArmour, int totalDamage)> orderedCombinations;
}