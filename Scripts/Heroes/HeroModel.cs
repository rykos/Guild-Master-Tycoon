using Abilities;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class HeroModel : Entity
{
    public string IconPath;
    public string Name;
    public Equipment Equipment;//Equipment active on this hero
    public Level Level;
    //
    public Stats BaseStats;//Basic stats before calculation
    public Stats FinalStats;//Stats result
    //
    public Perks Perks = new Perks();
    //
    public Skills Skills;//All skills this hero poses
    public double Price;

    public static HeroModel BuildHero(string iconPath = null, string name = null, Equipment equipment = default,
        Level level = default, Stats stats = default)
    {
        HeroModel hero = new HeroModel()
        { IconPath = iconPath, Name = name, Equipment = equipment, Level = level, BaseStats = stats, MasterType = typeof(HeroModel) };
        hero.RecalculateStats();
        return hero;
    }

    public void EquipItem(Item item)
    {
        this.Equipment.EquipItem(item);
        RecalculateStats();
    }
    public void UnequipItem(Item item)
    {
        this.Equipment.UnequipItem(item);
        RecalculateStats();
    }
    private void RecalculateStats()
    {
        this.FinalStats = this.BaseStats + this.Equipment.GetStats();
        this.Build();
    }
    public Stats GetStats()
    {
        if (this.FinalStats == default)
        {
            this.FinalStats = this.BaseStats;
        }
        return this.FinalStats;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Build()
    {
        this.MaxHealth = this.FinalStats.Health;
        this.CurrentHealth = MaxHealth;
        this.Damage = this.FinalStats.Attack;
    }

    public void ClearPerks()
    {
        this.Perks.Clear();
    }
}

public enum HeroClass
{
    Warrior,//Balanced
    Mage,//Glass canon
    Knight,//High defense
    Rogue,//Glass canon, boosts loot
    Priest//Weaker, Gives buffs
}
