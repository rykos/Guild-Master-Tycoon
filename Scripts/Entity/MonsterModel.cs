using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : Entity
{
    public string Name;
    public Level Level;
    public Stats Stats;
    public MonsterType MonsterType;

    public MonsterModel(Level lvl, MonsterType monsterType, Stats stats)
    {
        this.Level = lvl;
        this.Name = "Some Monster Name";
        this.MonsterType = monsterType;
        this.Stats = stats;
    }

    public override void Build()
    {
        this.MaxHealth = this.Stats.Health;
        this.CurrentHealth = MaxHealth;
        this.Damage = this.Stats.Attack;
    }

    public override void Die()
    {
        
    }
}

public enum MonsterType
{
    Normal,
    Rare,
    Elite,
    Boss
}