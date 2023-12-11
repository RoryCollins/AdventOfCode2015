namespace Day21;

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