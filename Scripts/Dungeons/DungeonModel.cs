using System.Collections.Generic;

public class DungeonModel
{
    public string Name;
    public Rarity Rarity;
    public Level Level;
}

public enum Rarity
{
    Common,
    Normal,
    Rare,
    Legendary
}