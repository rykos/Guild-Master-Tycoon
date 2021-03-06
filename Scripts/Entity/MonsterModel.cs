﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterModel : Entity
{
    public Stats Stats;
    public MonsterType MonsterType;

    public MonsterModel(Level lvl, MonsterType monsterType, Stats stats)
    {
        this.Level = lvl;
        this.Name = "Some Monster Name";
        this.MonsterType = monsterType;
        this.Stats = stats;
        this.MasterType = typeof(MonsterModel);
        this.Skills = new Abilities.Skills(this);
        this.Build();
    }

    public override void Build()
    {
        this.MaxHealth = this.Stats.Health;
        this.CurrentHealth = MaxHealth;
        this.Damage = this.Stats.Attack;
    }

    public override void Die()
    {
        Debug.Log($"Monster \"{this.Name}\" died");
    }

    public static MonsterModel MakeMonster(uint level)
    {
        var mm = new MonsterModel(new Level(level), MonsterType.Normal, new Stats(level * 10, level * 10, level * 10, level * 10));
        mm.Skills.AddSkill(AssetManager.Instance.Skills[3]);
        return mm;
    }
}

public enum MonsterType
{
    Normal,
    Rare,
    Elite,
    Boss
}