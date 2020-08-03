using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;

public abstract class Entity
{
    public System.Type MasterType;
    //
    public Sprite Avatar;
    //Events
    public delegate void DamageTakenHandler();
    public DamageTakenHandler OnDamageTaken;
    //
    public string Name;
    public double CurrentHealth;
    public double MaxHealth;
    public Level Level;
    public Stats BaseStats;//Basic stats before calculation
    public Stats FinalStats;//Stats result
    public Skills Skills;
    public Perks Perks = new Perks();
    public List<Buff> Buffs = new List<Buff>();
    //
    public double Damage;

    public virtual void TakeHealing(Damage heal)
    {
        this.CurrentHealth += heal.Value;
        this.OnDamageTaken?.Invoke();
        if (this.CurrentHealth <= 0)
        {
            this.Die();
        }
    }
    public virtual void TakeDamage(Damage damage)
    {
        this.CurrentHealth -= damage.Value;
        this.OnDamageTaken?.Invoke();
        if (this.CurrentHealth <= 0)
        {
            this.Die();
        }
    }
    public virtual double DealDamage()
    {
        return this.Damage;
    }
    public float GetHealthPercentage()
    {
        return (float)(this.CurrentHealth / this.MaxHealth);
    }
    public virtual void Reset()
    {
        this.CurrentHealth = this.MaxHealth;
        this.Perks.Clear();
    }
    public abstract void Die();
    public abstract void Build();
    public virtual void Recalculate()//Recalculates stats
    {
        //Recalculate: Resistances, Stats
    }
    public virtual void BeginTurn()
    {
        this.Recalculate();
        for (int i = this.Buffs.Count - 1; i > -1; i--)
        {
            this.Buffs[i].Tick(this);
        }
    }
}

public struct EntityState
{
    public Entity Entity;
    public float HealthPercentage;

    public EntityState(Entity entity, float healthPercentage)
    {
        this.Entity = entity;
        this.HealthPercentage = healthPercentage;
    }
    public EntityState(Entity entity)
    {
        this.Entity = entity;
        this.HealthPercentage = 1;
    }
}