﻿using System.Collections.Generic;
using System.Linq;

public class DungeonModel
{
    public string Name;
    public Rarity Rarity;
    public Level Level;
    public List<MonsterModel> Monsters;

    public void GenerateMonsters(int amount = 5)
    {
        this.Monsters = new List<MonsterModel>();
        amount *= ((int)this.Rarity + 1);
        for (int i = 0; i < amount; i++)
        {
            this.Monsters.Add(MonsterModel.MakeMonster(this.Level));
        }
        this.Monsters.OrderBy(x => x.MaxHealth);
    }
}

public enum Rarity
{
    Common,//0
    Normal,//1
    Rare,//2
    Legendary//3
}