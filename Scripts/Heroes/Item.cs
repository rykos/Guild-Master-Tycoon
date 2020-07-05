using System;
using UnityEngine;

[System.Serializable]
public struct Item
{
    [NonSerialized]
    public Sprite Image;
    public uint IconID;
    public string Name;
    public ItemType ItemType;
    public Stats Stats;
    public uint RequiredLevel;
    public uint ItemLevel;

    /// <summary>
    /// TO_DO! Direct to real icon path
    /// </summary>
    public Item(string name, ItemType itemType, Stats stats, uint reqLevel, uint itemLevel)
    {
        this.Image = null;
        this.Name = name;
        this.ItemType = itemType;
        this.Stats = stats;
        this.RequiredLevel = reqLevel;
        this.ItemLevel = itemLevel;
        this.IconID = 0;
    }

    /// <summary>
    /// Auto generates missing attributes
    /// </summary>
    public static Item Build(string Name = null, ItemType it = default, Stats stats = default, uint reqLvl = default, uint ilvl = default)
    {
        return new Item(Name, it, stats, reqLvl, ilvl);
    }
}

public enum ItemType 
{
    None,
    Head,
    Chest,
    Legs,
    WeaponMelee,
    WeaponMagic
}