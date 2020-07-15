using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public Sprite Avatar;
    //
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
