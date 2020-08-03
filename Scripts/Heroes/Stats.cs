[System.Serializable]
public struct Stats
{
    public float Attack;
    public float Defense;
    public float Health;
    public float Speed;

    public Stats(float attack, float defense, float health, float speed)
    {
        this.Attack = attack;
        this.Defense = defense;
        this.Health = health;
        this.Speed = speed;
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
        return new Stats(a.Attack + b.Attack, a.Defense + b.Defense, a.Health + b.Health, a.Speed + b.Speed);
    }
    public static bool operator ==(Stats a, Stats b)
    {
        return (a.Attack == b.Attack) && (a.Defense == b.Defense) && (a.Health == b.Health);
    }
    public static bool operator !=(Stats a, Stats b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
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