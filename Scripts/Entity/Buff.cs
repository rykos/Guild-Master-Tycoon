using System;
using System.Collections.Generic;

public class Buff
{
    public string Name;
    public string Description;
    public bool IsNegative;
    public uint Duration;//Duration in turns
    public Action<Entity> Action;

    public Buff(string name, string desc, bool IsNegative, uint duration, Action<Entity> action)
    {
        this.Name = name;
        this.Description = desc;
        this.IsNegative = IsNegative;
        this.Duration = duration;
        this.Action = action;
    }

    public void Tick(Entity entity)
    {
        this.Action?.Invoke(entity);
        this.Duration -= 1;
        if (this.Duration == 0)
        {
            entity.Buffs.Remove(this);
        }
    }
}