﻿using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// Hero Equipment
/// </summary>
[System.Serializable]
public class Equipment
{
    public delegate void ItemsChanged();
    public event ItemsChanged ItemsChangedEvent;
    //
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
        if (i != default)//Item already occupying slot
        {
            UnequipItem(i);
        }
        this.Items.Add(item);
        PlayerManager.Instance.PlayerModel.RemoveItem(item);
        this.ItemsChangedEvent?.Invoke();
    }

    public void UnequipItem(Item item)
    {
        Items.Remove(item);
        PlayerManager.Instance.PlayerModel.AddItem(item);
        this.ItemsChangedEvent?.Invoke();
    }

    public Item GetItemOfType(ItemType type)
    {
        return this.Items.FirstOrDefault(x => x.ItemType == type);
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
