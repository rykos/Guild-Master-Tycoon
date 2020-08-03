public struct Damage
{
    public double Value;//Raw damage value
    public DamageType DamageType;//Type of damage

    public Damage(double value, DamageType damageType)
    {
        Value = value;
        DamageType = damageType;
    }
}

public enum DamageType
{
    None,
    //<Physical>
    Slashing,
    Blunt,
    Piercing,
    //</Physical>
    Poison,
    Bleed,
    Fire,
    Cold,
    Arcane,
    Lightning,
    Necrotic,
    Psychic,
    Radiant,
    True
}