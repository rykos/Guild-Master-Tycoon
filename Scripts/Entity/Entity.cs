using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public System.Type MasterType;
    //
    public Sprite Avatar;
    //
    public string Name;
    public double CurrentHealth;
    public double MaxHealth;
    //
    public double Damage;

    public virtual void TakeDamage(double amount)
    {
        this.CurrentHealth -= amount;
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