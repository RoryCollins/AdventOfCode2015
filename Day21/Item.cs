namespace Day21;

internal record Item(string Name, int Cost, int Damage, int Armour)
{
    public static Item NoItem => new Item("None", 0, 0, 0);
};