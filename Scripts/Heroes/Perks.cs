using System.Collections.Generic;

public class Perks
{
    public List<Perk> GetPerks { get { return this.perks; } }
    private List<Perk> perks = new List<Perk>();

    public void AddPerk(Perk perk)
    {
        int oldPerkID = this.perks.FindIndex(p => p.Type == perk.Type);
        if (oldPerkID != -1)
        {
            this.perks[oldPerkID] = this.perks[oldPerkID] + perk;
        }
        else
        {
            this.perks.Add(perk);
        }
    }
    public void AddPerk(PerkType type, float value)
    {
        this.AddPerk(new Perk(type, value));
    }
    public void AddPerks(List<Perk> perks)
    {
        foreach (Perk perk in perks) 
        {
            this.AddPerk(perk);
        }
    }
    public void AddPerks(Perks perks)
    {
        foreach (Perk perk in perks.GetPerks)
        {
            this.AddPerk(perk);
        }
    }
    public void Clear()
    {
        this.perks.Clear();
    }
}


public struct Perk
{
    public PerkType Type;
    public float Value;

    public Perk(PerkType type, float value)
    {
        Type = type;
        Value = value;
    }

    public static bool operator ==(Perk a, Perk b)
    {
        return (a.Type == b.Type && a.Value == b.Value);
    }
    public static bool operator !=(Perk a, Perk b)
    {
        return !(a == b);
    }
    public static Perk operator +(Perk a, Perk b)
    {
        if (a.Type != b.Type)
        {
            throw new System.Exception();
        }
        return new Perk(a.Type, a.Value + b.Value);
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

public enum PerkType
{
    MoreGold,
    MoreDamage,
}