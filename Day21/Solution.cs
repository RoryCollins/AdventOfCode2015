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
        var possibleArmours = armours.Concat(new[] { Item.NoItem })
            .ToList();
        var possibleRings = rings
            .ChooseTwo()
            .Concat(rings.Select(r => (Item.NoItem, r)))
            .Concat(new[] { (ring1: Item.NoItem, ring2: Item.NoItem) })
            .ToList();

        return weapons.SelectMany(w => possibleArmours.SelectMany(a => possibleRings.Select(r =>
            {
                var totalCost = w.Cost + a.Cost + r.Item1.Cost + r.Item2.Cost;
                var totalDamage = w.Damage + r.Item1.Damage + r.Item2.Damage;
                var totalArmour = a.Armour + r.Item1.Armour + r.Item2.Armour;
                return (totalCost, totalArmour, totalDamage);
            })))
            .OrderBy(it => it.totalCost)
            .First(it => isPlayerAlive(new Fighter(100, it.totalDamage, it.totalArmour), boss))
            .totalCost;
    }

    public object PartTwo()
    {
        return "Not yet implemented";
    }

    // private static bool Fight(Fighter player, Fighter boss)
    // {
    //     int damageReceived = Math.Max((boss.Damage - player.Armour), 1);
    //     int damageDealt = Math.Max((player.Damage - boss.Armour), 1);
    //     return player.Hp / damageReceived >= boss.Hp / damageDealt;
    // }

    Func<Fighter, Fighter, bool> isPlayerAlive = (player, boss) =>
    {
        var turnsToKillBoss = (int)Math.Ceiling(boss.Hp / (double)(player.Damage - boss.Armour));
        return player.Hp - (boss.Damage - player.Armour) * (turnsToKillBoss - 1) >= 0;
    };
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