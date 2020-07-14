using System.Collections;
using System.Collections.Generic;

public abstract class Entity
{
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
    public abstract void Die();
    public abstract void Build();
}
