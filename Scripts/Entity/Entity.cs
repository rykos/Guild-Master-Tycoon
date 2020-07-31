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
    //
    public double Damage;

    public virtual void TakeDamage(double amount)
    {
        this.CurrentHealth -= amount;
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
    public abstract void Die();
    public abstract void Build();
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