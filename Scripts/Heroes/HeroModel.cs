using Abilities;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class HeroModel : Entity
{
    public string IconPath;
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

    public HeroModel(string iconPath, string name, Equipment equipment, Level level, Stats baseStats, double price)
    {
        IconPath = iconPath ?? throw new ArgumentNullException(nameof(iconPath));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
        Level = level ?? throw new ArgumentNullException(nameof(level));
        BaseStats = baseStats;
        Price = price;
        this.MasterType = typeof(HeroModel);
        this.Level.OnLevelUp += Level_OnLevelUp;//Hook event
    }

    private void Level_OnLevelUp()
    {
        this.Skills.AddSkillPoint();
    }

    public static HeroModel BuildHero(double price, string iconPath = null, string name = null, Equipment equipment = default,
        Level level = default, Stats stats = default)
    {
        HeroModel hero = new HeroModel(iconPath: iconPath, name: name, equipment: equipment, level: level, baseStats: stats, price: price);
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
        //no
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
