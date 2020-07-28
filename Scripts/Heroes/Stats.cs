[System.Serializable]
public struct Stats
{
    public float Attack;
    public float Defense;
    public float Health;

    public Stats(float attack, float defense, float health)
    {
        this.Attack = attack;
        this.Defense = defense;
        this.Health = health;
    }
    public Stats(Stats[] stats)
    {
        Stats rebuiltStats = new Stats();
        foreach (Stats stat in stats)
        {
            rebuiltStats = rebuiltStats + stat;
        }
        this = rebuiltStats;
    }
    public static Stats operator +(Stats a, Stats b)
    {
        return new Stats(a.Attack + b.Attack, a.Defense + b.Defense, a.Health + b.Health);
    }
    public static bool operator ==(Stats a, Stats b)
    {
        return (a.Attack == b.Attack) && (a.Defense == b.Defense) && (a.Health == b.Health);
    }
    public static bool operator !=(Stats a, Stats b)
    {
        return !(a == b);
    }
}

public struct StatsNew
{
    public float Attack;
    public float Defense;
    public float Dodge;
    public float Health;
    public int Actions;//Action points, might be moved to fight logic
}