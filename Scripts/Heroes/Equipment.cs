using Boo.Lang;
using System;
using System.Linq;

[System.Serializable]
public class Equipment
{
    public List<Item> Items = new List<Item>();

    /// <summary>
    /// Try to add item to equiped state
    /// </summary>
    public void EquipItem(Item item)
    {
        Item i = this.Items.FirstOrDefault(x =>
        {
            if (x.ItemType == item.ItemType) { return true; }
            return false;
        });
        if (!i.Equals(default))//Item already occupying slot
        {
            UnequipItem(i);
        }
        this.Items.Add(item);
    }

    public void UnequipItem(Item item)
    {
        Items.Remove(item);
    }

    /// <summary>
    /// Takes all stats from items and returns it as single Stats struct
    /// </summary>
    public Stats GetStats()
    {
        Stats sumStats = new Stats();
        foreach (Item item in this.Items)
        {
            sumStats = sumStats + item.Stats;
        }
        return sumStats;
    }
}
