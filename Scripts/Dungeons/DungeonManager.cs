﻿using System.Collections.Generic;

public class DungeonManager
{
    public delegate void DungeonManagerChanged();
    public event DungeonManagerChanged DungeonManagerChangedEvent;
    //Events
    public List<DungeonModel> ActiveDungeons = new List<DungeonModel>();

    public DungeonManager()
    {
        NewDungeon("Dung1", 5);
        NewDungeon("Dung2", 6);
    }

    public void NewDungeon(string name, uint level)
    {
        DungeonModel dm = new DungeonModel()
        {
            Name = name,
            Rarity = Rarity.Common,
            Level = new Level(level)
        };
        this.ActiveDungeons.Add(dm);
        this.DungeonManagerChangedEvent?.Invoke();
    }

    public void RemoveDungeon(DungeonModel dm)
    {
        this.ActiveDungeons.Remove(dm);
        this.DungeonManagerChangedEvent?.Invoke();
    }
}